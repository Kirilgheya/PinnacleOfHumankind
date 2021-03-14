using GameUI.UI.DataSource;
using Polenter.Serialization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameUI.UI.GameEngine
{
    
    public static class GameSession
    {
        public static List<StarSystem> GameSessionSystems { get; set; }
        public static String filename = ConfigurationManager.AppSettings.Get("SaveDataFilename");
        public static String filepath = ConfigurationManager.AppSettings.Get("SaveDataPath");
        public static String folder = ConfigurationManager.AppSettings.Get("SaveDataFolderPattern");
      
        public static void saveGame()
        {

            SharpSerializer formatter = new SharpSerializer(new SharpSerializerBinarySettings(BinarySerializationMode.SizeOptimized));

            int directoryCount;

            String extension = ".bin";

            directoryCount = System.IO.Directory.GetDirectories(filepath).Length;
            Directory.CreateDirectory(filepath + "\\" + folder + (directoryCount + 1));
            using (FileStream fs = File.Create(filepath + "\\" + folder + (directoryCount + 1) + "\\" + filename + extension))
            {

                if (GameSession.GameSessionSystems != null)
                {

                    formatter.Serialize(GameSession.saveDataInto(),fs);

                }

            }
        }

        public static void loadGame()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = filepath;
                openFileDialog.Filter = "Bin files (*.bin)|*.bin|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //Get the path of specified file
                    string localfilePath = openFileDialog.FileName;
                     SharpSerializer binaryFormatter = new SharpSerializer();
                    if (openFileDialog.OpenFile().Length > 0)
                    {
                        GameSessionSavedData saveData = (GameSessionSavedData)binaryFormatter.Deserialize(openFileDialog.OpenFile());
                        //Read the contents of the file into a stream

                        if (GameSession.loadDataInto(saveData))
                        {

                            Console.WriteLine("Game loaded Successfully from: " + localfilePath);
                        }
                        else
                        {

                            Console.WriteLine("Game did not load successfully from: " + localfilePath);
                        }
                    }
                }
            }
        }

        private static Boolean loadDataInto(GameSessionSavedData _savedData)
        {

            Boolean result = true;
            try
            { 
                GameSession.GameSessionSystems = _savedData.GameSessionSystems;
            }
            catch(Exception e)
            {
                result = false;
                throw e;
            }

            return result;
        }

        private static GameSessionSavedData saveDataInto()
        {

            GameSessionSavedData saveData = new GameSessionSavedData();

            saveData.GameSessionSystems = GameSession.GameSessionSystems;


            return saveData;
        }
    }
}
