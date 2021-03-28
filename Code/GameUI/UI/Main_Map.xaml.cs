using GameUI.UI.DataSource;
using GameUI.UI.DataSource.UIItems_DS;
using GameUI.UI.GameEngine;
using GameUI.UI.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using Gamecore = MainGame.Applicazione;


namespace GameUI.UI
{




    /// <summary>
    /// Interaction logic for Main_Map.xaml
    /// </summary>
    public partial class Main_Map : Window
    {

      
        private List<StarSystem> System_List = GameSession.GameSessionSystems == null ? new List<StarSystem>() : GameSession.GameSessionSystems;

        public static StarSystem selected_SS = null;

        private double scale = 10;

        private double zoomScale = 1;

        private double horizontal_offset = 0;
        private double vertical_offset = 0;

        private Point _pointOnClick; // Click Position for panning
        private Point MouseLocation;
        
        private bool timePassing;


        DispatcherTimer timer = new DispatcherTimer();

        public Main_Map()
        {
            InitializeComponent();

            generate_Star_System();

            add_starSystem_to_Tree();

            timer.Tick += (s, ev) => btn_advance_time.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            timer.Interval = new TimeSpan(0, 0, 0,1,000);
        }

        private void generate_Star_System(Boolean _forceRecreate = false)
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

            if(System_List == null || System_List.Count == 0 || _forceRecreate)
            { 
                if(System_List == null)
                {

                    System_List = new List<StarSystem>();
                }

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
                            item.Header = (item.Tag as Star).Name + "X " + Math.Round((item.Tag as Star).position.X) + " Y " + Math.Round((item.Tag as Star).position.Y);
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
                            item.Header = (item.Tag as Planet).Name + "X " + Math.Round((item.Tag as Planet).position.X) + " Y " + Math.Round((item.Tag as Planet).position.Y);
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

        private void setPositionRelativeToCenter(Ellipse _shape, double _fromLeft, double _fromTop)
        {
            
            Canvas.SetLeft(_shape, (get_x_center() - _fromLeft));
            Canvas.SetTop(_shape, (get_y_center() - _fromTop));

        }

        public  void draw_system(StarSystem sy, int increment = 0)
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
            
            Canvas.SetLeft(centro, get_x_center() - centro.Width / 2 );
            Canvas.SetTop(centro, get_y_center() - centro.Height / 2);
            
            foreach (Star star in sy.Children.Where(x => x is Star).ToList())
            {
                double angolo = 360 / System_List.First().Children.Where(x => x is Star).ToList().Count();
                
                //Draw new stars

                Ellipse starShape = star.drawBody(scale);

                starShape.PreviewMouseLeftButtonDown += Ellipse_preview_mouse_left_click;
                cv_backspace.Children.Add(starShape);


                double leftPositionStar = starShape.Width / 2 - (selected_SS.relatedStarSystem.getDeltasFromBarycenter()[n] * 1 / scale) ;
                double topPositionStar = starShape.Width / 2 - (selected_SS.relatedStarSystem.getDeltasFromBarycenter()[n] * 1 / scale) ;

                this.setPositionRelativeToCenter(starShape, leftPositionStar, topPositionStar);
                
                //End Draw
                star.position = new Point(Canvas.GetLeft(starShape), Canvas.GetTop(starShape));

                lbl_delta.Content = lbl_delta.Content + "    " + selected_SS.relatedStarSystem.getDeltasFromBarycenter()[n];

                if (Canvas.GetLeft(starShape) > 0)
                {
                    Point center = new Point(get_x_center(), get_y_center());
                    Point starCoordinate = new Point(Canvas.GetLeft(starShape), Canvas.GetTop(starShape));

                    UIStaticClass.generateOrbitForBody(cv_backspace, starShape, center, starCoordinate
                                ,UIStaticClass.BrushFromHex("#e5e6c3"),star);

                }

                find_node(star.Name, true);

                n++;
            }

            Random rnd = new Random();

            foreach (Planet planet in sy.Children.Where(x => x is TreeElementPlanets).First().Children.Where(y => y is Planet).ToList())
            {

                double angolo = 0;
                Point originCoordPlanet = new Point();

                Ellipse planetShape = planet.drawBody(scale);

                planetShape.PreviewMouseLeftButtonDown += Ellipse_preview_mouse_left_click;

                if(!planet.hasMoved())
                {

                    UIStaticClass.ScatterPlanetsOnOrbit(new List<Planet>() { planet });
                }

                planet.advanceTime(-1, increment);

                angolo = planet.angleOnOrbit;

                originCoordPlanet.X = (get_x_center() - planetShape.Width / 2 - (planet.relatedPlanet.distance_from_star * 600 / scale));
                originCoordPlanet.Y = (get_y_center() - planetShape.Width / 2);

               

                if (originCoordPlanet.X > 0 && originCoordPlanet.Y > 0)
                {
                    cv_backspace.Children.Add(planetShape);

                    planet.setPosition(originCoordPlanet);

                    Point center = new Point(get_x_center(), get_y_center());
                  

                    double orbitRadius = UIStaticClass.generateOrbitForBody(cv_backspace, planetShape, center, originCoordPlanet, Brushes.Aqua, planet);
       
                    UIStaticClass.moveBodyOnOrbit(planet, UIStaticClass.DegreeToRadiants(angolo) , orbitRadius, new Point(center.X, center.Y),true);
                }
             


                find_node(planet.Name, true);

                n++;
            }

        }

