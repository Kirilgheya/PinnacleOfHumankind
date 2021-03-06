﻿using GameUI.Artificial;
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

        double checkLocationX;
        double checkLocationY;
        double UADistance;
        double UnitLocation;

        private List<StarSystem> System_List = GameSessionHandler.GameSessionSystems == null ? new List<StarSystem>() : GameSessionHandler.GameSessionSystems;

        public static StarSystem selected_SS = null;

        private double scale_UAtoCanvasUnit;

        private double horizontal_offset = 0;
        private double vertical_offset = 0;

        private Point _pointOnClick; // Click Position for panning
        
        
        private bool timePassing;


        DispatcherTimer timer = new DispatcherTimer();

        public Main_Map()
        {
            InitializeComponent();

            generate_Star_System();

            add_starSystem_to_Tree();

            timer.Tick += (s, ev) => btn_advance_time.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            timer.Interval = new TimeSpan(0, 0, 0,1,000);

            GameSessionHandler.map = this;
        }

        internal void redrawSystem()
        {
            cv_backspace.Children.Clear();

            draw_system(selected_SS);
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
                    if (p.Children.First() is Planet)
                    {
                        foreach (Planet pl in p.Children)
                        {
                            SystemTree.Items.Cast<TreeViewItem>().ToList()[n].Items.Add(new TreeViewItem() { Header = pl.Name, Tag = pl });
                        }
                    }
                    if (GameSessionHandler.drawAsteroids)
                    {
                        if (p.Children.First() is Asteroid)
                        {
                            foreach (Asteroid ast in p.Children)
                            {
                                SystemTree.Items.Cast<TreeViewItem>().ToList()[n].Items.Add(new TreeViewItem() { Header = ast.Name, Tag = ast });
                            }
                        }
                    }
                    //break;
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

        public void draw_system(StarSystem sy)
        {
           
            
            
            Point center = new Point(get_x_center(), get_y_center());
            double furthestOrbit = 0;

            if (sy == null)
            {
                return;
            }

            int n = 0;

            lbl_delta.Content = "";

            createCenter();

            List<IBodyTreeViewItem> StarList = sy.Children.Where(x => x is Star).ToList();
            List<Planet> PlanetList = sy.Children.Where(x => x is TreeElementPlanets).First().Children.Where(y => y is Planet).Cast<Planet>().ToList<Planet>();

            furthestOrbit = PlanetList.Max<Planet>(x => x.relatedPlanet.distance_from_star);


            scale_UAtoCanvasUnit = 600;

            if ( (scale_UAtoCanvasUnit * furthestOrbit > cv_backspace.Width) || (scale_UAtoCanvasUnit * furthestOrbit > cv_backspace.Height))
            {

                

                this.scale_UAtoCanvasUnit *= Math.Min(1 / 
                                                    ( (scale_UAtoCanvasUnit * furthestOrbit) / cv_backspace.Width)
                                                ,1/
                                                   ( (scale_UAtoCanvasUnit  * furthestOrbit) / cv_backspace.Height));
               // Console.WriteLine("La scala è:"+this.scale_UAtoCanvasUnit);
               // Console.WriteLine("L'orbita più lontana è:" + scale_UAtoCanvasUnit * furthestOrbit);
            }
          
           

            foreach (Star star in StarList)
            {
                Ellipse starShape;

                starShape = UIStaticClass.RedrawStar(star,1, StarList.Count(), selected_SS.relatedStarSystem.getDeltasFromBarycenter()[n] * this.scale_UAtoCanvasUnit);


                Point origStarCoordinates = new Point();

                if (!star.hasMoved() )
                {
                    origStarCoordinates.X = (get_x_center() - starShape.Width / 2 - (selected_SS.relatedStarSystem.getDeltasFromBarycenter()[n] * this.scale_UAtoCanvasUnit ));

                    origStarCoordinates.Y = (get_y_center() - starShape.Width / 2 - (selected_SS.relatedStarSystem.getDeltasFromBarycenter()[n] * this.scale_UAtoCanvasUnit));


                    
                }
                else
                {

                    origStarCoordinates = star.position;
                }
                cv_backspace.Children.Add(starShape);
                star.setPosition(origStarCoordinates);

                Console.WriteLine(starShape.Width);

               // this.setPositionRelativeToCenter(starShape, origStarCoordinates.X, origStarCoordinates.Y);

              

                if (Canvas.GetLeft(starShape) > 0)
                {
                    //Point center = new Point(get_x_center(), get_y_center());
                   

                    if (StarList.Count() > 1)
                    {
                        double orbitRadius = UIStaticClass.generateOrbitForBody(cv_backspace, starShape, center, star.position
                                    , UIStaticClass.BrushFromHex("#e5e6c3"), star);



                    
                        if (StarList.Count() > 1 && !star.hasMoved())
                        {
                            UIStaticClass.ScatterBodiesOnOrbit(new List<IBodyTreeViewItem>() { star });
                            UIStaticClass.moveBodyOnOrbit(star, UIStaticClass.DegreeToRadiants(star.angleOnOrbit), orbitRadius, new Point(get_x_center(), get_y_center()), true);
                        }
                        //UIStaticClass.moveBodyOnOrbit(star,UIStaticClass.DegreeToRadiants(star.angleOnOrbit),orbitRadius,new Point(get_x_center(),get_y_center()),true);
                    }
                    

                   
                }

                find_node(star.Name, true);

                if (GameSessionHandler.selected.Contains(star))
                {
                    star.selected = true;
                }


                n++;
            }

            Random rnd = new Random();

          
            foreach (Planet planet in PlanetList)
            {

                double angolo = 0;
                Point originCoordPlanet = new Point();

                Ellipse planetShape = planet.drawBody();
             
                originCoordPlanet.X = (get_x_center() - planetShape.Width / 2 - (planet.relatedPlanet.distance_from_star * this.scale_UAtoCanvasUnit ));
                originCoordPlanet.Y = (get_y_center() - planetShape.Width / 2 - (planet.relatedPlanet.distance_from_star * this.scale_UAtoCanvasUnit ));

                checkLocationX = originCoordPlanet.X;
                checkLocationX = originCoordPlanet.Y;

              
                UADistance = planet.relatedPlanet.distance_from_star;

                cv_backspace.Children.Add(planetShape);

                planet.setPosition(originCoordPlanet);

                  
                double orbitRadius = UIStaticClass.generateOrbitForBody(cv_backspace, planetShape, center, originCoordPlanet, Brushes.Aqua, planet);

                find_node(planet.Name, true);

                if (!planet.hasMoved())
                {

                    UIStaticClass.ScatterBodiesOnOrbit(new List<IBodyTreeViewItem>() { planet });
                }
                UIStaticClass.moveBodyOnOrbit(planet, UIStaticClass.DegreeToRadiants(planet.angleOnOrbit), planet.orbitRadius, new Point(get_x_center(), get_y_center()), true);
                

                if (GameSessionHandler.selected.Contains(planet))
                {
                    planet.selected = true;
                }

                n++;
            }

            if (GameSessionHandler.drawAsteroids)
            {
                foreach (Asteroid Asteroid in sy.Children.Where(x => x is TreeElementPlanets).ToList()[1].Children.Where(y => y is Asteroid).ToList())
                {

                    double angolo = 0;
                    Point originCoordAsteroid = new Point();

                    Ellipse AsteroidShape = Asteroid.drawBody();

                    if (!Asteroid.hasMoved())
                    {

                       // UIStaticClass.ScatterBodiesOnOrbit(new List<IBodyTreeViewItem>() { Asteroid });
                    }

                    //Asteroid.advanceTime(-1, increment / Asteroid.relatedAsteroid.relativeRevolutionTime);

                    angolo = Asteroid.angleOnOrbit;


             
                    if (originCoordAsteroid.X > 0 && originCoordAsteroid.Y > 0)
                    {
                        cv_backspace.Children.Add(AsteroidShape);

                        Asteroid.setPosition(originCoordAsteroid);

                        double orbitRadius = UIStaticClass.generateOrbitForBody(cv_backspace, AsteroidShape, center, originCoordAsteroid, Brushes.Aqua, Asteroid);

                    }



                    find_node(Asteroid.Name, true);


                    if (GameSessionHandler.selected.Contains(Asteroid))
                    {
                        Asteroid.selected = true;
                    }

                    n++;
                }
            }

            Line line = new Line();

         

        }

        private void createCenter()
        {

            Ellipse centro = new Ellipse { Width = 2, Height = 2, Fill = Brushes.Red };
            cv_backspace.Children.Add(centro);

            Canvas.SetLeft(centro, get_x_center() - centro.Width / 2);
            Canvas.SetTop(centro, get_y_center() - centro.Height / 2);
        }

        private void draw_artificial(bool fromZoom, bool fromPan, int increment)
        {
            if (GameSessionHandler.artificialList.Count == 0)
            {
                Ship s = new Ship("Aurora");

                s.spawn( 700 , 300 );

                cv_backspace.Children.Add(s.Shape);

                GameSessionHandler.artificialList.Add(s);

                Ship s2 = new Ship("Gantritor");

                s2.spawn(600, 300);

                cv_backspace.Children.Add(s2.Shape);

                GameSessionHandler.artificialList.Add(s2);
            }
            else
            {
                foreach (artificialObj art in GameSessionHandler.artificialList)
                {
                    if (art is Ship)
                    {
                        if (increment > 0)
                        {
                            (art as Ship).moveToDestination();
                            art.ShipInfoP.LoadInfo(art as Ship);
                        }
                        if (fromPan)
                        {
                            (art as Ship).redrawPan(horizontal_offset, vertical_offset);
                        }

                        cv_backspace.Children.Remove((art as Ship).Shape);
                        cv_backspace.Children.Add((art as Ship).Shape);

                        txtShip.Text = (art as Ship).position.X + " " + (art as Ship).position.Y;
                    }
                }
            }
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

                redrawSystem();
                

            }
            if ((SystemTree.SelectedItem as TreeViewItem).Tag is Planet)
            {
                try
                {
                    Planet selectedPlanet = (SystemTree.SelectedItem as TreeViewItem).Tag as Planet;
                    reset_pan();

                 
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
            if ((SystemTree.SelectedItem as TreeViewItem).Tag is Star)
            {

                try
                {
                    Star selectedPlanet = (SystemTree.SelectedItem as TreeViewItem).Tag as Star;
                    reset_pan();

                  
                    horizontal_offset = cv_backspace.Width / 4 + (cv_backspace.Width / 4 - (
                                                                                        selectedPlanet.getShapeCenter().X
                                                                                        )
                                                               );
                    vertical_offset = cv_backspace.Width / 4 + (cv_backspace.Height / 4 - (
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

            GameSessionHandler.artificialList = new List<artificialObj>();
        }

        private void txt_scale_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_scale.Text != null && txt_scale.Text != String.Empty)
            {
               

               
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txt_scale.Text != null && txt_scale.Text != String.Empty)
            {
                

                draw_system(selected_SS);
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            GameSessionHandler.GameSessionSystems = this.System_List;
            GameSessionHandler.saveGame();
        }

        // Zoom
        private Double zoomMax = 50;
        private Double zoomMin = 1;
        private Double zoomSpeed = 0.001;
        private Double zoom = 1;

       

        // Zoom on Mouse wheel
        private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            zoom += zoomSpeed * e.Delta; // Ajust zooming speed (e.Delta = Mouse spin value )
            if (zoom < zoomMin) { zoom = zoomMin; } // Limit Min Scale
            //if (zoom > zoomMax) { zoom = zoomMax; } // Limit Max Scale

            Point mousePos = e.GetPosition(cv_backspace);
        
            if (zoom > 1)
            {

                
                cv_backspace.RenderTransform = new ScaleTransform(zoom, zoom, mousePos.X, mousePos.Y); // transform Canvas size from mouse position
            }
            else
            {
                cv_backspace.RenderTransform = new ScaleTransform(zoom, zoom, mousePos.X, mousePos.Y); // transform Canvas size
            }
        }

        private void cv_backspace_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!(e.OriginalSource is Canvas))
            {
             
                if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                {

                    if (UIStaticClass.Show_body_info(e.OriginalSource))
                    {
                        
                    }
                    return;
                }
                else
                {
                    if (e.OriginalSource is Ellipse)
                    {
                        GameSessionHandler.UpdateSelected((e.OriginalSource as Ellipse).Tag as IBodyTreeViewItem);
                    }
                    if(e.OriginalSource is Path)
                    {
                        GameSessionHandler.UpdateSelected((e.OriginalSource as Path).Tag as IBodyTreeViewItem);
                    }

                }
            }
            else
            {

                //Capture Mouse
                cv_backspace.CaptureMouse();
                //Store click position relation to Parent of the canvas
                _pointOnClick = e.GetPosition((FrameworkElement)cv_backspace.Parent);
            }
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

            Point pointOnMove = e.GetPosition((FrameworkElement)cv_backspace.Parent);

            //Return if mouse is not captured
            if (cv_backspace.IsMouseCaptured)
            {

                horizontal_offset -= (_pointOnClick.X - pointOnMove.X);
                vertical_offset -= (_pointOnClick.Y - pointOnMove.Y);

                foreach (UIElement element in cv_backspace.Children)
                {

                    if(element is Shape)
                    {

                        Shape bodyShape = element as Shape;

                        TranslateTransform translate = bodyShape.RenderTransform as TranslateTransform;


                        translate = new TranslateTransform(horizontal_offset, vertical_offset);
                        if(bodyShape.Tag is Planet)
                        { 
                            find_node((bodyShape.Tag as Planet).Name, true);
                       
                        }

                       

                        bodyShape.RenderTransform = translate;
                    }
                }
            }
            //Point on move from Parent
       
            //set TranslateTransform
           
            //Update pointOnClic
            _pointOnClick = e.GetPosition((FrameworkElement)cv_backspace.Parent);

            //Console.WriteLine(horizontal_offset + " / " + vertical_offset);
     
            // draw_system(selected_SS,0,false,true);
            
        }



        public double get_x_center()
        {
            return cv_backspace.Width / 2;// + horizontal_offset /*/ (zoomScale / 100)*/;
        }

        public double get_y_center()
        {
            return cv_backspace.Height / 2;// + vertical_offset/* / (zoomScale / 100)*/;
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


            List<Star> StarList = selected_SS.Children.Where(x => x is Star).Cast<Star>().ToList<Star>();
            List<Planet> PlanetList = selected_SS.Children.Where(x => x is TreeElementPlanets).First().Children.Where(y => y is Planet).Cast<Planet>().ToList<Planet>();

            foreach(Star star in StarList)
            {

                if (Canvas.GetLeft(star.bodyShape) > 0)
                {
                    //Point center = new Point(get_x_center(), get_y_center());


                    if (StarList.Count() > 1)
                    {
                        //Console.WriteLine("PreAdvanceTime:" + star.Name + " " + star.angleOnOrbit + "° -" + star.position);
                        star.advanceTime(-1, 1);
                        UIStaticClass.moveBodyOnOrbit(star, star.angleOnOrbit, star.orbitRadius, new Point(get_x_center(), get_y_center()), true);
                        //Console.WriteLine("PostAdvanceTime:"+star.Name + " " + star.angleOnOrbit + "° -" + star.position);
                    }

                }
            }

            foreach (Planet planet in PlanetList)
            {

                
                planet.advanceTime(-1, 1 );
                /// planet.relatedPlanet.relativeRevolutionTime
       
                UIStaticClass.moveBodyOnOrbit(planet, UIStaticClass.DegreeToRadiants(planet.angleOnOrbit), planet.orbitRadius, new Point(get_x_center(), get_y_center()), true);
                
                find_node(planet.Name, true);
            }

            draw_artificial(false,false,1);

            //draw_system(selected_SS, + 1);

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

        private void cv_backspace_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            foreach (artificialObj art in GameSessionHandler.selected)
            {
                if(art is Ship)
                {
                    (art as Ship).destination = new Point(e.GetPosition(cv_backspace).X, e.GetPosition(cv_backspace).Y);
                }
            }
        }

        private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("no");
        }

        private void RaceCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void RaceCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Race");
        }

        private void EmpireCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void EmpireCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Empire");
        }

        private void ScienceCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ScienceCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TreeVisualizer tv = new TreeVisualizer();
            tv.Show();
        }

        private void LogCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void LogCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Log");
        }

        private void PlanetCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void PlanetCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Planet");
        }

        private void ProjectCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ProjectCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Project");
        }

        private void MarketCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void MarketCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Market");
        }

        private void SettingsCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SettingsCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Settings");
        }

        private void DiplomacyCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void DiplomacyCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Diplomacy");
        }
    }

    public static class CustomCommands
    {
        public static readonly RoutedUICommand Exit = new RoutedUICommand
            (
                "Exit",
                "Exit",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.F4, ModifierKeys.Alt)
                }
            );

        public static readonly RoutedUICommand Race = new RoutedUICommand
          (
              "Race",
              "Race",
              typeof(CustomCommands),
              new InputGestureCollection()
              {
                    new KeyGesture(Key.F4, ModifierKeys.Alt)
              }
          );

        public static readonly RoutedUICommand Empire = new RoutedUICommand
          (
              "Empire",
              "Empire",
              typeof(CustomCommands),
              new InputGestureCollection()
              {
                    new KeyGesture(Key.F4, ModifierKeys.Alt)
              }
          );

        public static readonly RoutedUICommand Science = new RoutedUICommand
          (
              "Science",
              "Science",
              typeof(CustomCommands),
              new InputGestureCollection()
              {
                    new KeyGesture(Key.F4, ModifierKeys.Alt)
              }
          );

        public static readonly RoutedUICommand Log = new RoutedUICommand
          (
              "Log",
              "Log",
              typeof(CustomCommands),
              new InputGestureCollection()
              {
                    new KeyGesture(Key.F4, ModifierKeys.Alt)
              }
          );

        public static readonly RoutedUICommand Settings = new RoutedUICommand
          (
              "Settings",
              "Settings",
              typeof(CustomCommands),
              new InputGestureCollection()
              {
                    new KeyGesture(Key.F4, ModifierKeys.Alt)
              }
          );

        public static readonly RoutedUICommand Planet = new RoutedUICommand
          (
              "Planet",
              "Planet",
              typeof(CustomCommands),
              new InputGestureCollection()
              {
                    new KeyGesture(Key.F4, ModifierKeys.Alt)
              }
          );

        public static readonly RoutedUICommand Project = new RoutedUICommand
          (
              "Project",
              "Project",
              typeof(CustomCommands),
              new InputGestureCollection()
              {
                    new KeyGesture(Key.F4, ModifierKeys.Alt)
              }
          );

        public static readonly RoutedUICommand Market = new RoutedUICommand
          (
              "Market",
              "Market",
              typeof(CustomCommands),
              new InputGestureCollection()
              {
                    new KeyGesture(Key.F4, ModifierKeys.Alt)
              }
          );

        public static readonly RoutedUICommand Diplomacy = new RoutedUICommand
          (
              "Diplomacy",
              "Diplomacy",
              typeof(CustomCommands),
              new InputGestureCollection()
              {
                    new KeyGesture(Key.F4, ModifierKeys.Alt)
              }
          );
        //Define more commands here, just like the one above
    }


}