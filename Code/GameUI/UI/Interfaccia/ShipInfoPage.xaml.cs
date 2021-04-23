using GameUI.Artificial;
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
using static GameUI.Artificial.Ship;

namespace GameUI.UI.Interfaccia
{
    /// <summary>
    /// Interaction logic for ShipInfoPage.xaml
    /// </summary>
    public partial class ShipInfoPage : Window
    {

        public Ship ship;

        public ShipInfoPage()
        {
            InitializeComponent();
        }

        public void LoadInfo(Ship s)
        {
            cv_back.Children.Clear();

            Ellipse el = new Ellipse();

            el.Width = 200;
            el.Height = 200;
            el.Fill = s.Shape.Fill;

            cv_back.Children.Add(el);

            txtInfo.Text = s.name + " HP " + s.totalHP + "\n SPEED " + s.speed + "\n FIREPOWER " + s.totalFirePower +
                "\n POSITION    X = " + Math.Round(s.position.X, 0) + " Y = " + Math.Round(s.position.Y, 0) +
                "\n DESTINATION X = " + Math.Round(s.destination.X,0)  + " Y = " + Math.Round(s.destination.Y,0); 

         



            foreach (ComponentsEnumerator sc in s.ShipsComponentsList)
            {
                txtInfo.Text += "\n " + sc.comp.Name + " TOTAL " + sc.totali + " ACTIVE " + sc.attivi;
            }

            shipBluePrint.RowDefinitions.Clear();
            shipBluePrint.ColumnDefinitions.Clear();
            shipBluePrint.Children.Clear();

            foreach(List<ShipSector> Lsec in s.Structure)
            {
                shipBluePrint.ColumnDefinitions.Add(new ColumnDefinition());             
                foreach (ShipSector sec in Lsec)
                {
                    shipBluePrint.RowDefinitions.Add(new RowDefinition());

                    Button btn = new Button();
                    btn.Content = sec.x.ToString() + " " + sec.y.ToString() + " " + sec.SectorComponents.First().Name + " HP " + sec.HP;

                    if(sec.SectorComponents.Where(x => x.active).Count() == 0)
                    {
                        btn.Background = new SolidColorBrush(Colors.Red);
                    }

                    shipBluePrint.Children.Add(btn);

                    Grid.SetColumn(btn, sec.x);
                    Grid.SetRow(btn, sec.y);

                }

            }

            ship = s;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            GameEngine.GameSession.Map_UpdateRequested();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ship.Setdamage(1, ship.GetRandomValidDamageLocation());

            }
            catch(Exception exc)
            {
                return;
            }
            LoadInfo(ship);
        }
    }
}
