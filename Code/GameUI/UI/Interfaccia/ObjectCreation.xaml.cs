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
    /// Interaction logic for ObjectCreation.xaml
    /// </summary>
    public partial class ObjectCreation : Window
    {
        public ObjectCreation()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UI.Main_Map.selected_SS.relatedStarSystem.getStars();
        }
    }
}
