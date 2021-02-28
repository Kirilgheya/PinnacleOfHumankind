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

        Matrix min_zoom;

        public Home()
        {
            InitializeComponent();


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
                        ss.Add(new Ellipse() { Width = 10, Height = 10, Fill = Brushes.Black, Tag = star });
                    }
                    else
                    {
                        ss.Add(new Ellipse() { Width = radious * scale, Height = radious * scale, Fill = Brushes.Red, Tag = star });
                    }
                    backspace.Children.Add(ss[n]);

                    

                    ss[n].MouseLeftButtonUp += Home_MouseLeftButtonUp;

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

                                ss[n+m].MouseLeftButtonUp += Home_MouseLeftButtonUp;

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

        private void Home_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Star s = (sender as Ellipse).Tag as Star;

                MessageBox.Show(s.relatedStar.ToString());
            }
            catch(Exception exc)
            {
                Planet s = (sender as Ellipse).Tag as Planet;

                MessageBox.Show(s.relatedPlanet.ToString());
            }
           
        }

        private void backspace_MouseWheel(object sender, MouseWheelEventArgs e)
        {
         

            var element = sender as UIElement;
            var position = e.GetPosition(element);
            var transform = element.RenderTransform as MatrixTransform;
            var matrix = transform.Matrix;

            if (min_zoom == null)
            {
                min_zoom = transform.Matrix; // min scale
            }


            var scale = e.Delta >= 0 ? 1.1 : (1.0 / 1.1); // choose appropriate scaling factor


            if (transform.Matrix.M11 <= min_zoom.M11 + 0.05 && scale < 1)
            {
                return;
            }

            matrix.ScaleAtPrepend(scale, scale, position.X, position.Y);

            transform.Matrix = matrix;
        }

        private void UpdateViewBox(int newValue)
        {
            if ((backspace.Width >= 0) && backspace.Height >= 0)
            {
                backspace.Width += newValue;
                backspace.Height += newValue;
            }
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

    }
    
    
}
