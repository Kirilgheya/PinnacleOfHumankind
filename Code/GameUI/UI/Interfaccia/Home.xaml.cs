using GameUI.UI.DataSource;
using GameUI.UI.DataSource.UIItems_DS;
using Gamecore = MainGame.Applicazione;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameUI
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        TreeViewStarSystems StarSystems_DS;

        Gamecore.DataModel.ChemicalComposition chemicalComposition;



        private Point _pointOnClick; // Click Position for panning
        private ScaleTransform _scaleTransform;


        private TranslateTransform _translateTransform;
        private TransformGroup _transformGroup;

        double? startZoom = null;

        public Home()
        {
            InitializeComponent();

            _translateTransform = new TranslateTransform();
            _scaleTransform = new ScaleTransform();
            _transformGroup = new TransformGroup();
            _transformGroup.Children.Add(_scaleTransform);
            _transformGroup.Children.Add(_translateTransform);
            backspace.RenderTransform = _transformGroup;



            StarSystems_DS = new TreeViewStarSystems();

            Applicazione.DataModel.PeriodicTable.init();

            List<Gamecore.DataModel.ChemicalElement> chemicalElements = Gamecore.Engine.DataEngine.starSeed;
            List<double> percentageList = new List<double>();

            percentageList = Gamecore.Engine.SimulationEngine.generateDistributionList(90, 70, chemicalElements.Count);

            chemicalComposition = new Gamecore.DataModel.ChemicalComposition(chemicalElements, percentageList);

            generate_Star_System();
        }

        private void generate_Star_System()
        {
            Gamecore.DataModel.StarSystem system = new Gamecore.DataModel.StarSystem();
            system.InitSystemParams(new Double[] { 1, Gamecore.ParametriUtente.Science.r_sun, 1 }, chemicalComposition);
            system.createStarSystem();


            StarSystems_DS.StarSystems.Clear();

            StarSystems_DS.StarSystems.Add(new StarSystem(system));

            system = new Gamecore.DataModel.StarSystem();
            system.InitSystemParams(new Double[] { 1, Gamecore.ParametriUtente.Science.r_sun * 3, 1 }, chemicalComposition);
            system.createStarSystem();


            StarSystems_DS.StarSystems.Add(new StarSystem(system));

            system = new Gamecore.DataModel.StarSystem();
            system.InitSystemParams(new Double[] { 1, Gamecore.ParametriUtente.Science.r_sun * 10.7, 1 }, chemicalComposition);
            system.createStarSystem();


            StarSystems_DS.StarSystems.Add(new StarSystem(system));


            system = new Gamecore.DataModel.StarSystem();
            system.InitSystemParams(new Double[] { 1, Gamecore.ParametriUtente.Science.r_sun * 0.9, 1 }, chemicalComposition);
            system.createStarSystem();


            StarSystems_DS.StarSystems.Add(new StarSystem(system));
        }

        private void TreeView_Loaded(object sender, RoutedEventArgs e)
        {
            // ... Create a TreeViewItem.
            TreeViewItem item = new TreeViewItem();
            item.Header = "Computer";
            item.ItemsSource = new string[] { "Monitor", "CPU", "Mouse" };

            // ... Create a second TreeViewItem.
            TreeViewItem item2 = new TreeViewItem();
            item2.Header = "Outfit";
            item2.ItemsSource = new string[] { "Pants", "Shirt", "Hat", "Socks" };

            // ... Get TreeView reference and add both items.
            var tree = sender as TreeView;
            tree.Items.Add(item);
            tree.Items.Add(item2);
        }

        private void TreeView_SelectedItemChanged(object sender,
            RoutedPropertyChangedEventArgs<object> e)
        {
            var tree = sender as TreeView;

            // ... Determine type of SelectedItem.
            if (tree.SelectedItem is TreeViewItem)
            {
                // ... Handle a TreeViewItem.
                var item = tree.SelectedItem as TreeViewItem;
                this.Title = "Selected header: " + item.Header.ToString();
            }
            else if (tree.SelectedItem is string)
            {
                // ... Handle a string.
                this.Title = "Selected: " + tree.SelectedItem.ToString();
            }
        }



        private void TestLoaded(object sender, RoutedEventArgs e)
        {
            // ... Create a TreeViewItem.

       
            this.StarSystemTreeView.DataContext = StarSystems_DS;

        }



        private void Btn_draw_click(object sender, RoutedEventArgs e)
        {
            double scale = 50;

            StarSystem s = StarSystems_DS.StarSystems[Int32.Parse(txt_index.Text.Trim()) -1 ];


            system_name.Content = s.Name;

            backspace.Children.Clear();

            List<Ellipse> ss = new List<Ellipse>();

            int n = 0;



            foreach (IBodyTreeViewItem body in s.Children)
            {
                double radious;
                try
                {
                    Star star = body as Star;

                    radious = (s.Children[n] as Star).relatedStar.relativeRadius;

                    if (star.relatedStar.isStarABlackHole())
                    {
                        ss.Add(new Ellipse() { Width = 10, Height = 10, Fill = star.getBrushFromStarColor(), Tag = star });
                    }
                    else
                    {
                        ss.Add(new Ellipse() { Width = radious * scale, Height = radious * scale, Fill = star.getBrushFromStarColor(), Tag = star });
                    }
                    backspace.Children.Add(ss[n]);

                    
                    Canvas.SetLeft(ss[n], 0);
                    Canvas.SetTop(ss[n], 100 * n);



                }
                catch(Exception exc)
                {
                    //non è una stella 

                    List<IBodyTreeViewItem> cose = (body as TreeElementPlanets).Children.ToList();

                    int m = 0;

                    foreach(IBodyTreeViewItem b in cose)
                    {
                        try
                        {
                           if(b is Planet)
                            {
                                radious = (b as Planet).relatedPlanet.relativeRadius;
                                ss.Add(new Ellipse() { Width = (radious * scale)/10, Height = (radious * scale)/10, Fill = Brushes.Blue, Tag = b });
                                backspace.Children.Add(ss[n+m]);

                                //Canvas.SetLeft(ss[n+m], (b as Planet).relatedPlanet.distance_from_star * scale * 4);
                                Canvas.SetLeft(ss[n + m], 100 * m + 1);
                                Canvas.SetTop(ss[n+m], 100 * n + 1 );

 

                                m++;
                            }
                        }
                        catch(Exception exc2)
                        {

                        }
                    }


                }
                n++;
            }


        }

        private void get_tooltip_from_Ellipse(Ellipse sender)
        {
            try
            {
                Star s = sender.Tag as Star;

                MessageBox.Show(s.relatedStar.ToString_Info());

            }
            catch (Exception exc)
            {

                Planet s = (sender as Ellipse).Tag as Planet;

                MessageBox.Show(s.relatedPlanet.ToString());

            }
           
        }

        private void MainCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {

            Point mousePosition = e.GetPosition(backspace);
            //Actual Zoom
            double zoomNow = Math.Round(backspace.RenderTransform.Value.M11, 1);

            if(startZoom == null)
            {
                startZoom = zoomNow;
            }

            //ZoomScale
            double zoomScale = 0.1;
            //Positive or negative zoom
            double valZoom = e.Delta > 0 ? zoomScale : -zoomScale;

            if(valZoom < 0 && zoomNow <= startZoom)
            {
                return;
            }

            Point pointOnMove = e.GetPosition((FrameworkElement)backspace.Parent);
            //RenderTransformOrigin (doesn't fully working)
            backspace.RenderTransformOrigin = new Point(mousePosition.X / backspace.ActualWidth, mousePosition.Y / backspace.ActualHeight);

            Zoom(new Point(mousePosition.X, mousePosition.Y), zoomNow + valZoom);
        }

        /// Zoom function
        private void Zoom(Point point, double scale)
        {

            double centerX = (point.X - _translateTransform.X) / _scaleTransform.ScaleX;
            double centerY = (point.Y - _translateTransform.Y) / _scaleTransform.ScaleY;

            _scaleTransform.ScaleX = scale;
            _scaleTransform.ScaleY = scale;

            _translateTransform.X = point.X - centerX * _scaleTransform.ScaleX;
            _translateTransform.Y = point.Y - centerY * _scaleTransform.ScaleY;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {




            ZoomViewbox.MaxWidth = 669;
            ZoomViewbox.MaxHeight = 835;

            ZoomViewbox.MinWidth = 669;
            ZoomViewbox.MinHeight = 835;

        }

        private void ScrollViewer_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
        }

        private void Btn_recreate_click(object sender, RoutedEventArgs e)
        {
            generate_Star_System();
        }


        private void MainCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is Ellipse)
            {
                get_tooltip_from_Ellipse(e.Source as Ellipse);

                return;
            }
            

            //Capture Mouse
            backspace.CaptureMouse();
            //Store click position relation to Parent of the canvas
            _pointOnClick = e.GetPosition((FrameworkElement)backspace.Parent);

            
        }

        private void MainCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Release Mouse Capture
            backspace.ReleaseMouseCapture();
            //Set cursor by default
            Mouse.OverrideCursor = null;


        }



        private void MainCanvas_MouseMove(object sender, MouseEventArgs e)
        {

            //Return if mouse is not captured
            if (!backspace.IsMouseCaptured) return;
            //Point on move from Parent
            Point pointOnMove = e.GetPosition((FrameworkElement)backspace.Parent);
            //set TranslateTransform
            _translateTransform.X = backspace.RenderTransform.Value.OffsetX - (_pointOnClick.X - pointOnMove.X);
            _translateTransform.Y = backspace.RenderTransform.Value.OffsetY - (_pointOnClick.Y - pointOnMove.Y);
            //Update pointOnClic
            _pointOnClick = e.GetPosition((FrameworkElement)backspace.Parent);
        }

    }
    
    
}
