using GameUI.UI.DataSource;
using GameUI.UI.DataSource.UIItems_DS;
using GameUI.UI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Gamecore = MainGame.Applicazione;

namespace GameUI.UI
{
    /// <summary>
    /// Interaction logic for Main_Map.xaml
    /// </summary>
    public partial class Main_Map : Window
    {

        List<StarSystem> System_List = new List<StarSystem>();

        StarSystem selected_SS = null;

        double scale = 1;

        double zoomScale = 1000;

        double horizontal_offset = 0;
        double vertical_offset = 0;

        private Point _pointOnClick; // Click Position for panning


        public Main_Map()
        {
            InitializeComponent();

            generate_Star_System();

            add_starSystem_to_Tree();



        }


        private void generate_Star_System()
        {

            Applicazione.DataModel.PeriodicTable.init();

            List<Gamecore.DataModel.ChemicalElement> chemicalElements = Gamecore.Engine.DataEngine.starSeed;
            List<double> percentageList = new List<double>();

            percentageList = Gamecore.Engine.SimulationEngine.generateDistributionList(90, 70, chemicalElements.Count);

            Gamecore.DataModel.ChemicalComposition chemicalComposition;

            chemicalComposition = new Gamecore.DataModel.ChemicalComposition(chemicalElements, percentageList);

            Gamecore.DataModel.StarSystem system = new Gamecore.DataModel.StarSystem();
            system.InitSystemParams(new Double[] { 1, Gamecore.ParametriUtente.Science.r_sun, 1 }, chemicalComposition);
            system.createStarSystem();


            System_List.Clear();

            System_List.Add(new StarSystem(system));

            system = new Gamecore.DataModel.StarSystem();
            system.InitSystemParams(new Double[] { 1, Gamecore.ParametriUtente.Science.r_sun * 3, 5 }, chemicalComposition);
            system.createStarSystem();


            System_List.Add(new StarSystem(system));

            system = new Gamecore.DataModel.StarSystem();
            system.InitSystemParams(new Double[] { 1, Gamecore.ParametriUtente.Science.r_sun * 10.7, 35 }, chemicalComposition);
            system.createStarSystem();


            System_List.Add(new StarSystem(system));


            system = new Gamecore.DataModel.StarSystem();
            system.InitSystemParams(new Double[] { 1, Gamecore.ParametriUtente.Science.r_sun * 0.9, 0.5 }, chemicalComposition);
            system.createStarSystem();



            System_List.Add(new StarSystem(system));

        }

        private void add_starSystem_to_Tree()
        {
            SystemTree.Items.Clear();

            int n = 0;
            int m = 0;
            foreach (StarSystem sys in System_List)
            {
                SystemTree.Items.Add(new TreeViewItem() { Header = sys.Name, Tag = sys });

                foreach (Star s in sys.Children.Where(x => x is Star).ToList())
                {
                    SystemTree.Items.Cast<TreeViewItem>().ToList()[n].Items.Add(new TreeViewItem() { Header = s.Name, Tag = s });

                }

                foreach (TreeElementPlanets p in sys.Children.Where(x => x is TreeElementPlanets).ToList())
                {
                    foreach (Planet pl in p.Children)
                    {
                        SystemTree.Items.Cast<TreeViewItem>().ToList()[n].Items.Add(new TreeViewItem() { Header = pl.Name, Tag = pl });

                    }
                    break;
                }
                n++;
            }

        }

        private void txt_search_TextChanged(object sender, TextChangedEventArgs e)
        {


            string nodeToFind = txt_search.Text.Trim();

            find_node(nodeToFind);
        }

        private void find_node(string nodeToFind, bool update = false)
        {
            foreach (TreeViewItem item in SystemTree.Items)
            {
                if (item.Tag is Star)
                {
                    if ((item.Tag as Star).relatedStar.FullName == nodeToFind)
                    {
                        if (update)
                        {
                            item.Header = (item.Tag as Star).Name + "X " + Math.Round((item.Tag as Star).position.X )+ " Y " + Math.Round((item.Tag as Star).position.Y);
                        }
                        else
                        {
                            (item.Parent as TreeViewItem).IsExpanded = true;
                            item.IsSelected = true;
                            break;
                        }
                    }
                }
                else if (item.Tag is Planet)
                {
                    if ((item.Tag as Planet).relatedPlanet.name == nodeToFind)
                    {
                        if (update)
                        {
                            item.Header = (item.Tag as Planet).Name + "X " + Math.Round((item.Tag as Planet).position.X )+ " Y " + Math.Round((item.Tag as Planet).position.Y);
                        }
                        else
                        {
                            (item.Parent as TreeViewItem).IsExpanded = true;
                            item.IsSelected = true;
                            break;
                        }
                    }
                }

                if (item.HasItems)
                {
                    find_node_internal(item, nodeToFind, update);
                }
            }
        }

