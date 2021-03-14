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
            BinaryFormatter formatter = new BinaryFormatter();
            String filename, filepath, folder;
            int directoryCount;
            filename = ConfigurationManager.AppSettings.Get("SaveDataFilename");
            filepath = ConfigurationManager.AppSettings.Get("SaveDataPath");
            folder = ConfigurationManager.AppSettings.Get("SaveDataFolderPattern");


            directoryCount = System.IO.Directory.GetDirectories(filepath).Length;
            Directory.CreateDirectory(filepath + "\\" + folder + (directoryCount + 1));
            using (FileStream fs = File.Create(filepath+"\\"+ folder+(directoryCount+1)+ "\\"+filename))
            { 

                if(GameSessionSavedData.Instance.GameSessionSystems != null)
                { 

                    formatter.Serialize(fs, GameSessionSavedData.Instance);

                }

            }
        }
    }
}
