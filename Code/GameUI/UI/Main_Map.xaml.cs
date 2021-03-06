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
using System.Windows.Shapes;

using GameUI.UI.DataSource;
using GameUI.UI.DataSource.UIItems_DS;
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

        double scale;


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

    

        }

        private void add_starSystem_to_Tree()
        {
            SystemTree.Items.Clear();

            int n = 0;
            int m = 0;
            foreach (StarSystem sys in System_List)
            {
                SystemTree.Items.Add(new TreeViewItem() { Header = sys.Name, Tag = sys });

                foreach(Star s in sys.Children.Where(x => x is Star).ToList())
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

            foreach (TreeViewItem item in SystemTree.Items)
            {
                if (item.Tag is Star)
                {
                    if ((item.Tag as Star).relatedStar.FullName == nodeToFind)
                    {
                        (item.Parent as TreeViewItem).IsExpanded = true;
                        item.IsSelected = true;
                        break;
                    }
                }
                else if (item.Tag is Planet)
                {
                    if ((item.Tag as Planet).relatedPlanet.name == nodeToFind)
                    {
                        (item.Parent as TreeViewItem).IsExpanded = true;
                        item.IsSelected = true;
                        break;
                    }
                }

                if (item.HasItems)
                {
                    findNode(item, nodeToFind);
                }
            }
        }

        public void findNode(TreeViewItem parent, string name)
        {
            foreach (TreeViewItem item in parent.Items)
            {
                if (item.Tag is Star)
                {
                    if ((item.Tag as Star).relatedStar.FullName == name)
                    {

                        (item.Parent as TreeViewItem).IsExpanded = true;
                        item.IsSelected = true;
                        break;
                    }
                }
                else if (item.Tag is Planet)
                {
                    if ((item.Tag as Planet).relatedPlanet.name == name)
                    {

                        (item.Parent as TreeViewItem).IsExpanded = true;
                        item.IsSelected = true;
                        break;
                    }
                }

                if (item.HasItems)
                {
                    findNode(item, name);
                }
            }
        }

        private void draw_system(StarSystem sy)
        {

            cv_backspace.Children.Clear();

            if(sy == null)
            {
                return;
            }

            int n = 0;

            lbl_delta.Content = "";

            Ellipse centro = new Ellipse { Width = 10, Height = 10, Fill = Brushes.Red };
            cv_backspace.Children.Add(centro);
            Canvas.SetLeft(centro, cv_backspace.Width / 2 - centro.Width / 2);
            Canvas.SetTop(centro, cv_backspace.Height / 2 - centro.Height / 2);


            foreach (Star s in sy.Children.Where(x => x is Star).ToList())
            {
                double angolo = 360 / System_List.First().Children.Where(x => x is Star).ToList().Count();

                Ellipse el = new Ellipse { Width = 10, Height = 10, Fill = Brushes.White };
                cv_backspace.Children.Add(el);

                lbl_delta.Content = lbl_delta.Content + "    " + selected_SS.relatedStarSystem.getDeltasFromBarycenter()[n];

             
                Canvas.SetLeft(el, (cv_backspace.Width/2 - el.Width / 2 - (selected_SS.relatedStarSystem.getDeltasFromBarycenter()[n] * 1/scale))) ;
                Canvas.SetTop(el, (cv_backspace.Width / 2 - el.Height / 2 - (selected_SS.relatedStarSystem.getDeltasFromBarycenter()[n] * 1/scale)));


                double debug = selected_SS.relatedStarSystem.getDeltasFromBarycenter()[n] * 1 / scale;


                    Ellipse orbit = new Ellipse
                    {
                        Stroke = Brushes.Yellow,
                        StrokeThickness = 4,
                        Width = Double.Parse(txt_orbit.Text.Trim()),
                        Height = Double.Parse(txt_orbit.Text.Trim()),
                        Fill = Brushes.Transparent
                    };

                    cv_backspace.Children.Add(orbit);
                    Canvas.SetLeft(orbit, cv_backspace.Width / 2 - orbit.Width / 2);
                    Canvas.SetTop(orbit, cv_backspace.Width / 2 - orbit.Height / 2);


                

                n++;
            }
                
        }


        public double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        private void SystemTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if(SystemTree.SelectedItem == null)
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
    }
}
