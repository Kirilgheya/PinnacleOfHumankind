using GameUI.UI;
using Session = GameUI.UI.GameEngine;
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
using System.Configuration;
using System.Collections.Specialized;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using GameUI.UI.GameEngine;

namespace GameUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Home home = new Home();

            //home.Show();

            Main_Map map = new Main_Map();

            map.Show();

            this.Close();
        }
        
        private void BtnSaveData_Click(object sender, RoutedEventArgs e)
        {

            GameSessionHandler.saveGame();
        }

        private void BtnLoadData_Click(object sender, RoutedEventArgs e)
        {

            GameSessionHandler.loadGame();
        }

        private void ToggleAudioCLick(object sender, RoutedEventArgs e)
        {
            GameSessionHandler.audio = !GameSessionHandler.audio;
            if (GameSessionHandler.audio)
            {
                btnAudio.Content = "🔈";
            }
            else
            {
                btnAudio.Content = "🔇";
            }
        }


        private void BtnMapEditor_Click(object sender, RoutedEventArgs e)
        {
            EditorMap map = new EditorMap();

            map.Show();

        }
    }
}