        public void find_node_internal(TreeViewItem parent, string name, bool update = false)
        {
            foreach (TreeViewItem item in parent.Items)
            {
                if (item.Tag is Star)
                {
                    if ((item.Tag as Star).relatedStar.FullName == name)
                    {

                        if (update)
                        {
                            item.Header = (item.Tag as Star).Name + " X " + Math.Round((item.Tag as Star).position.X) + " Y " + Math.Round((item.Tag as Star).position.Y);
                        }
                        else
                        {

                            (item.Parent as TreeViewItem).IsExpanded = true;
                            item.IsSelected = true;
                            break;
                        }
                    }
                }
                else if (item.Tag is Planet)
                {
                    if ((item.Tag as Planet).relatedPlanet.name == name)
                    {
                        if (update)
                        {
                            item.Header = (item.Tag as Planet).Name + " X " + Math.Round((item.Tag as Planet).position.X) + " Y " + Math.Round((item.Tag as Planet).position.Y);
                        }
                        else
                        {
                            (item.Parent as TreeViewItem).IsExpanded = true;
                            item.IsSelected = true;
                            break;
                        }
                    }
                }

                if (item.HasItems)
                {
                    find_node_internal(item, name);
                }
            }
        }

        private void draw_system(StarSystem sy)
        {

            cv_backspace.Children.Clear();

            if (sy == null)
            {
                return;
            }

            int n = 0;

            lbl_delta.Content = "";

            Ellipse centro = new Ellipse { Width = 2, Height = 2, Fill = Brushes.Red };
            cv_backspace.Children.Add(centro);
            Canvas.SetLeft(centro, get_x_center() - centro.Width / 2);
            Canvas.SetTop(centro, get_y_center() - centro.Height / 2);

            Console.WriteLine("|||||||||||||");
            foreach (Star s in sy.Children.Where(x => x is Star).ToList())
            {
                double angolo = 360 / System_List.First().Children.Where(x => x is Star).ToList().Count();

                Ellipse el = new Ellipse { Width = 10, Height = 10, Fill = Brushes.White };

                el.Tag = s.relatedStar;

                el.PreviewMouseLeftButtonDown += Ellipse_preview_mouse_left_click;

                cv_backspace.Children.Add(el);

                lbl_delta.Content = lbl_delta.Content + "    " + selected_SS.relatedStarSystem.getDeltasFromBarycenter()[n];


                Canvas.SetLeft(el, (get_x_center() - el.Width / 2 - (selected_SS.relatedStarSystem.getDeltasFromBarycenter()[n] * 1 / scale)));
                Canvas.SetTop(el, (get_y_center()));

                s.position = new Point(Canvas.GetLeft(el), Canvas.GetTop(el));


                double debug = selected_SS.relatedStarSystem.getDeltasFromBarycenter()[n] * 1 / scale;



                double length = 0;
                if (Canvas.GetLeft(el) > 0)
                {
                    Point p1 = new Point(get_x_center(), get_y_center());
                    Point p2 = new Point(Canvas.GetLeft(el), Canvas.GetTop(el));
                    length = Point.Subtract(p1, p2).Length;

                    Path orbitPath = new Path
                    {
                        Stroke = Brushes.Yellow,
                        StrokeThickness = 2,
                        Fill = Brushes.Transparent
                        
                    };

                    EllipseGeometry eg = new EllipseGeometry();
                    eg.Center = p1;
                    eg.RadiusX = length - (el.Width / 2);
                    eg.RadiusY = length - (el.Width / 2);

                    // Add all the geometries to a GeometryGroup.  
                    GeometryGroup orbitGroup = new GeometryGroup();
                    orbitGroup.Children.Add(eg);

                    // Set Path.Data  
                    orbitPath.Data = orbitGroup;


                    Line line = new Line();
                    line.X1 = p1.X;
                    line.X2 = p2.X;
                    line.Y1 = p1.Y;
                    line.Y2 = p2.Y;

                    cv_backspace.Children.Add(orbitPath);
                    cv_backspace.Children.Add(line);
                    // Canvas.SetLeft(orbit, get_x_center - orbit.Width / 2);
                    //  Canvas.SetTop(orbit, get_x_center - orbit.Height / 2);


                }

                Console.WriteLine("---------");
                Console.WriteLine(Canvas.GetLeft(el) + " \\ " + Canvas.GetTop(el) + " \\// " + length);


                find_node(s.Name, true);

                n++;
            }

            foreach (Planet s in sy.Children.Where(x => x is TreeElementPlanets).First().Children.Where(y => y is Planet).ToList())
            {
                double angolo = 360 / System_List.First().Children.Where(x => x is Star).ToList().Count();

                Ellipse el = new Ellipse { Width = 10, Height = 10, Fill = Brushes.Blue };

                el.Tag = s.relatedPlanet;

                el.PreviewMouseLeftButtonDown += Ellipse_preview_mouse_left_click;

                cv_backspace.Children.Add(el);

                Canvas.SetLeft(el, (get_x_center() - el.Width / 2 - (s.relatedPlanet.distance_from_star * 600/ scale)));
                Canvas.SetTop(el, (get_y_center()));

                s.position = new Point(Canvas.GetLeft(el), Canvas.GetTop(el));

                
           

                double length = 0;
                if (Canvas.GetLeft(el) > 0)
                {
                    Point p1 = new Point(get_x_center(), get_y_center());
                    Point p2 = new Point(Canvas.GetLeft(el), Canvas.GetTop(el));
                    length = Point.Subtract(p1, p2).Length;

                    Path orbitPath = new Path
                    {
                        Stroke = Brushes.LightBlue,
                        StrokeThickness = 2,
                        Fill = Brushes.Transparent

                    };

                    EllipseGeometry eg = new EllipseGeometry();
                    eg.Center = p1;
                    eg.RadiusX = length - (el.Width / 2);
                    eg.RadiusY = length - (el.Width / 2);

                    // Add all the geometries to a GeometryGroup.  
                    GeometryGroup orbitGroup = new GeometryGroup();
                    orbitGroup.Children.Add(eg);

                    // Set Path.Data  
                    orbitPath.Data = orbitGroup;


                    Line line = new Line();
                    line.X1 = p1.X;
                    line.X2 = p2.X;
                    line.Y1 = p1.Y;
                    line.Y2 = p2.Y;

                    cv_backspace.Children.Add(orbitPath);
                    cv_backspace.Children.Add(line);
                }

                Console.WriteLine("---------");
                Console.WriteLine(Canvas.GetLeft(el) + " \\ " + Canvas.GetTop(el) + " \\// " + length);


                find_node(s.Name, true);

                n++;
            }
        }

