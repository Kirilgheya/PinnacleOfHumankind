using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using static GameUI.UI.Interfaccia.AnimationHelper;

namespace GameUI.UI.Interfaccia
{
    public partial class ZoomAndPanControl : ContentControl, IScrollInfo, INotifyPropertyChanged
    {
        #region Fields
        /// <summary>
        /// Reference to the underlying content, which is named PART_Content in the template.
        /// </summary>
        private FrameworkElement _content = null;

        /// <summary>
        /// The transform that is applied to the content to scale it by 'ViewportZoom'.
        /// </summary>
        private ScaleTransform _contentZoomTransform = null;

        /// <summary>
        /// The transform that is applied to the content to offset it by 'ContentOffsetX' and 'ContentOffsetY'.
        /// </summary>
        private TranslateTransform _contentOffsetTransform = null;

        /// <summary>
        /// The height of the viewport in content coordinates, clamped to the height of the content.
        /// </summary>
        private double _constrainedContentViewportHeight = 0.0;

        /// <summary>
        /// The width of the viewport in content coordinates, clamped to the width of the content.
        /// </summary>
        private double _constrainedContentViewportWidth = 0.0;

        /// <summary>
        /// Normally when content offsets changes the content focus is automatically updated.
        /// This syncronization is disabled when 'disableContentFocusSync' is set to 'true'.
        /// When we are zooming in or out we 'disableContentFocusSync' is set to 'true' because 
        /// we are zooming in or out relative to the content focus we don't want to update the focus.
        /// </summary>
        private bool _disableContentFocusSync = false;

        /// <summary>
        /// Enable the update of the content offset as the content scale changes.
        /// This enabled for zooming about a point (google-maps style zooming) and zooming to a rect.
        /// </summary>
        private bool _enableContentOffsetUpdateFromScale = false;

        /// <summary>
        /// Used to disable syncronization between IScrollInfo interface and ContentOffsetX/ContentOffsetY.
        /// </summary>
        private bool _disableScrollOffsetSync = false;
        #endregion

