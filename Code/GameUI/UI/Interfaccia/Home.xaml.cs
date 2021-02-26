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
    public partial class Home : Page
    {
        TreeViewStarSystems StarSystems_DS;
        public Home()
        {
            InitializeComponent();


            StarSystems_DS = new TreeViewStarSystems();

            Applicazione.DataModel.PeriodicTable.init();

            List<Gamecore.DataModel.ChemicalElement> chemicalElements = Gamecore.Engine.DataEngine.starSeed;
            List<double> percentageList = new List<double>();

            percentageList = Gamecore.Engine.SimulationEngine.generateDistributionList(90, 70, chemicalElements.Count);


            Gamecore.DataModel.ChemicalComposition chemicalComposition = new Gamecore.DataModel.ChemicalComposition(chemicalElements, percentageList);
            Gamecore.DataModel.StarSystem system = new Gamecore.DataModel.StarSystem();
            system.InitSystemParams(new Double[] { 1, Gamecore.ParametriUtente.Science.r_sun, 1 }, chemicalComposition);
            system.createStarSystem();


       
          
            StarSystems_DS.StarSystems.Add(new StarSystem(system));

            system = new Gamecore.DataModel.StarSystem();
            system.InitSystemParams(new Double[] { 1, Gamecore.ParametriUtente.Science.r_sun*3, 1 }, chemicalComposition);
            system.createStarSystem();

      
            StarSystems_DS.StarSystems.Add(new StarSystem(system));

            StarSystems_DS.StarSystems.Add(new StarSystem(system));

            system = new Gamecore.DataModel.StarSystem();
            system.InitSystemParams(new Double[] { 1, Gamecore.ParametriUtente.Science.r_sun * 10.7, 1 }, chemicalComposition);
            system.createStarSystem();


            StarSystems_DS.StarSystems.Add(new StarSystem(system));


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
    }
}