        private void Ellipse_preview_mouse_left_click(object sender, MouseButtonEventArgs e)
        {
            UIStaticClass.Show_body_info(sender);
        }

        public double ConvertToRadiants(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        private void SystemTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (SystemTree.SelectedItem == null)
            {
                return;
            }

            if ((SystemTree.SelectedItem as TreeViewItem).Tag is StarSystem)
            {
                selected_SS = (SystemTree.SelectedItem as TreeViewItem).Tag as StarSystem;
                draw_system(selected_SS);
            }
        }

        private void btn_recreate_Click(object sender, RoutedEventArgs e)
        {
            generate_Star_System();

            add_starSystem_to_Tree();
        }

        private void txt_scale_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_scale.Text != null && txt_scale.Text != String.Empty)
            {
                scale = Double.Parse(txt_scale.Text.Trim());

                draw_system(selected_SS);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txt_scale.Text != null && txt_scale.Text != String.Empty)
            {
                scale = Double.Parse(txt_scale.Text.Trim());

                draw_system(selected_SS);
            }
        }

        private void ZoomViewbox_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (scale - zoomScale < 0)
                {
                    return;
                }

                //zoomScale = zoomScale - 1000;
                scale = scale - zoomScale;

            }
            else
            {

                scale = scale + zoomScale;
                //zoomScale = zoomScale + 1000;

            }

            txt_scale.Text = scale.ToString();
        }

        private void cv_backspace_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {


            //Capture Mouse
            cv_backspace.CaptureMouse();
            //Store click position relation to Parent of the canvas
            _pointOnClick = e.GetPosition((FrameworkElement)cv_backspace.Parent);
        }

        private void cv_backspace_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Release Mouse Capture
            cv_backspace.ReleaseMouseCapture();
            //Set cursor by default
            Mouse.OverrideCursor = null;



        }

        private void cv_backspace_MouseMove(object sender, MouseEventArgs e)
        {
            lbl_mouse_pos.Content = "X " + Math.Round(e.GetPosition((FrameworkElement)cv_backspace).X) + " Y " + Math.Round(e.GetPosition((FrameworkElement)cv_backspace).Y);

            //Return if mouse is not captured
            if (!cv_backspace.IsMouseCaptured) return;
            //Point on move from Parent
            Point pointOnMove = e.GetPosition((FrameworkElement)cv_backspace.Parent);
            //set TranslateTransform
            horizontal_offset = horizontal_offset + (_pointOnClick.X - pointOnMove.X);
            vertical_offset = vertical_offset + (_pointOnClick.Y - pointOnMove.Y);
            //Update pointOnClic
            _pointOnClick = e.GetPosition((FrameworkElement)cv_backspace.Parent);

            draw_system(selected_SS);

        }


        public double get_x_center()
        {
            return cv_backspace.Width / 2 - horizontal_offset / (zoomScale / 1000);
        }

        public double get_y_center()
        {
            return cv_backspace.Height / 2 - vertical_offset / (zoomScale / 1000);
        }

        private void btn_reset_zoom_pan_Click(object sender, RoutedEventArgs e)
        {
            vertical_offset = horizontal_offset = 0;

            draw_system(selected_SS);

        }
    }
}