        #region constructor and overrides
        /// <summary>
        /// Static constructor to define metadata for the control (and link it to the style in Generic.xaml).
        /// </summary>
        static ZoomAndPanControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZoomAndPanControl), new FrameworkPropertyMetadata(typeof(ZoomAndPanControl)));
        }

        /// <summary>
        /// Need to update zoom values if size changes, and update ViewportZoom if too low
        /// </summary>
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            if (sizeInfo.NewSize.Width <= 1 || sizeInfo.NewSize.Height <= 1) return;
            switch (_currentZoomTypeEnum)
            {
                case CurrentZoomTypeEnum.Fit:
                    InternalViewportZoom = ViewportHelpers.FitZoom(sizeInfo.NewSize.Width, sizeInfo.NewSize.Height,
                        _content?.ActualWidth, _content?.ActualHeight);
                    break;
                case CurrentZoomTypeEnum.Fill:
                    InternalViewportZoom = ViewportHelpers.FillZoom(sizeInfo.NewSize.Width, sizeInfo.NewSize.Height,
                        _content?.ActualWidth, _content?.ActualHeight);
                    break;
            }
            if (InternalViewportZoom < MinimumZoomClamped) InternalViewportZoom = MinimumZoomClamped;
            //
            // INotifyPropertyChanged property update
            //
            OnPropertyChanged(nameof(MinimumZoomClamped));
            OnPropertyChanged(nameof(FillZoomValue));
            OnPropertyChanged(nameof(FitZoomValue));
        }

        /// <summary>
        /// Called when a template has been applied to the control.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _content = this.Template.FindName("PART_Content", this) as FrameworkElement;
            if (_content != null)
            {
                //
                // Setup the transform on the content so that we can scale it by 'ViewportZoom'.
                //
                this._contentZoomTransform = new ScaleTransform(this.InternalViewportZoom, this.InternalViewportZoom);

                //
                // Setup the transform on the content so that we can translate it by 'ContentOffsetX' and 'ContentOffsetY'.
                //
                this._contentOffsetTransform = new TranslateTransform();
                UpdateTranslationX();
                UpdateTranslationY();

                //
                // Setup a transform group to contain the translation and scale transforms, and then
                // assign this to the content's 'RenderTransform'.
                //
                var transformGroup = new TransformGroup();
                transformGroup.Children.Add(this._contentOffsetTransform);
                transformGroup.Children.Add(this._contentZoomTransform);
                _content.RenderTransform = transformGroup;
                ZoomAndPanControl_EventHandlers_OnApplyTemplate();
            }
        }

        /// <summary>
        /// Measure the control and it's children.
        /// </summary>
        protected override Size MeasureOverride(Size constraint)
        {
            var infiniteSize = new Size(double.PositiveInfinity, double.PositiveInfinity);
            var childSize = base.MeasureOverride(infiniteSize);

            if (childSize != _unScaledExtent)
            {
                //
                // Use the size of the child as the un-scaled extent content.
                //
                _unScaledExtent = childSize;
                ScrollOwner?.InvalidateScrollInfo();
            }

            //
            // Update the size of the viewport onto the content based on the passed in 'constraint'.
            //
            UpdateViewportSize(constraint);
            var width = constraint.Width;
            var height = constraint.Height;
            if (double.IsInfinity(width)) width = childSize.Width;
            if (double.IsInfinity(height)) height = childSize.Height;
            UpdateTranslationX();
            UpdateTranslationY();
            return new Size(width, height);
        }

        /// <summary>
        /// Arrange the control and it's children.
        /// </summary>
        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            var size = base.ArrangeOverride(this.DesiredSize);

            if (_content.DesiredSize != _unScaledExtent)
            {
                //
                // Use the size of the child as the un-scaled extent content.
                //
                _unScaledExtent = _content.DesiredSize;
                ScrollOwner?.InvalidateScrollInfo();
            }

            //
            // Update the size of the viewport onto the content based on the passed in 'arrangeBounds'.
            //
            UpdateViewportSize(arrangeBounds);

            return size;
        }
        #endregion 

        #region IScrollInfo Data Members
        //
        // These data members are for the implementation of the IScrollInfo interface.
        // This interface works with the ScrollViewer such that when ZoomAndPanControl is 
        // wrapped (in XAML) with a ScrollViewer the IScrollInfo interface allows the ZoomAndPanControl to
        // handle the the scrollbar offsets.
        //
        // The IScrollInfo properties and member functions are implemented in ZoomAndPanControl_IScrollInfo.cs.
        //
        // There is a good series of articles showing how to implement IScrollInfo starting here:
        //     http://blogs.msdn.com/bencon/archive/2006/01/05/509991.aspx
        //

        /// <summary>
        /// Records the unscaled extent of the content.
        /// This is calculated during the measure and arrange.
        /// </summary>
        private Size _unScaledExtent = new Size(0, 0);

        /// <summary>
        /// Records the size of the viewport (in viewport coordinates) onto the content.
        /// This is calculated during the measure and arrange.
        /// </summary>
        private Size _viewport = new Size(0, 0);
        #endregion IScrollInfo Data Members

        #region Dependency Property Definitions
        //
        // Definitions for dependency properties.
        //
        /// <summary>
        /// This allows the same property name be used for direct and indirect access to this control.
        /// </summary>
        public ZoomAndPanControl ZoomAndPanContent => this;

        /// <summary>
        /// The duration of the animations (in seconds) started by calling AnimatedZoomTo and the other animation methods.
        /// </summary>
        public double AnimationDuration
        {
            get { return (double)GetValue(AnimationDurationProperty); }
            set { SetValue(AnimationDurationProperty, value); }
        }
        public static readonly DependencyProperty AnimationDurationProperty = DependencyProperty.Register("AnimationDuration",
            typeof(double), typeof(ZoomAndPanControl), new FrameworkPropertyMetadata(0.4));

        /// <summary>
        /// The duration of the animations (in seconds) started by calling AnimatedZoomTo and the other animation methods.
        /// </summary>
        public ZoomAndPanInitialPositionEnum ZoomAndPanInitialPosition
        {
            get { return (ZoomAndPanInitialPositionEnum)GetValue(ZoomAndPanInitialPositionProperty); }
            set { SetValue(ZoomAndPanInitialPositionProperty, value); }
        }
        public static readonly DependencyProperty ZoomAndPanInitialPositionProperty = DependencyProperty.Register("ZoomAndPanInitialPosition",
            typeof(ZoomAndPanInitialPositionEnum), typeof(ZoomAndPanControl), new FrameworkPropertyMetadata(ZoomAndPanInitialPositionEnum.Default, ZoomAndPanInitialPositionChanged));

        private static void ZoomAndPanInitialPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var zoomAndPanControl = (ZoomAndPanControl)d;
            // zoomAndPanControl.SetZoomAndPanInitialPosition();
        }

        /// <summary>
        /// Get/set the X offset (in content coordinates) of the view on the content.
        /// </summary>
        public double ContentOffsetX
        {
            get { return (double)GetValue(ContentOffsetXProperty); }
            set { SetValue(ContentOffsetXProperty, value); }
        }
        public static readonly DependencyProperty ContentOffsetXProperty = DependencyProperty.Register("ContentOffsetX",
            typeof(double), typeof(ZoomAndPanControl), new FrameworkPropertyMetadata(0.0, ContentOffsetX_PropertyChanged, ContentOffsetX_Coerce));

        /// <summary>
        /// Get/set the Y offset (in content coordinates) of the view on the content.
        /// </summary>
        public double ContentOffsetY
        {
            get { return (double)GetValue(ContentOffsetYProperty); }
            set { SetValue(ContentOffsetYProperty, value); }
        }
        public static readonly DependencyProperty ContentOffsetYProperty = DependencyProperty.Register("ContentOffsetY",
            typeof(double), typeof(ZoomAndPanControl), new FrameworkPropertyMetadata(0.0, ContentOffsetY_PropertyChanged, ContentOffsetY_Coerce));

        /// <summary>
        /// Get the viewport height, in content coordinates.
        /// </summary>
        public double ContentViewportHeight
        {
            get { return (double)GetValue(ContentViewportHeightProperty); }
            set { SetValue(ContentViewportHeightProperty, value); }
        }
        public static readonly DependencyProperty ContentViewportHeightProperty = DependencyProperty.Register("ContentViewportHeight",
             typeof(double), typeof(ZoomAndPanControl), new FrameworkPropertyMetadata(0.0));

        /// <summary>
        /// Get the viewport width, in content coordinates.
        /// </summary>
        public double ContentViewportWidth
        {
            get { return (double)GetValue(ContentViewportWidthProperty); }
            set { SetValue(ContentViewportWidthProperty, value); }
        }
        public static readonly DependencyProperty ContentViewportWidthProperty = DependencyProperty.Register("ContentViewportWidth",
            typeof(double), typeof(ZoomAndPanControl), new FrameworkPropertyMetadata(0.0));

        /// <summary>
        /// The X coordinate of the content focus, this is the point that we are focusing on when zooming.
        /// </summary>
        public double ContentZoomFocusX
        {
            get { return (double)GetValue(ContentZoomFocusXProperty); }
            set { SetValue(ContentZoomFocusXProperty, value); }
        }
        public static readonly DependencyProperty ContentZoomFocusXProperty = DependencyProperty.Register("ContentZoomFocusX",
            typeof(double), typeof(ZoomAndPanControl), new FrameworkPropertyMetadata(0.0));

        /// <summary>
        /// The Y coordinate of the content focus, this is the point that we are focusing on when zooming.
        /// </summary>
        public double ContentZoomFocusY
        {
            get { return (double)GetValue(ContentZoomFocusYProperty); }
            set { SetValue(ContentZoomFocusYProperty, value); }
        }
        public static readonly DependencyProperty ContentZoomFocusYProperty = DependencyProperty.Register("ContentZoomFocusY",
            typeof(double), typeof(ZoomAndPanControl), new FrameworkPropertyMetadata(0.0));

        /// <summary>
        /// Set to 'true' to enable the mouse wheel to scroll the zoom and pan control.
        /// This is set to 'false' by default.
        /// </summary>
        public bool IsMouseWheelScrollingEnabled
        {
            get { return (bool)GetValue(IsMouseWheelScrollingEnabledProperty); }
            set { SetValue(IsMouseWheelScrollingEnabledProperty, value); }
        }
        public static readonly DependencyProperty IsMouseWheelScrollingEnabledProperty = DependencyProperty.Register("IsMouseWheelScrollingEnabled",
            typeof(bool), typeof(ZoomAndPanControl), new FrameworkPropertyMetadata(false));

        /// <summary>
        /// Get/set the maximum value for 'ViewportZoom'.
        /// </summary>
        public double MaximumZoom
        {
            get { return (double)GetValue(MaximumZoomProperty); }
            set { SetValue(MaximumZoomProperty, value); }
        }
        public static readonly DependencyProperty MaximumZoomProperty = DependencyProperty.Register("MaximumZoom",
            typeof(double), typeof(ZoomAndPanControl), new FrameworkPropertyMetadata(10.0, MinimumOrMaximumZoom_PropertyChanged));

        /// <summary>
        /// Get/set the maximum value for 'ViewportZoom'.
        /// </summary>
        public MinimumZoomTypeEnum MinimumZoomType
        {
            get { return (MinimumZoomTypeEnum)GetValue(MinimumZoomTypeProperty); }
            set { SetValue(MinimumZoomTypeProperty, value); }
        }
        public static readonly DependencyProperty MinimumZoomTypeProperty = DependencyProperty.Register("MinimumZoomType",
            typeof(MinimumZoomTypeEnum), typeof(ZoomAndPanControl), new FrameworkPropertyMetadata(MinimumZoomTypeEnum.MinimumZoom));

        /// <summary>
        /// Get/set the MinimumZoom value for 'ViewportZoom'.
        /// </summary>
        public double MinimumZoom
        {
            get { return (double)GetValue(MinimumZoomProperty); }
            set { SetValue(MinimumZoomProperty, value); }
        }
        public static readonly DependencyProperty MinimumZoomProperty = DependencyProperty.Register("MinimumZoom",
            typeof(double), typeof(ZoomAndPanControl), new FrameworkPropertyMetadata(0.1, MinimumOrMaximumZoom_PropertyChanged));

        /// <summary>
        /// Get/set the MinimumZoom value for 'ViewportZoom'.
        /// </summary>
        public Point? MousePosition
        {
            get { return (Point?)GetValue(MousePositionProperty); }
            set { SetValue(MousePositionProperty, value); }
        }
        public static readonly DependencyProperty MousePositionProperty = DependencyProperty.Register("MousePosition",
            typeof(Point?), typeof(ZoomAndPanControl), new FrameworkPropertyMetadata(null, MinimumOrMaximumZoom_PropertyChanged));

        /// <summary>
        /// This is used for binding a slider to control the zoom. Cannot use the InternalUseAnimations because of all the 
        /// assumptions in when the this property is changed. THIS IS NOT USED FOR THE ANIMATIONS
        /// </summary>
        public bool UseAnimations
        {
            get { return (bool)GetValue(UseAnimationsProperty); }
            set { SetValue(UseAnimationsProperty, value); }
        }
        public static readonly DependencyProperty UseAnimationsProperty = DependencyProperty.Register("UseAnimations",
            typeof(bool), typeof(ZoomAndPanControl), new FrameworkPropertyMetadata(true));

        /// <summary>
        /// This is used for binding a slider to control the zoom. Cannot use the InternalViewportZoom because of all the 
        /// assumptions in when the this property is changed. THIS IS NOT USED FOR THE ANIMATIONS
        /// </summary>
        public double ViewportZoom
        {
            get { return (double)GetValue(ViewportZoomProperty); }
            set { SetValue(ViewportZoomProperty, value); }
        }
        public static readonly DependencyProperty ViewportZoomProperty = DependencyProperty.Register("ViewportZoom",
            typeof(double), typeof(ZoomAndPanControl), new FrameworkPropertyMetadata(1.0, ViewportZoom_PropertyChanged));

        /// <summary>
        /// The X coordinate of the viewport focus, this is the point in the viewport (in viewport coordinates) 
        /// that the content focus point is locked to while zooming in.
        /// </summary>
        public double ViewportZoomFocusX
        {
            get { return (double)GetValue(ViewportZoomFocusXProperty); }
            set { SetValue(ViewportZoomFocusXProperty, value); }
        }
        public static readonly DependencyProperty ViewportZoomFocusXProperty = DependencyProperty.Register("ViewportZoomFocusX",
            typeof(double), typeof(ZoomAndPanControl), new FrameworkPropertyMetadata(0.0));

        /// <summary>
        /// The Y coordinate of the viewport focus, this is the point in the viewport (in viewport coordinates) 
        /// that the content focus point is locked to while zooming in.
        /// </summary>
        public double ViewportZoomFocusY
        {
            get { return (double)GetValue(ViewportZoomFocusYProperty); }
            set { SetValue(ViewportZoomFocusYProperty, value); }
        }
        public static readonly DependencyProperty ViewportZoomFocusYProperty = DependencyProperty.Register("ViewportZoomFocusY",
            typeof(double), typeof(ZoomAndPanControl), new FrameworkPropertyMetadata(0.0));

        #endregion Dependency Property Definitions

        #region events
        /// <summary>
        /// Event raised when the ContentOffsetX property has changed.
        /// </summary>
        public event EventHandler ContentOffsetXChanged;

        /// <summary>
        /// Event raised when the ContentOffsetY property has changed.
        /// </summary>
        public event EventHandler ContentOffsetYChanged;

        /// <summary>
        /// Event raised when the ViewportZoom property has changed.
        /// </summary>
        public event EventHandler ContentZoomChanged;
        #endregion

        #region Event Handlers

        /// <summary>
        /// This is required for the animations, but has issues if set by something like a slider.
        /// </summary>
        private double InternalViewportZoom
        {
            get { return (double)GetValue(InternalViewportZoomProperty); }
            set { SetValue(InternalViewportZoomProperty, value); }
        }
        private static readonly DependencyProperty InternalViewportZoomProperty = DependencyProperty.Register("InternalViewportZoom",
            typeof(double), typeof(ZoomAndPanControl), new FrameworkPropertyMetadata(1.0, InternalViewportZoom_PropertyChanged, InternalViewportZoom_Coerce));

        /// <summary>
        /// Event raised when the 'ViewportZoom' property has changed value.
        /// </summary>
        private static void InternalViewportZoom_PropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var c = (ZoomAndPanControl)dependencyObject;

            if (c._contentZoomTransform != null)
            {
                //
                // Update the content scale transform whenever 'ViewportZoom' changes.
                //
                c._contentZoomTransform.ScaleX = c.InternalViewportZoom;
                c._contentZoomTransform.ScaleY = c.InternalViewportZoom;
            }

            //
            // Update the size of the viewport in content coordinates.
            //
            c.UpdateContentViewportSize();

            if (c._enableContentOffsetUpdateFromScale)
            {
                try
                {
                    // 
                    // Disable content focus syncronization.  We are about to update content offset whilst zooming
                    // to ensure that the viewport is focused on our desired content focus point.  Setting this
                    // to 'true' stops the automatic update of the content focus when content offset changes.
                    //
                    c._disableContentFocusSync = true;

                    //
                    // Whilst zooming in or out keep the content offset up-to-date so that the viewport is always
                    // focused on the content focus point (and also so that the content focus is locked to the 
                    // viewport focus point - this is how the google maps style zooming works).
                    //
                    var viewportOffsetX = c.ViewportZoomFocusX - (c.ViewportWidth / 2);
                    var viewportOffsetY = c.ViewportZoomFocusY - (c.ViewportHeight / 2);
                    var contentOffsetX = viewportOffsetX / c.InternalViewportZoom;
                    var contentOffsetY = viewportOffsetY / c.InternalViewportZoom;
                    c.ContentOffsetX = (c.ContentZoomFocusX - (c.ContentViewportWidth / 2)) - contentOffsetX;
                    c.ContentOffsetY = (c.ContentZoomFocusY - (c.ContentViewportHeight / 2)) - contentOffsetY;
                }
                finally
                {
                    c._disableContentFocusSync = false;
                }
            }
            c.ContentZoomChanged?.Invoke(c, EventArgs.Empty);
            c.ViewportZoom = c.InternalViewportZoom;
            c.OnPropertyChanged(new DependencyPropertyChangedEventArgs(ViewportZoomProperty, c.ViewportZoom, c.InternalViewportZoom));
            c.ScrollOwner?.InvalidateScrollInfo();
            c.SetCurrentZoomTypeEnum();
            c.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Method called to clamp the 'ViewportZoom' value to its valid range.
        /// </summary>
        private static object InternalViewportZoom_Coerce(DependencyObject dependencyObject, object baseValue)
        {
            var c = (ZoomAndPanControl)dependencyObject;
            var value = Math.Max((double)baseValue, c.MinimumZoomClamped);
            switch (c.MinimumZoomType)
            {
                case MinimumZoomTypeEnum.FitScreen:
                    value = Math.Min(Math.Max(value, c.FitZoomValue), c.MaximumZoom);
                    break;
                case MinimumZoomTypeEnum.FillScreen:
                    value = Math.Min(Math.Max(value, c.FillZoomValue), c.MaximumZoom);
                    break;
                case MinimumZoomTypeEnum.MinimumZoom:
                    value = Math.Min(Math.Max(value, c.MinimumZoom), c.MaximumZoom);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return value;
        }
        #endregion

        #region DependencyProperty Event Handlers
        /// <summary>
        /// Event raised 'MinimumZoom' or 'MaximumZoom' has changed.
        /// </summary>
        private static void MinimumOrMaximumZoom_PropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var c = (ZoomAndPanControl)o;
            c.InternalViewportZoom = Math.Min(Math.Max(c.InternalViewportZoom, c.MinimumZoomClamped), c.MaximumZoom);
        }

        /// <summary>
        /// Event raised when the 'ContentOffsetX' property has changed value.
        /// </summary>
        private static void ContentOffsetX_PropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var c = (ZoomAndPanControl)o;

            c.UpdateTranslationX();

            if (!c._disableContentFocusSync)
                //
                // Normally want to automatically update content focus when content offset changes.
                // Although this is disabled using 'disableContentFocusSync' when content offset changes due to in-progress zooming.
                //
                c.UpdateContentZoomFocusX();
            //
            // Raise an event to let users of the control know that the content offset has changed.
            //
            c.ContentOffsetXChanged?.Invoke(c, EventArgs.Empty);

            if (!c._disableScrollOffsetSync)
                //
                // Notify the owning ScrollViewer that the scrollbar offsets should be updated.
                //
                c.ScrollOwner?.InvalidateScrollInfo();
        }

        /// <summary>
        /// Method called to clamp the 'ContentOffsetX' value to its valid range.
        /// </summary>
        private static object ContentOffsetX_Coerce(DependencyObject d, object baseValue)
        {
            var c = (ZoomAndPanControl)d;
            var value = (double)baseValue;
            var minOffsetX = 0.0;
            var maxOffsetX = Math.Max(0.0, c._unScaledExtent.Width - c._constrainedContentViewportWidth);
            value = Math.Min(Math.Max(value, minOffsetX), maxOffsetX);
            return value;
        }

        /// <summary>
        /// Event raised when the 'ContentOffsetY' property has changed value.
        /// </summary>
        private static void ContentOffsetY_PropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var c = (ZoomAndPanControl)o;

            c.UpdateTranslationY();

            if (!c._disableContentFocusSync)
                //
                // Normally want to automatically update content focus when content offset changes.
                // Although this is disabled using 'disableContentFocusSync' when content offset changes due to in-progress zooming.
                //
                c.UpdateContentZoomFocusY();
            if (!c._disableScrollOffsetSync)
                //
                // Notify the owning ScrollViewer that the scrollbar offsets should be updated.
                //
                c.ScrollOwner?.InvalidateScrollInfo();
            //
            // Raise an event to let users of the control know that the content offset has changed.
            //
            c.ContentOffsetYChanged?.Invoke(c, EventArgs.Empty);
        }

        /// <summary>
        /// Method called to clamp the 'ContentOffsetY' value to its valid range.
        /// </summary>
        private static object ContentOffsetY_Coerce(DependencyObject d, object baseValue)
        {
            var c = (ZoomAndPanControl)d;
            var value = (double)baseValue;
            var minOffsetY = 0.0;
            var maxOffsetY = Math.Max(0.0, c._unScaledExtent.Height - c._constrainedContentViewportHeight);
            value = Math.Min(Math.Max(value, minOffsetY), maxOffsetY);
            return value;
        }

        private static void ViewportZoom_PropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var c = (ZoomAndPanControl)dependencyObject;
            var newZoom = (double)e.NewValue;
            if (c.InternalViewportZoom != newZoom)
            {
                var centerPoint = new Point(c.ContentOffsetX + (c._constrainedContentViewportWidth / 2), c.ContentOffsetY + (c._constrainedContentViewportHeight / 2));
                c.ZoomAboutPoint(newZoom, centerPoint);
            }
        }
        #endregion

        /// <summary>
        /// Reset the viewport zoom focus to the center of the viewport.
        /// </summary>
        private void ResetViewportZoomFocus()
        {
            ViewportZoomFocusX = ViewportWidth / 2;
            ViewportZoomFocusY = ViewportHeight / 2;
        }

        /// <summary>
        /// Update the viewport size from the specified size.
        /// </summary>
        private void UpdateViewportSize(Size newSize)
        {
            if (_viewport == newSize)
                return;

            _viewport = newSize;

            //
            // Update the viewport size in content coordiates.
            //
            UpdateContentViewportSize();

            //
            // Initialise the content zoom focus point.
            //
            UpdateContentZoomFocusX();
            UpdateContentZoomFocusY();

            //
            // Reset the viewport zoom focus to the center of the viewport.
            //
            ResetViewportZoomFocus();

            //
            // Update content offset from itself when the size of the viewport changes.
            // This ensures that the content offset remains properly clamped to its valid range.
            //
            this.ContentOffsetX = this.ContentOffsetX;
            this.ContentOffsetY = this.ContentOffsetY;

            //
            // Tell that owning ScrollViewer that scrollbar data has changed.
            //
            ScrollOwner?.InvalidateScrollInfo();
        }

        /// <summary>
        /// Update the size of the viewport in content coordinates after the viewport size or 'ViewportZoom' has changed.
        /// </summary>
        private void UpdateContentViewportSize()
        {
            ContentViewportWidth = ViewportWidth / InternalViewportZoom;
            ContentViewportHeight = ViewportHeight / InternalViewportZoom;

            _constrainedContentViewportWidth = Math.Min(ContentViewportWidth, _unScaledExtent.Width);
            _constrainedContentViewportHeight = Math.Min(ContentViewportHeight, _unScaledExtent.Height);

            UpdateTranslationX();
            UpdateTranslationY();
        }

        /// <summary>
        /// Update the X coordinate of the translation transformation.
        /// </summary>
        private void UpdateTranslationX()
        {
            if (this._contentOffsetTransform != null)
            {
                var scaledContentWidth = this._unScaledExtent.Width * this.InternalViewportZoom;
                if (scaledContentWidth < this.ViewportWidth)
                    //
                    // When the content can fit entirely within the viewport, center it.
                    //
                    this._contentOffsetTransform.X = (this.ContentViewportWidth - this._unScaledExtent.Width) / 2;
                else
                    this._contentOffsetTransform.X = -this.ContentOffsetX;
            }
        }

        /// <summary>
        /// Update the Y coordinate of the translation transformation.
        /// </summary>
        private void UpdateTranslationY()
        {
            if (this._contentOffsetTransform != null)
            {
                var scaledContentHeight = this._unScaledExtent.Height * this.InternalViewportZoom;
                if (scaledContentHeight < this.ViewportHeight)
                    //
                    // When the content can fit entirely within the viewport, center it.
                    //
                    this._contentOffsetTransform.Y = (this.ContentViewportHeight - this._unScaledExtent.Height) / 2;
                else
                    this._contentOffsetTransform.Y = -this.ContentOffsetY;
            }
        }

        /// <summary>
        /// Update the X coordinate of the zoom focus point in content coordinates.
        /// </summary>
        private void UpdateContentZoomFocusX()
        {
            ContentZoomFocusX = ContentOffsetX + (_constrainedContentViewportWidth / 2);
        }

        /// <summary>
        /// Update the Y coordinate of the zoom focus point in content coordinates.
        /// </summary>
        private void UpdateContentZoomFocusY()
        {
            ContentZoomFocusY = ContentOffsetY + (_constrainedContentViewportHeight / 2);
        }

        public double FitZoomValue => ViewportHelpers.FitZoom(ActualWidth, ActualHeight, _content?.ActualWidth, _content?.ActualHeight);
        public double FillZoomValue => ViewportHelpers.FillZoom(ActualWidth, ActualHeight, _content?.ActualWidth, _content?.ActualHeight);
        public double MinimumZoomClamped => ((MinimumZoomType == MinimumZoomTypeEnum.FillScreen) ? FillZoomValue
                                      : (MinimumZoomType == MinimumZoomTypeEnum.FitScreen) ? FitZoomValue
                                      : MinimumZoom).ToRealNumber();

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private enum CurrentZoomTypeEnum { Fill, Fit, Other }

        private CurrentZoomTypeEnum _currentZoomTypeEnum;

        private void SetCurrentZoomTypeEnum()
        {
            if (ViewportZoom.IsWithinOnePercent(FitZoomValue))
                _currentZoomTypeEnum = CurrentZoomTypeEnum.Fit;
            else if (ViewportZoom.IsWithinOnePercent(FillZoomValue))
                _currentZoomTypeEnum = CurrentZoomTypeEnum.Fill;
            else
                _currentZoomTypeEnum = CurrentZoomTypeEnum.Other;
        }
    }


    public partial class ZoomAndPanControl
    {
        private void ZoomAndPanControl_EventHandlers_OnApplyTemplate()
        {
            _partDragZoomBorder = this.Template.FindName("PART_DragZoomBorder", this) as Border;
            _partDragZoomCanvas = this.Template.FindName("PART_DragZoomCanvas", this) as Canvas;
        }

        /// <summary>
        /// The control for creating a zoom border
        /// </summary>
        private Border _partDragZoomBorder;

        /// <summary>
        /// The control for containing a zoom border
        /// </summary>
        private Canvas _partDragZoomCanvas;

        /// <summary>
        /// Specifies the current state of the mouse handling logic.
        /// </summary>
        private MouseHandlingModeEnum _mouseHandlingMode = MouseHandlingModeEnum.None;

        /// <summary>
        /// The point that was clicked relative to the ZoomAndPanControl.
        /// </summary>
        private Point _origZoomAndPanControlMouseDownPoint;

        /// <summary>
        /// The point that was clicked relative to the content that is contained within the ZoomAndPanControl.
        /// </summary>
        private Point _origContentMouseDownPoint;

        /// <summary>
        /// Records which mouse button clicked during mouse dragging.
        /// </summary>
        private MouseButton _mouseButtonDown;

        /// <summary>
        /// Event raised on mouse down in the ZoomAndPanControl.
        /// </summary>
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            SaveZoom();
            _content.Focus();
            Keyboard.Focus(_content);

            _mouseButtonDown = e.ChangedButton;
            _origZoomAndPanControlMouseDownPoint = e.GetPosition(this);
            _origContentMouseDownPoint = e.GetPosition(_content);

            if ((Keyboard.Modifiers & ModifierKeys.Shift) != 0 &&
                (e.ChangedButton == MouseButton.Left ||
                 e.ChangedButton == MouseButton.Right))
            {
                // Shift + left- or right-down initiates zooming mode.
                _mouseHandlingMode = MouseHandlingModeEnum.Zooming;
            }
            else if (_mouseButtonDown == MouseButton.Left)
            {
                // Just a plain old left-down initiates panning mode.
                _mouseHandlingMode = MouseHandlingModeEnum.Panning;
            }

            if (_mouseHandlingMode != MouseHandlingModeEnum.None)
            {
                // Capture the mouse so that we eventually receive the mouse up event.
                this.CaptureMouse();
            }
        }

        /// <summary>
        /// Event raised on mouse up in the ZoomAndPanControl.
        /// </summary>
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);

            if (_mouseHandlingMode != MouseHandlingModeEnum.None)
            {
                if (_mouseHandlingMode == MouseHandlingModeEnum.Zooming)
                {
                    if (_mouseButtonDown == MouseButton.Left)
                    {
                        // Shift + left-click zooms in on the content.
                        ZoomIn(_origContentMouseDownPoint);
                    }
                    else if (_mouseButtonDown == MouseButton.Right)
                    {
                        // Shift + left-click zooms out from the content.
                        ZoomOut(_origContentMouseDownPoint);
                    }
                }
                else if (_mouseHandlingMode == MouseHandlingModeEnum.DragZooming)
                {
                    var finalContentMousePoint = e.GetPosition(_content);
                    // When drag-zooming has finished we zoom in on the rectangle that was highlighted by the user.
                    ApplyDragZoomRect(finalContentMousePoint);
                }

                this.ReleaseMouseCapture();
                _mouseHandlingMode = MouseHandlingModeEnum.None;
            }
        }

        /// <summary>
        /// Event raised on mouse move in the ZoomAndPanControl.
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            var oldContentMousePoint = MousePosition;
            var curContentMousePoint = e.GetPosition(_content);
            MousePosition = curContentMousePoint.FilterClamp(_content.ActualWidth - 1, _content.ActualHeight - 1);
            OnPropertyChanged(new DependencyPropertyChangedEventArgs(MousePositionProperty, oldContentMousePoint,
                curContentMousePoint));

            if (_mouseHandlingMode == MouseHandlingModeEnum.Panning)
            {
                //
                // The user is left-dragging the mouse.
                // Pan the viewport by the appropriate amount.
                //
                var dragOffset = curContentMousePoint - _origContentMouseDownPoint;

                this.ContentOffsetX -= dragOffset.X;
                this.ContentOffsetY -= dragOffset.Y;

                e.Handled = true;
            }
            else if (_mouseHandlingMode == MouseHandlingModeEnum.Zooming)
            {
                var curZoomAndPanControlMousePoint = e.GetPosition(this);
                var dragOffset = curZoomAndPanControlMousePoint - _origZoomAndPanControlMouseDownPoint;
                double dragThreshold = 10;
                if (_mouseButtonDown == MouseButton.Left &&
                    (Math.Abs(dragOffset.X) > dragThreshold ||
                     Math.Abs(dragOffset.Y) > dragThreshold))
                {
                    //
                    // When Shift + left-down zooming mode and the user drags beyond the drag threshold,
                    // initiate drag zooming mode where the user can drag out a rectangle to select the area
                    // to zoom in on.
                    //
                    _mouseHandlingMode = MouseHandlingModeEnum.DragZooming;
                    InitDragZoomRect(_origContentMouseDownPoint, curContentMousePoint);
                }
            }
            else if (_mouseHandlingMode == MouseHandlingModeEnum.DragZooming)
            {
                //
                // When in drag zooming mode continously update the position of the rectangle
                // that the user is dragging out.
                //
                curContentMousePoint = e.GetPosition(this);
                SetDragZoomRect(_origZoomAndPanControlMouseDownPoint, curContentMousePoint);
            }
        }

        /// <summary>
        /// Event raised on mouse wheel moved in the ZoomAndPanControl.
        /// </summary>
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            DelayedSaveZoom750Miliseconds();
            e.Handled = true;

            if (e.Delta > 0)
                ZoomIn(e.GetPosition(_content));
            else if (e.Delta < 0)
                ZoomOut(e.GetPosition(_content));
        }

        /// <summary>
        /// Event raised with the double click command
        /// </summary>
        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            if ((Keyboard.Modifiers & ModifierKeys.Shift) == 0)
            {
                SaveZoom();
                this.AnimatedSnapTo(e.GetPosition(_content));
            }
        }

        #region private Zoom methods

        /// <summary>
        /// Zoom the viewport out, centering on the specified point (in content coordinates).
        /// </summary>
        private void ZoomOut(Point contentZoomCenter)
        {
            this.ZoomAboutPoint(this.InternalViewportZoom * 0.90909090909, contentZoomCenter);
        }

        /// <summary>
        /// Zoom the viewport in, centering on the specified point (in content coordinates).
        /// </summary>
        private void ZoomIn(Point contentZoomCenter)
        {
            this.ZoomAboutPoint(this.InternalViewportZoom * 1.1, contentZoomCenter);
        }

        /// <summary>
        /// Initialise the rectangle that the use is dragging out.
        /// </summary>
        private void InitDragZoomRect(Point pt1, Point pt2)
        {
            _partDragZoomCanvas.Visibility = Visibility.Visible;
            _partDragZoomBorder.Opacity = 1;
            SetDragZoomRect(pt1, pt2);
        }

        /// <summary>
        /// Update the position and size of the rectangle that user is dragging out.
        /// </summary>
        private void SetDragZoomRect(Point pt1, Point pt2)
        {
            //
            // Update the coordinates of the rectangle that is being dragged out by the user.
            // The we offset and rescale to convert from content coordinates.
            //
            var rect = ViewportHelpers.Clip(pt1, pt2, new Point(0, 0),
                new Point(_partDragZoomCanvas.ActualWidth, _partDragZoomCanvas.ActualHeight));
            ViewportHelpers.PositionBorderOnCanvas(_partDragZoomBorder, rect);
        }

        /// <summary>
        /// When the user has finished dragging out the rectangle the zoom operation is applied.
        /// </summary>
        private void ApplyDragZoomRect(Point finalContentMousePoint)
        {
            var rect = ViewportHelpers.Clip(finalContentMousePoint, _origContentMouseDownPoint, new Point(0, 0),
                new Point(_partDragZoomCanvas.ActualWidth, _partDragZoomCanvas.ActualHeight));
            this.AnimatedZoomTo(rect);
            // new Rect(contentX, contentY, contentWidth, contentHeight));
            FadeOutDragZoomRect();
        }

        //
        // Fade out the drag zoom rectangle.
        //
        private void FadeOutDragZoomRect()
        {
            AnimationHelper.StartAnimation(_partDragZoomBorder, OpacityProperty, 0.0, 0.1,
                delegate { _partDragZoomCanvas.Visibility = Visibility.Collapsed; }, UseAnimations);
        }

        #endregion

        #region Commands

        /// <summary>
        ///     Command to implement the zoom to fill 
        /// </summary>
        public ICommand FillCommand => _fillCommand ?? (_fillCommand = new RelayCommand(() =>
        {
            SaveZoom();
            AnimatedZoomToCentered(FillZoomValue);
            RaiseCanExecuteChanged();
        }, () => !InternalViewportZoom.IsWithinOnePercent(FillZoomValue) && FillZoomValue >= MinimumZoomClamped));

        private RelayCommand _fillCommand;

        /// <summary>
        ///     Command to implement the zoom to fit 
        /// </summary>
        public ICommand FitCommand => _fitCommand ?? (_fitCommand = new RelayCommand(() =>
        {
            SaveZoom();
            AnimatedZoomTo(FitZoomValue);
            RaiseCanExecuteChanged();
        }, () => !InternalViewportZoom.IsWithinOnePercent(FitZoomValue) && FitZoomValue >= MinimumZoomClamped));

        private RelayCommand _fitCommand;

        /// <summary>
        ///     Command to implement the zoom to a percentage where 100 (100%) is the default and 
        ///     shows the image at a zoom where 1 pixel is 1 pixel. Other percentages specified
        ///     with the command parameter. 50 (i.e. 50%) would display 4 times as much of the image
        /// </summary>
        public ICommand ZoomPercentCommand
            => _zoomPercentCommand ?? (_zoomPercentCommand = new RelayCommand<double>(value =>
            {
                SaveZoom();
                var adjustedValue = value == 0 ? 1 : value / 100;
                AnimatedZoomTo(adjustedValue);
                RaiseCanExecuteChanged();
            }, value =>
            {
                var adjustedValue = value == 0 ? 1 : value / 100;
                return !InternalViewportZoom.IsWithinOnePercent(adjustedValue) && adjustedValue >= MinimumZoomClamped;
            }));


        // Math.Abs(InternalViewportZoom - ((value == 0) ? 1.0 : value / 100)) > .01 * InternalViewportZoom 

        private RelayCommand<double> _zoomPercentCommand;

        /// <summary>
        ///     Command to implement the zoom ratio where 1 is is the the specified minimum. 2 make the image twices the size,
        ///     and is the default. Other values are specified with the CommandParameter. 
        /// </summary>
        public ICommand ZoomRatioFromMinimumCommand
            => _zoomRatioFromMinimumCommand ?? (_zoomRatioFromMinimumCommand = new RelayCommand<double>(value =>
            {
                SaveZoom();
                var adjustedValue = (value == 0 ? 2 : value) * MinimumZoomClamped;
                AnimatedZoomTo(adjustedValue);
                RaiseCanExecuteChanged();
            }, value =>
            {
                var adjustedValue = (value == 0 ? 2 : value) * MinimumZoomClamped;
                return !InternalViewportZoom.IsWithinOnePercent(adjustedValue) && adjustedValue >= MinimumZoomClamped;
            }));

        private RelayCommand<double> _zoomRatioFromMinimumCommand;


        /// <summary>
        ///     Command to implement the zoom out by 110% 
        /// </summary>
        public ICommand ZoomOutCommand => _zoomOutCommand ?? (_zoomOutCommand = new RelayCommand(() =>
        {
            DelayedSaveZoom1500Miliseconds();
            ZoomOut(new Point(ContentZoomFocusX, ContentZoomFocusY));
        }, () => InternalViewportZoom > MinimumZoomClamped));
        private RelayCommand _zoomOutCommand;

        /// <summary>
        ///     Command to implement the zoom in by 91% 
        /// </summary>
        public ICommand ZoomInCommand => _zoomInCommand ?? (_zoomInCommand = new RelayCommand(() =>
        {
            DelayedSaveZoom1500Miliseconds();
            ZoomIn(new Point(ContentZoomFocusX, ContentZoomFocusY));
        }, () => InternalViewportZoom < MaximumZoom));
        private RelayCommand _zoomInCommand;

        private void RaiseCanExecuteChanged()
        {
            _zoomPercentCommand?.RaiseCanExecuteChanged();
            _zoomOutCommand?.RaiseCanExecuteChanged();
            _zoomInCommand?.RaiseCanExecuteChanged();
            _fitCommand?.RaiseCanExecuteChanged();
            _fillCommand?.RaiseCanExecuteChanged();
        }
        #endregion

        /// <summary>
        /// When content is renewed, set event to set the initial position as specified
        /// </summary>
        /// <param name="oldContent"></param>
        /// <param name="newContent"></param>
        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
            if (oldContent != null)
                ((FrameworkElement)oldContent).SizeChanged -= SetZoomAndPanInitialPosition;
            ((FrameworkElement)newContent).SizeChanged += SetZoomAndPanInitialPosition;
        }

        /// <summary>
        /// When content is renewed, set the initial position as specified
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetZoomAndPanInitialPosition(object sender, SizeChangedEventArgs e)
        {
            switch (ZoomAndPanInitialPosition)
            {
                case ZoomAndPanInitialPositionEnum.Default:
                    break;
                case ZoomAndPanInitialPositionEnum.FitScreen:
                    InternalViewportZoom = FitZoomValue;
                    break;
                case ZoomAndPanInitialPositionEnum.FillScreen:
                    InternalViewportZoom = FillZoomValue;
                    ContentOffsetX = (_content.ActualWidth - ViewportWidth / InternalViewportZoom) / 2;
                    ContentOffsetY = (_content.ActualHeight - ViewportHeight / InternalViewportZoom) / 2;
                    break;
                case ZoomAndPanInitialPositionEnum.OneHundredPercentCentered:
                    InternalViewportZoom = 1.0;
                    ContentOffsetX = (_content.ActualWidth - ViewportWidth) / 2;
                    ContentOffsetY = (_content.ActualHeight - ViewportHeight) / 2;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    internal enum MouseHandlingModeEnum
    {
        /// <summary>
        /// Not in any special mode.
        /// </summary>
        None,

        /// <summary>
        /// The user is left-dragging rectangles with the mouse.
        /// </summary>
        DraggingRectangles,

        /// <summary>
        /// The user is left-mouse-button-dragging to pan the viewport.
        /// </summary>
        Panning,

        /// <summary>
        /// The user is holding down shift and left-clicking or right-clicking to zoom in or out.
        /// </summary>
        Zooming,

        /// <summary>
        /// The user is holding down shift and left-mouse-button-dragging to select a region to zoom to.
        /// </summary>
        DragZooming,
    }

    public partial class ZoomAndPanControl
    {
        /// <summary>
        /// Set to 'true' when the vertical scrollbar is enabled.
        /// </summary>
        public bool CanVerticallyScroll { get; set; } = false;

        /// <summary>
        /// Set to 'true' when the vertical scrollbar is enabled.
        /// </summary>
        public bool CanHorizontallyScroll { get; set; } = false;

        /// <summary>
        /// The width of the content (with 'ViewportZoom' applied).
        /// </summary>
        public double ExtentWidth => _unScaledExtent.Width * InternalViewportZoom;

        /// <summary>
        /// The height of the content (with 'ViewportZoom' applied).
        /// </summary>
        public double ExtentHeight => _unScaledExtent.Height * InternalViewportZoom;

        /// <summary>
        /// Get the width of the viewport onto the content.
        /// </summary>
        public double ViewportWidth => _viewport.Width;

        /// <summary>
        /// Get the height of the viewport onto the content.
        /// </summary>
        public double ViewportHeight => _viewport.Height;

        /// <summary>
        /// Reference to the ScrollViewer that is wrapped (in XAML) around the ZoomAndPanControl.
        /// Or set to null if there is no ScrollViewer.
        /// </summary>
        public ScrollViewer ScrollOwner { get; set; } = null;

        /// <summary>
        /// The offset of the horizontal scrollbar.
        /// </summary>
        public double HorizontalOffset => ContentOffsetX * InternalViewportZoom;

        /// <summary>
        /// The offset of the vertical scrollbar.
        /// </summary>
        public double VerticalOffset => ContentOffsetY * InternalViewportZoom;

        /// <summary>
        /// Called when the offset of the horizontal scrollbar has been set.
        /// </summary>
        public void SetHorizontalOffset(double offset)
        {
            if (_disableScrollOffsetSync) return;

            try
            {
                _disableScrollOffsetSync = true;
                ContentOffsetX = offset / InternalViewportZoom;
                DelayedSaveZoom750Miliseconds();
            }
            finally
            {
                _disableScrollOffsetSync = false;
            }
        }

        /// <summary>
        /// Called when the offset of the vertical scrollbar has been set.
        /// </summary>
        public void SetVerticalOffset(double offset)
        {
            if (_disableScrollOffsetSync) return;

            try
            {
                _disableScrollOffsetSync = true;
                ContentOffsetY = offset / InternalViewportZoom;
                DelayedSaveZoom750Miliseconds();
            }
            finally
            {
                _disableScrollOffsetSync = false;
            }
        }

        /// <summary>
        /// Shift the content offset one line up.
        /// </summary>
        public void LineUp()
        {
            DelayedSaveZoom750Miliseconds();
            ContentOffsetY -= (ContentViewportHeight / 10);
        }

        /// <summary>
        /// Shift the content offset one line down.
        /// </summary>
        public void LineDown()
        {
            DelayedSaveZoom750Miliseconds();
            ContentOffsetY += (ContentViewportHeight / 10);
        }

        /// <summary>
        /// Shift the content offset one line left.
        /// </summary>
        public void LineLeft()
        {
            DelayedSaveZoom750Miliseconds();
            ContentOffsetX -= (ContentViewportWidth / 10);
        }

        /// <summary>
        /// Shift the content offset one line right.
        /// </summary>
        public void LineRight()
        {
            DelayedSaveZoom750Miliseconds();
            ContentOffsetX += (ContentViewportWidth / 10);
        }

        /// <summary>
        /// Shift the content offset one page up.
        /// </summary>
        public void PageUp()
        {
            DelayedSaveZoom1500Miliseconds();
            ContentOffsetY -= ContentViewportHeight;
        }

        /// <summary>
        /// Shift the content offset one page down.
        /// </summary>
        public void PageDown()
        {
            DelayedSaveZoom1500Miliseconds();
            ContentOffsetY += ContentViewportHeight;
        }

        /// <summary>
        /// Shift the content offset one page left.
        /// </summary>
        public void PageLeft()
        {
            DelayedSaveZoom1500Miliseconds();
            ContentOffsetX -= ContentViewportWidth;
        }

        /// <summary>
        /// Shift the content offset one page right.
        /// </summary>
        public void PageRight()
        {
            DelayedSaveZoom1500Miliseconds();
            ContentOffsetX += ContentViewportWidth;
        }

        /// <summary>
        /// Don't handle mouse wheel input from the ScrollViewer, the mouse wheel is
        /// used for zooming in and out, not for manipulating the scrollbars.
        /// </summary>
        public void MouseWheelDown()
        {
            if (IsMouseWheelScrollingEnabled)
            {
                LineDown();
            }
        }

        /// <summary>
        /// Don't handle mouse wheel input from the ScrollViewer, the mouse wheel is
        /// used for zooming in and out, not for manipulating the scrollbars.
        /// </summary>
        public void MouseWheelLeft()
        {
            if (IsMouseWheelScrollingEnabled)
            {
                LineLeft();
            }
        }

        /// <summary>
        /// Don't handle mouse wheel input from the ScrollViewer, the mouse wheel is
        /// used for zooming in and out, not for manipulating the scrollbars.
        /// </summary>
        public void MouseWheelRight()
        {
            if (IsMouseWheelScrollingEnabled)
            {
                LineRight();
            }
        }

        /// <summary>
        /// Don't handle mouse wheel input from the ScrollViewer, the mouse wheel is
        /// used for zooming in and out, not for manipulating the scrollbars.
        /// </summary>
        public void MouseWheelUp()
        {
            if (IsMouseWheelScrollingEnabled)
            {
                LineUp();
            }
        }

        /// <summary>
        /// Bring the specified rectangle to view.
        /// </summary>
        public Rect MakeVisible(Visual visual, Rect rectangle)
        {
            if (_content.IsAncestorOf(visual))
            {
                var transformedRect = visual.TransformToAncestor(_content).TransformBounds(rectangle);
                var viewportRect = new Rect(ContentOffsetX, ContentOffsetY, ContentViewportWidth, ContentViewportHeight);
                if (!transformedRect.Contains(viewportRect))
                {
                    double horizOffset = 0;
                    double vertOffset = 0;

                    if (transformedRect.Left < viewportRect.Left)
                    {
                        //
                        // Want to move viewport left.
                        //
                        horizOffset = transformedRect.Left - viewportRect.Left;
                    }
                    else if (transformedRect.Right > viewportRect.Right)
                    {
                        //
                        // Want to move viewport right.
                        //
                        horizOffset = transformedRect.Right - viewportRect.Right;
                    }

                    if (transformedRect.Top < viewportRect.Top)
                    {
                        //
                        // Want to move viewport up.
                        //
                        vertOffset = transformedRect.Top - viewportRect.Top;
                    }
                    else if (transformedRect.Bottom > viewportRect.Bottom)
                    {
                        //
                        // Want to move viewport down.
                        //
                        vertOffset = transformedRect.Bottom - viewportRect.Bottom;
                    }

                    SnapContentOffsetTo(new Point(ContentOffsetX + horizOffset, ContentOffsetY + vertOffset));
                }
            }
            return rectangle;
        }
    }

    public partial class ZoomAndPanControl
    {
        private readonly Stack<UndoRedoStackItem> _undoStack = new Stack<UndoRedoStackItem>();
        private readonly Stack<UndoRedoStackItem> _redoStack = new Stack<UndoRedoStackItem>();
        private UndoRedoStackItem _viewportZoomCache;

        /// <summary> 
        ///     Record the previous zoom level, so that we can return to it.
        /// </summary>
        public void SaveZoom()
        {
            _viewportZoomCache = CreateUndoRedoStackItem();
            if (_undoStack.Any() && _viewportZoomCache.Equals(_undoStack.Peek())) return;
            _undoStack.Push(_viewportZoomCache);
            _redoStack.Clear();
            _undoZoomCommand?.RaiseCanExecuteChanged();
            _redoZoomCommand?.RaiseCanExecuteChanged();
        }

        /// <summary> 
        ///  Record the last saved zoom level, so that we can return to it if no activity for 750 milliseconds
        /// </summary>
        public void DelayedSaveZoom750Miliseconds()
        {
            if (_timer750Miliseconds?.Running != true) _viewportZoomCache = CreateUndoRedoStackItem();
            (_timer750Miliseconds ?? (_timer750Miliseconds = new KeepAliveTimer(TimeSpan.FromMilliseconds(740), () =>
            {
                if (_undoStack.Any() && _viewportZoomCache.Equals(_undoStack.Peek())) return;
                _undoStack.Push(_viewportZoomCache);
                _redoStack.Clear();
                _undoZoomCommand?.RaiseCanExecuteChanged();
                _redoZoomCommand?.RaiseCanExecuteChanged();
            }))).Nudge();
        }
        private KeepAliveTimer _timer750Miliseconds;


        /// <summary> 
        ///  Record the last saved zoom level, so that we can return to it if no activity for 1550 milliseconds
        /// </summary>
        public void DelayedSaveZoom1500Miliseconds()
        {
            if (!_timer1500Miliseconds?.Running != true) _viewportZoomCache = CreateUndoRedoStackItem();
            (_timer1500Miliseconds ?? (_timer1500Miliseconds = new KeepAliveTimer(TimeSpan.FromMilliseconds(1500), () =>
            {
                if (_undoStack.Any() && _viewportZoomCache.Equals(_undoStack.Peek())) return;
                _undoStack.Push(_viewportZoomCache);
                _redoStack.Clear();
                _undoZoomCommand?.RaiseCanExecuteChanged();
                _redoZoomCommand?.RaiseCanExecuteChanged();
            }))).Nudge();
        }
        private KeepAliveTimer _timer1500Miliseconds;

        private UndoRedoStackItem CreateUndoRedoStackItem()
        {
            return new UndoRedoStackItem(this.ContentOffsetX, this.ContentOffsetY,
                this.ContentViewportWidth, this.ContentViewportHeight, InternalViewportZoom);
        }

        /// <summary>
        ///     Jump back to the previous zoom level, saving current zoom to Redo Stack.
        /// </summary>
        private void UndoZoom()
        {
            _viewportZoomCache = CreateUndoRedoStackItem();
            if (!_undoStack.Any() || !_viewportZoomCache.Equals(_undoStack.Peek()))
                _redoStack.Push(_viewportZoomCache);
            _viewportZoomCache = _undoStack.Pop();
            this.AnimatedZoomTo(_viewportZoomCache.Zoom, _viewportZoomCache.Rect);
            SetScrollViewerFocus();
            _undoZoomCommand?.RaiseCanExecuteChanged();
            _redoZoomCommand?.RaiseCanExecuteChanged();
        }

        /// <summary>
        ///     Jump back to the most recent zoom level saved on redo stack.
        /// </summary>
        private void RedoZoom()
        {
            _viewportZoomCache = CreateUndoRedoStackItem();
            if (!_redoStack.Any() || !_viewportZoomCache.Equals(_redoStack.Peek()))
                _undoStack.Push(_viewportZoomCache);
            _viewportZoomCache = _redoStack.Pop();
            this.AnimatedZoomTo(_viewportZoomCache.Zoom, _viewportZoomCache.Rect);
            SetScrollViewerFocus();
            _undoZoomCommand?.RaiseCanExecuteChanged();
            _redoZoomCommand?.RaiseCanExecuteChanged();
        }

        private bool CanUndoZoom => _undoStack.Any();
        private bool CanRedoZoom => _redoStack.Any();

        /// <summary>
        ///     Command to implement Undo 
        /// </summary>
        public ICommand UndoZoomCommand => _undoZoomCommand ?? (_undoZoomCommand =
            new RelayCommand(UndoZoom, () => CanUndoZoom));
        private RelayCommand _undoZoomCommand;

        /// <summary>
        ///     Command to implement Redo 
        /// </summary>
        public ICommand RedoZoomCommand => _redoZoomCommand ?? (_redoZoomCommand =
             new RelayCommand(RedoZoom, () => CanRedoZoom));
        private RelayCommand _redoZoomCommand;

        private class UndoRedoStackItem
        {
            public UndoRedoStackItem(Rect rect, double zoom)
            {
                Rect = rect;
                Zoom = zoom;
            }

            public UndoRedoStackItem(double offsetX, double offsetY, double width, double height, double zoom)
            {
                Rect = new Rect(offsetX, offsetY, width, height);
                Zoom = zoom;
            }

            public Rect Rect { get; }
            public double Zoom { get; }

            public override string ToString()
            {
                return $"Rectangle {{{Rect.X},{Rect.X}}}, Zoom {Zoom}";
            }

            public bool Equals(UndoRedoStackItem obj)
            {
                return Zoom.IsWithinOnePercent(obj.Zoom) && Rect.Equals(obj.Rect);
            }
        }

        private void SetScrollViewerFocus()
        {
            var scrollViewer = _content.FindParentControl<ScrollViewer>();
            if (scrollViewer != null)
            {
                Keyboard.Focus(scrollViewer);
                scrollViewer.Focus();
            }
        }
    }

    public enum ZoomAndPanInitialPositionEnum
    {
        Default, FitScreen, FillScreen, OneHundredPercentCentered
    }

    public enum MinimumZoomTypeEnum
    {
        FitScreen, FillScreen, MinimumZoom
    }

    public partial class ZoomAndPanControl
    {
        #region Public Methods
        /// <summary>
        /// Do an animated zoom to view a specific scale and rectangle (in content coordinates).
        /// </summary>
        public void AnimatedZoomTo(double newScale, Rect contentRect)
        {
            AnimatedZoomPointToViewportCenter(newScale, new Point(contentRect.X + (contentRect.Width / 2), contentRect.Y + (contentRect.Height / 2)),
                delegate
                {
                    //
                    // At the end of the animation, ensure that we are snapped to the specified content offset.
                    // Due to zooming in on the content focus point and rounding errors, the content offset may
                    // be slightly off what we want at the end of the animation and this bit of code corrects it.
                    //
                    this.ContentOffsetX = contentRect.X;
                    this.ContentOffsetY = contentRect.Y;
                });
        }

        /// <summary>
        /// Do an animated zoom to the specified rectangle (in content coordinates).
        /// </summary>
        public void AnimatedZoomTo(Rect contentRect)
        {
            var scaleX = this.ContentViewportWidth / contentRect.Width;
            var scaleY = this.ContentViewportHeight / contentRect.Height;
            var contentFitZoom = this.InternalViewportZoom * Math.Min(scaleX, scaleY);
            AnimatedZoomPointToViewportCenter(contentFitZoom, new Point(contentRect.X + (contentRect.Width / 2), contentRect.Y + (contentRect.Height / 2)), null);
        }

        /// <summary>
        /// Instantly zoom to the specified rectangle (in content coordinates).
        /// </summary>
        public void ZoomTo(Rect contentRect)
        {
            var scaleX = this.ContentViewportWidth / contentRect.Width;
            var scaleY = this.ContentViewportHeight / contentRect.Height;
            var newScale = this.InternalViewportZoom * Math.Min(scaleX, scaleY);

            ZoomPointToViewportCenter(newScale, new Point(contentRect.X + (contentRect.Width / 2), contentRect.Y + (contentRect.Height / 2)));
        }

        /// <summary>
        /// Instantly center the view on the specified point (in content coordinates).
        /// </summary>
        public void SnapContentOffsetTo(Point contentOffset)
        {
            AnimationHelper.CancelAnimation(this, ContentOffsetXProperty);
            AnimationHelper.CancelAnimation(this, ContentOffsetYProperty);

            this.ContentOffsetX = contentOffset.X;
            this.ContentOffsetY = contentOffset.Y;
        }

        /// <summary>
        /// Instantly center the view on the specified point (in content coordinates).
        /// </summary>
        public void SnapTo(Point contentPoint)
        {
            AnimationHelper.CancelAnimation(this, ContentOffsetXProperty);
            AnimationHelper.CancelAnimation(this, ContentOffsetYProperty);

            this.ContentOffsetX = contentPoint.X - (this.ContentViewportWidth / 2);
            this.ContentOffsetY = contentPoint.Y - (this.ContentViewportHeight / 2);
        }

        /// <summary>
        /// Use animation to center the view on the specified point (in content coordinates).
        /// </summary>
        public void AnimatedSnapTo(Point contentPoint)
        {
            var newX = contentPoint.X - (this.ContentViewportWidth / 2);
            var newY = contentPoint.Y - (this.ContentViewportHeight / 2);

            AnimationHelper.StartAnimation(this, ContentOffsetXProperty, newX, AnimationDuration, UseAnimations);
            AnimationHelper.StartAnimation(this, ContentOffsetYProperty, newY, AnimationDuration, UseAnimations);
        }

        /// <summary>
        /// Zoom in/out centered on the specified point (in content coordinates).
        /// The focus point is kept locked to it's on screen position (ala google maps).
        /// </summary>
        public void AnimatedZoomAboutPoint(double newContentZoom, Point contentZoomFocus)
        {
            newContentZoom = Math.Min(Math.Max(newContentZoom, MinimumZoomClamped), MaximumZoom);

            AnimationHelper.CancelAnimation(this, ContentZoomFocusXProperty);
            AnimationHelper.CancelAnimation(this, ContentZoomFocusYProperty);
            AnimationHelper.CancelAnimation(this, ViewportZoomFocusXProperty);
            AnimationHelper.CancelAnimation(this, ViewportZoomFocusYProperty);

            ContentZoomFocusX = contentZoomFocus.X;
            ContentZoomFocusY = contentZoomFocus.Y;
            ViewportZoomFocusX = (ContentZoomFocusX - ContentOffsetX) * InternalViewportZoom;
            ViewportZoomFocusY = (ContentZoomFocusY - ContentOffsetY) * InternalViewportZoom;

            //
            // When zooming about a point make updates to ViewportZoom also update content offset.
            //
            _enableContentOffsetUpdateFromScale = true;

            AnimationHelper.StartAnimation(this, InternalViewportZoomProperty, newContentZoom, AnimationDuration,
                (sender, e) =>
                {
                    _enableContentOffsetUpdateFromScale = false;
                    ResetViewportZoomFocus();
                }, UseAnimations);
        }

        /// <summary>
        /// Zoom in/out centered on the specified point (in content coordinates).
        /// The focus point is kept locked to it's on screen position (ala google maps).
        /// </summary>
        public void ZoomAboutPoint(double newContentZoom, Point contentZoomFocus)
        {
            newContentZoom = Math.Min(Math.Max(newContentZoom, MinimumZoomClamped), MaximumZoom);

            var screenSpaceZoomOffsetX = (contentZoomFocus.X - ContentOffsetX) * InternalViewportZoom;
            var screenSpaceZoomOffsetY = (contentZoomFocus.Y - ContentOffsetY) * InternalViewportZoom;
            var contentSpaceZoomOffsetX = screenSpaceZoomOffsetX / newContentZoom;
            var contentSpaceZoomOffsetY = screenSpaceZoomOffsetY / newContentZoom;
            var newContentOffsetX = contentZoomFocus.X - contentSpaceZoomOffsetX;
            var newContentOffsetY = contentZoomFocus.Y - contentSpaceZoomOffsetY;

            AnimationHelper.CancelAnimation(this, InternalViewportZoomProperty);
            AnimationHelper.CancelAnimation(this, ContentOffsetXProperty);
            AnimationHelper.CancelAnimation(this, ContentOffsetYProperty);

            this.InternalViewportZoom = newContentZoom;
            this.ContentOffsetX = newContentOffsetX;
            this.ContentOffsetY = newContentOffsetY;
            RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Zoom in/out centered on the viewport center.
        /// </summary>
        public void AnimatedZoomTo(double viewportZoom)
        {
            var xadjust = (ContentViewportWidth - _content.ActualWidth) * InternalViewportZoom / 2;
            var yadjust = (ContentViewportHeight - _content.ActualHeight) * InternalViewportZoom / 2;
            var zoomCenter = (InternalViewportZoom >= FillZoomValue)
                ? new Point(ContentOffsetX + (ContentViewportWidth / 2), ContentOffsetY + (ContentViewportHeight / 2))
                : new Point(_content.ActualWidth / 2 - xadjust, _content.ActualHeight / 2 + yadjust);
            AnimatedZoomAboutPoint(viewportZoom, zoomCenter);
        }

        /// <summary>
        /// Zoom in/out centered on the viewport center.
        /// </summary>
        public void AnimatedZoomToCentered(double viewportZoom)
        {
            var zoomCenter = new Point(_content.ActualWidth / 2, _content.ActualHeight / 2); ;
            AnimatedZoomAboutPoint(viewportZoom, zoomCenter);
        }

        /// <summary>
        /// Zoom in/out centered on the viewport center.
        /// </summary>
        public void ZoomTo(double viewportZoom)
        {
            var zoomCenter = new Point(ContentOffsetX + (ContentViewportWidth / 2), ContentOffsetY + (ContentViewportHeight / 2));
            ZoomAboutPoint(viewportZoom, zoomCenter);
        }

        /// <summary>
        /// Do animation that scales the content so that it fits completely in the control.
        /// </summary>
        public void AnimatedScaleToFit()
        {
            if (_content == null)
                throw new ApplicationException("PART_Content was not found in the ZoomAndPanControl visual template!");
            ZoomTo(FillZoomValue);
            //AnimatedZoomTo(new Rect(0, 0, _content.ActualWidth, _content.ActualHeight));
        }

        /// <summary>
        /// Instantly scale the content so that it fits completely in the control.
        /// </summary>
        public void ScaleToFit()
        {
            if (_content == null)
                throw new ApplicationException("PART_Content was not found in the ZoomAndPanControl visual template!");
            ZoomTo(FitZoomValue);
            //ZoomTo(new Rect(0, 0, _content.ActualWidth, _content.ActualHeight));
        }

        /// <summary>
        /// Zoom to the specified scale and move the specified focus point to the center of the viewport.
        /// </summary>
        private void AnimatedZoomPointToViewportCenter(double newContentZoom, Point contentZoomFocus, EventHandler callback)
        {
            newContentZoom = Math.Min(Math.Max(newContentZoom, MinimumZoomClamped), MaximumZoom);

            AnimationHelper.CancelAnimation(this, ContentZoomFocusXProperty);
            AnimationHelper.CancelAnimation(this, ContentZoomFocusYProperty);
            AnimationHelper.CancelAnimation(this, ViewportZoomFocusXProperty);
            AnimationHelper.CancelAnimation(this, ViewportZoomFocusYProperty);

            ContentZoomFocusX = contentZoomFocus.X;
            ContentZoomFocusY = contentZoomFocus.Y;
            ViewportZoomFocusX = (ContentZoomFocusX - ContentOffsetX) * InternalViewportZoom;
            ViewportZoomFocusY = (ContentZoomFocusY - ContentOffsetY) * InternalViewportZoom;

            //
            // When zooming about a point make updates to ViewportZoom also update content offset.
            //
            _enableContentOffsetUpdateFromScale = true;

            AnimationHelper.StartAnimation(this, InternalViewportZoomProperty, newContentZoom, AnimationDuration,
                delegate (object sender, EventArgs e)
                {
                    _enableContentOffsetUpdateFromScale = false;
                    callback?.Invoke(this, EventArgs.Empty);
                }, UseAnimations);

            AnimationHelper.StartAnimation(this, ViewportZoomFocusXProperty, ViewportWidth / 2, AnimationDuration, UseAnimations);
            AnimationHelper.StartAnimation(this, ViewportZoomFocusYProperty, ViewportHeight / 2, AnimationDuration, UseAnimations);
        }

        /// <summary>
        /// Zoom to the specified scale and move the specified focus point to the center of the viewport.
        /// </summary>
        private void ZoomPointToViewportCenter(double newContentZoom, Point contentZoomFocus)
        {
            newContentZoom = Math.Min(Math.Max(newContentZoom, MinimumZoomClamped), MaximumZoom);

            AnimationHelper.CancelAnimation(this, InternalViewportZoomProperty);
            AnimationHelper.CancelAnimation(this, ContentOffsetXProperty);
            AnimationHelper.CancelAnimation(this, ContentOffsetYProperty);

            this.InternalViewportZoom = newContentZoom;
            this.ContentOffsetX = contentZoomFocus.X - (ContentViewportWidth / 2);
            this.ContentOffsetY = contentZoomFocus.Y - (ContentViewportHeight / 2);
        }
        #endregion
    }

    internal static class DoubleHelpers
    {
        public static double ToRealNumber(this double value, double defaultValue = 0)
        {
            return (double.IsInfinity(value) || double.IsNaN(value)) ? defaultValue : value;
        }
    }

    internal static class AnimationHelper
    {
        /// <summary>
        /// Starts an animation to a particular value on the specified dependency property.
        /// </summary>
        public static void StartAnimation(UIElement animatableElement, DependencyProperty dependencyProperty, double toValue, double animationDurationSeconds, bool useAnimations)
        {
            StartAnimation(animatableElement, dependencyProperty, toValue, animationDurationSeconds, null, useAnimations);
        }

        /// <summary>
        /// Starts an animation to a particular value on the specified dependency property.
        /// You can pass in an event handler to call when the animation has completed.
        /// </summary>
        public static void StartAnimation(UIElement animatableElement, DependencyProperty dependencyProperty, double toValue, double animationDurationSeconds, EventHandler completedEvent, bool useAnimations)
        {
            if (useAnimations)
            {
                var fromValue = (double)animatableElement.GetValue(dependencyProperty);

                var animation = new DoubleAnimation
                {
                    From = fromValue,
                    To = toValue,
                    Duration = TimeSpan.FromSeconds(animationDurationSeconds)
                };

                animation.Completed += delegate (object sender, EventArgs e)
                {
                    //
                    // When the animation has completed bake final value of the animation
                    // into the property.
                    //
                    animatableElement.SetValue(dependencyProperty, animatableElement.GetValue(dependencyProperty));
                    CancelAnimation(animatableElement, dependencyProperty);
                    completedEvent?.Invoke(sender, e);
                };
                animation.Freeze();
                animatableElement.BeginAnimation(dependencyProperty, animation);
            }
            else
            {
                animatableElement.SetValue(dependencyProperty, toValue);
                completedEvent?.Invoke(null, new EventArgs());
            }
        }

        /// <summary>
        /// Cancel any animations that are running on the specified dependency property.
        /// </summary>
        public static void CancelAnimation(UIElement animatableElement, DependencyProperty dependencyProperty)
        {
            animatableElement.BeginAnimation(dependencyProperty, null);
        }

        internal class RelayCommand : ICommand
        {
            private readonly Action _execute;
            private readonly Func<bool> _canExecute;

            /// <summary>
            /// Initializes a new instance of the RelayCommand class that 
            /// can always execute.
            /// </summary>
            /// <param name="execute">The execution logic.</param>
            /// <exception cref="ArgumentNullException">If the execute argument is null.</exception>
            public RelayCommand(Action execute)
                : this(execute, null)
            {
            }

            /// <summary>
            /// Initializes a new instance of the RelayCommand class.
            /// </summary>
            /// <param name="execute">The execution logic.</param>
            /// <param name="canExecute">The execution status logic.</param>
            /// <exception cref="ArgumentNullException">If the execute argument is null.</exception>
            public RelayCommand(Action execute, Func<bool> canExecute)
            {
                if (execute == null)
                {
                    throw new ArgumentNullException(nameof(execute));
                }

                _execute = execute;

                if (canExecute != null)
                {
                    _canExecute = canExecute;
                }
            }

#if SILVERLIGHT
        /// <summary>
        /// Occurs when changes occur that affect whether the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;
#else
#if NETFX_CORE
        /// <summary>
        /// Occurs when changes occur that affect whether the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;
#else
#if XAMARIN
        /// <summary>
        /// Occurs when changes occur that affect whether the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;
#else
            /// <summary>
            /// Occurs when changes occur that affect whether the command should execute.
            /// </summary>
            public event EventHandler CanExecuteChanged
            {
                add
                {
                    if (_canExecute != null)
                        CommandManager.RequerySuggested += value;
                }

                remove
                {
                    if (_canExecute != null)
                        CommandManager.RequerySuggested -= value;
                }
            }
#endif
#endif
#endif

            /// <summary>
            /// Raises the <see cref="CanExecuteChanged" /> event.
            /// </summary>
            [SuppressMessage(
                "Microsoft.Performance",
                "CA1822:MarkMembersAsStatic",
                Justification = "The this keyword is used in the Silverlight version")]
            [SuppressMessage(
                "Microsoft.Design",
                "CA1030:UseEventsWhereAppropriate",
                Justification = "This cannot be an event")]
            public void RaiseCanExecuteChanged()
            {
#if SILVERLIGHT
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
#else
#if NETFX_CORE
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
#else
#if XAMARIN
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
#else
                CommandManager.InvalidateRequerySuggested();
#endif
#endif
#endif
            }

            /// <summary>
            /// Defines the method that determines whether the command can execute in its current state.
            /// </summary>
            /// <param name="parameter">This parameter will always be ignored.</param>
            /// <returns>true if this command can be executed; otherwise, false.</returns>
            public bool CanExecute(object parameter)
            {
                return _canExecute == null || _canExecute();
            }

            /// <summary>
            /// Defines the method to be called when the command is invoked. 
            /// </summary>
            /// <param name="parameter">This parameter will always be ignored.</param>
            public virtual void Execute(object parameter)
            {
                if (CanExecute(parameter))
                    _execute?.Invoke();
            }
        }
    }

    internal class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

        /// <summary>
        /// Initializes a new instance of the RelayCommand class that 
        /// can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <exception cref="ArgumentNullException">If the execute argument is null.</exception>
        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RelayCommand class.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        /// <exception cref="ArgumentNullException">If the execute argument is null.</exception>
        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            _execute = execute;

            if (canExecute != null)
            {
                _canExecute = canExecute;
            }
        }

#if SILVERLIGHT
        /// <summary>
        /// Occurs when changes occur that affect whether the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;
#else
#if NETFX_CORE
        /// <summary>
        /// Occurs when changes occur that affect whether the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;
#else
#if XAMARIN
        /// <summary>
        /// Occurs when changes occur that affect whether the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;
#else
        /// <summary>
        /// Occurs when changes occur that affect whether the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                    CommandManager.RequerySuggested += value;
            }

            remove
            {
                if (_canExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }
#endif
#endif
#endif

        /// <summary>
        /// Raises the <see cref="CanExecuteChanged" /> event.
        /// </summary>
        [SuppressMessage(
            "Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "The this keyword is used in the Silverlight version")]
        [SuppressMessage(
            "Microsoft.Design",
            "CA1030:UseEventsWhereAppropriate",
            Justification = "This cannot be an event")]
        public void RaiseCanExecuteChanged()
        {
#if SILVERLIGHT
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
#else
#if NETFX_CORE
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
#else
#if XAMARIN
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
#else
            CommandManager.InvalidateRequerySuggested();
#endif
#endif
#endif
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">This parameter will always be ignored.</param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null) return true;
            if (parameter == null
#if NETFX_CORE
                    && typeof(T).GetTypeInfo().IsValueType)
#else
                    && typeof(T).IsValueType)
#endif
            {
                return _canExecute(default(T));
            }

            return _canExecute((T)parameter);
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked. 
        /// </summary>
        /// <param name="parameter">This parameter will always be ignored.</param>
        public virtual void Execute(object parameter)
        {
            var val = parameter;

#if !NETFX_CORE
            if (parameter != null
                && parameter.GetType() != typeof(T))
            {
                if (parameter is IConvertible)
                {
                    val = Convert.ChangeType(parameter, typeof(T), null);
                }
            }
#endif

            if (val == null)
            {
#if NETFX_CORE
                    if (typeof(T).GetTypeInfo().IsValueType)
#else
                if (typeof(T).IsValueType)
#endif
                {
                    _execute(default(T));
                }
                else
                {
                    _execute((T)val);
                }
            }
            else
            {
                _execute((T)val);
            }
        }
    }

    public static class VisualTreeHelpers
    {
        /// <summary>
        /// Find first paretn of type T in VisualTree.
        /// </summary>
        public static T FindParentControl<T>(this DependencyObject control) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(control);
            while (parent != null && !(parent is T))
                parent = VisualTreeHelper.GetParent(parent);
            return parent as T;
        }

        /// <summary>
        /// Find first child of type T in VisualTree.
        /// </summary>
        public static T FindChildControl<T>(this DependencyObject control) where T : DependencyObject
        {
            int childNumber = VisualTreeHelper.GetChildrenCount(control);
            for (var i = 0; i < childNumber; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(control, i);
                return (child is T)
                    ? (T)child : FindChildControl<T>(child);
            }
            return null;
        }
    }

    public class KeepAliveTimer
    {
        private readonly DispatcherTimer _timer;
        private DateTime _startTime;
        private TimeSpan? _runTime;

        public TimeSpan Time { get; set; }
        public Action Action { get; set; }
        public bool Running { get; private set; }

        public KeepAliveTimer(TimeSpan time, Action action)
        {
            Time = time;
            Action = action;
            _timer = new DispatcherTimer(DispatcherPriority.ApplicationIdle) { Interval = time };
            _timer.Tick += TimerExpired;
        }

        private void TimerExpired(object sender, EventArgs e)
        {
            lock (_timer)
            {
                Running = false;
                _timer.Stop();
                _runTime = DateTime.UtcNow.Subtract(_startTime);
                Action();
            }
        }

        public void Nudge()
        {
            lock (_timer)
            {
                if (!Running)
                {
                    _startTime = DateTime.UtcNow;
                    _runTime = null;
                    _timer.Start();
                    Running = true;
                }
                else
                {
                    //Reset the timer
                    _timer.Stop();
                    _timer.Start();
                }
            }
        }

        public TimeSpan GetTimeSpan()
        {
            return _runTime ?? DateTime.UtcNow.Subtract(_startTime);
        }
    }
}
