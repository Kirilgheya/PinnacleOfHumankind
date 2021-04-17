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

namespace GameUI.UI.Interfaccia
{
    /// <summary>
    /// Interaction logic for ShipInfoPage.xaml
    /// </summary>
    public partial class ShipInfoPage : Window
    {
        public ShipInfoPage()
        {
            InitializeComponent();
        }

        public void LoadInfo(Ship s)
        {
            Ellipse el = new Ellipse();

            el.Width = 200;
            el.Height = 200;
            el.Fill = s.Shape.Fill;

            cv_back.Children.Add(el);

            txtInfo.Text = s.name;

            int column = 0;
            int row = 0;
            foreach(List<ShipSector> Lsec in s.Structure)
            {
                shipBluePrint.ColumnDefinitions.Add(new ColumnDefinition());             
                foreach (ShipSector sec in Lsec)
                {
                    shipBluePrint.RowDefinitions.Add(new RowDefinition());

                    Button btn = new Button();
                    btn.Content = column.ToString() + " " + row.ToString() + sec.SectorComponents.First().Name;

                    shipBluePrint.Children.Add(btn);

                    Grid.SetColumn(btn, column);
                    Grid.SetRow(btn, row);

                    row++; 
                }
                row = 0;
                column++;
            }

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            GameEngine.GameSession.Map_UpdateRequested();
        }

    }
}
