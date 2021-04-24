using GameUI.Artificial;
using GameUI.UI.DataSource;
using GameUI.UI.DataSource.UIItems_DS;
using Polenter.Serialization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameUI.UI.GameEngine
{

    public static class GameSessionHandler
    {

        public static List<artificialObj> artificialList = new List<artificialObj>();



        public static bool drawAsteroids = false;
        public static bool drawAsteroidsOrbits = false;

        public static List<StarSystem> GameSessionSystemList = new List<StarSystem>();
        public static List<StarSystem> GameSessionSystems { get { return GameSessionSystemList; } set { GameSessionSystemList = value; } }
        public static String filename = ConfigurationManager.AppSettings.Get("SaveDataFilename");
        public static String filepath = ConfigurationManager.AppSettings.Get("SaveDataPath");
        public static String folder = ConfigurationManager.AppSettings.Get("SaveDataFolderPattern");
        public static double TimePassed = 0;


        //Audio Player? START
        public static List<String> AudioFiles = new List<string>();


        public static SoundPlayerEx MusicPlayer;
        public static int filesnumber = 0;
        public static int playing = 0;
        public static String musicDirectoryPath = AppDomain.CurrentDomain.BaseDirectory + "Res\\Sounds\\";
        private static bool _audio;
        internal static Main_Map map;
        //Audio Player END


        public static List<IBodyTreeViewItem> selected = new List<IBodyTreeViewItem>();


        internal static bool UpdateSelected(IBodyTreeViewItem item)
        {
            if (selected.Contains(item))
            {
                selected.Remove(item);

                item.selected = false;
            }
            else
            {
                selected.Add(item);

                item.selected = true;

                return true;
            }

            return false;
        }

        public static void AdvanceTime(double _timestep)
        {

            TimePassed += _timestep;

            
        }

        public static bool somethingSelected
        {
            get
            {
                return selected.Count > 0;
            }
        }


        internal static void Map_UpdateRequested()
        {
            map.redrawSystem();
        }

   

        public static bool audio
        {
            set
            {
                _audio = value;

                if (audio)
                {
                    AudioFiles = new List<string>();
                    foreach (string musicFilePath in Directory.EnumerateFiles(musicDirectoryPath, "*", SearchOption.AllDirectories))
                    {
                        AudioFiles.Add(musicFilePath);

                        filesnumber++;
                    }


                    PlayAllMusics();


                }
                else
                {
                    try
                    {
                        MusicPlayer.Stop();
                    }
                    catch(Exception exc)
                    {

                    }
                }
            }
            get
            {
                return _audio;
            }
        }

        private static void PlayAllMusics()
        {
            playing = 0;
            if (AudioFiles.Count > 0)
            {
                MusicPlayer = new SoundPlayerEx(AudioFiles[playing]);
                MusicPlayer.SoundFinished += player_SoundFinished;
                MusicPlayer.PlayAsync();
            }
        }


        public static void saveGame()
        {

            SharpSerializer formatter = new SharpSerializer();

            int directoryCount;

            String fileExtension = ".bin";

            directoryCount = System.IO.Directory.GetDirectories(filepath).Length;
            Directory.CreateDirectory(filepath + "\\" + folder + (directoryCount + 1));
            using (FileStream fs = File.Create(filepath + "\\" + folder + (directoryCount + 1) + "\\" + filename + fileExtension))
            {

                if (GameSessionHandler.GameSessionSystems != null)
                {

                    formatter.Serialize(GameSessionHandler.saveDataInto(), fs);

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
                    SharpSerializer binaryFormatter = new SharpSerializer(true);
                    if (openFileDialog.OpenFile().Length > 0)
                    {
                        GameSessionSavedData saveData = (GameSessionSavedData)binaryFormatter.Deserialize(openFileDialog.OpenFile());
                        //Read the contents of the file into a stream

                        if (GameSessionHandler.loadDataInto(saveData))
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
                GameSessionHandler.GameSessionSystems = _savedData.GameSessionSystems;
                GameSessionHandler.TimePassed = _savedData.TimePassed;
            }
            catch (Exception e)
            {
                result = false;
                throw e;
            }

            return result;
        }

        internal static double GetTimePassed()
        {
            return GameSessionHandler.TimePassed;
        }

        private static GameSessionSavedData saveDataInto()
        {

            GameSessionSavedData saveData = new GameSessionSavedData();

            saveData.GameSessionSystems = GameSessionHandler.GameSessionSystems;
            saveData.TimePassed = GameSessionHandler.TimePassed;

            return saveData;
        }



        static void player_SoundFinished(object sender, EventArgs e)
        {
            playing++;
            if (playing > AudioFiles.Count)
            {
                playing = 0;

                MusicPlayer = new SoundPlayerEx(AudioFiles[playing]);
                MusicPlayer.SoundFinished += player_SoundFinished;
            }
        }


    }


    public static class SoundInfo
    {
        [DllImport("winmm.dll")]
        private static extern uint mciSendString(
            string command,
            StringBuilder returnValue,
            int returnLength,
            IntPtr winHandle);

        public static int GetSoundLength(string fileName)
        {
            StringBuilder lengthBuf = new StringBuilder(32);

            mciSendString(string.Format("open \"{0}\" type waveaudio alias wave", fileName), null, 0, IntPtr.Zero);
            mciSendString("status wave length", lengthBuf, lengthBuf.Capacity, IntPtr.Zero);
            mciSendString("close wave", null, 0, IntPtr.Zero);

            int length = 0;
            int.TryParse(lengthBuf.ToString(), out length);

            return length;
        }
    }

    public class SoundPlayerEx : SoundPlayer
    {
        public bool Finished { get; private set; }

        private Task _playTask;
        private CancellationTokenSource _tokenSource = new CancellationTokenSource();
        private CancellationToken _ct;
        private string _fileName;
        private bool _playingAsync = false;

        public event EventHandler SoundFinished;

        public SoundPlayerEx(string soundLocation)
            : base(soundLocation)
        {
            _fileName = soundLocation;
            _ct = _tokenSource.Token;
        }

        public void PlayAsync()
        {
            Finished = false;
            _playingAsync = true;
            Task.Run(() =>
            {
                try
                {
                    double lenMs = SoundInfo.GetSoundLength(_fileName);
                    DateTime stopAt = DateTime.Now.AddMilliseconds(lenMs);
                    this.Play();
                    while (DateTime.Now < stopAt)
                    {
                        _ct.ThrowIfCancellationRequested();
                        //The delay helps reduce processor usage while "spinning"
                        Task.Delay(10).Wait();
                    }
                }
                catch (OperationCanceledException)
                {
                    base.Stop();
                }
                finally
                {
                    OnSoundFinished();
                }

            }, _ct);
        }

        public new void Stop()
        {
            if (_playingAsync)
                _tokenSource.Cancel();
            else
                base.Stop();   //To stop the SoundPlayer Wave file
        }

        protected virtual void OnSoundFinished()
        {
            Finished = true;
            _playingAsync = false;

            EventHandler handler = SoundFinished;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}