        private void Ellipse_preview_mouse_left_click(object sender, MouseButtonEventArgs e)
        {
            //UIStaticClass.Show_body_info(sender);
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
            if ((SystemTree.SelectedItem as TreeViewItem).Tag is Planet)
            {
                try
                {
                    Planet selectedPlanet = (SystemTree.SelectedItem as TreeViewItem).Tag as Planet;
                    reset_pan();

                    if(selectedPlanet.position.X > (cv_backspace.Width / 2) )
                    {


                    }
                    else
                    {


                    }

                    horizontal_offset = cv_backspace.Width/4 + (cv_backspace.Width/4 - (
                                                                                        selectedPlanet.getShapeCenter().X
                                                                                        )
                                                               );
                    vertical_offset = cv_backspace.Width/4 + (cv_backspace.Height/4  - (
                                                                                        selectedPlanet.getShapeCenter().Y
                                                                                        ) 
                                                             );

                    draw_system(selected_SS);

                }
                catch (Exception exc)
                {

                }
            }
        }

        private void btn_recreate_Click(object sender, RoutedEventArgs e)
        {
            generate_Star_System(true);

            add_starSystem_to_Tree();
        }

        private void txt_scale_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_scale.Text != null && txt_scale.Text != String.Empty)
            {
                scale = Double.Parse(txt_scale.Text.Trim());

               
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

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            GameSession.GameSessionSystems = this.System_List;
            GameSession.saveGame();
        }

        private void ZoomViewbox_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {

                if(scale <= zoomScale)
                {

                    zoomScale = zoomScale / 10;
                }

                if ((scale - zoomScale < 1))
                {
                    return;
                }

                //zoomScale = zoomScale - 1000;
                scale = scale - zoomScale;
            }
            else
            {

                if(scale /10  == zoomScale )
                {

                    zoomScale = zoomScale * 10;
                }
                scale = scale + zoomScale;
                //zoomScale = zoomScale + 1000;
            }

            txt_scale.Text = scale.ToString();

            draw_system(selected_SS);
        }

        private void cv_backspace_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!(e.OriginalSource is Canvas))
            {
                UIStaticClass.Show_body_info(e.OriginalSource);
                return;
            }

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
            MouseLocation = new Point(e.GetPosition((FrameworkElement)cv_backspace).X, e.GetPosition((FrameworkElement)cv_backspace).Y);
            lbl_mouse_pos.Content = "X " + Math.Round(e.GetPosition((FrameworkElement)cv_backspace).X) + " Y " + Math.Round(e.GetPosition((FrameworkElement)cv_backspace).Y);

            //Return if mouse is not captured
            if (!cv_backspace.IsMouseCaptured) return;
            //Point on move from Parent
            Point pointOnMove = e.GetPosition((FrameworkElement)cv_backspace.Parent);
            //set TranslateTransform
            horizontal_offset -= (_pointOnClick.X - pointOnMove.X) ;
            vertical_offset -=  (_pointOnClick.Y - pointOnMove.Y);
            //Update pointOnClic
            _pointOnClick = e.GetPosition((FrameworkElement)cv_backspace.Parent);

            Console.WriteLine(horizontal_offset + " / " + vertical_offset);
     
            draw_system(selected_SS);
            
        }



        public double get_x_center()
        {
            return cv_backspace.Width / 2 + horizontal_offset /*/ (zoomScale / 100)*/;
        }

        public double get_y_center()
        {
            return cv_backspace.Height / 2 + vertical_offset/* / (zoomScale / 100)*/;
        }

        private void btn_reset_zoom_pan_Click(object sender, RoutedEventArgs e)
        {
            reset_pan();
        }

        private void reset_pan()
        {
            vertical_offset = horizontal_offset = 0;

            draw_system(selected_SS);
        }


        public void btn_advance_time_Click(object sender, RoutedEventArgs e)
        {

            UIStaticClass.AdvanceTimeStep(1);
            draw_system(selected_SS, + 1);

        }

        private void btn_play_time_Click(object sender, RoutedEventArgs e)
        {
            timePassing = !timePassing;

            if (timePassing)
            {
                timer.Start();
            }
            else
            {
                timer.Stop();
            }

           
           
        }

      

        private void CommonCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }



    }

   


}