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

namespace GameUI.UI.Utilities
{
    /// <summary>
    /// Interaction logic for TreeVisualizer.xaml
    /// </summary>
    public partial class TreeVisualizer : Window
    {
        public TreeVisualizer()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(((sender as Button).Content as System.Xml.XmlAttribute).Value + " " + ((sender as Button).Tag as System.Xml.XmlAttribute).Value);
        }
    }
}
