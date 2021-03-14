using GameUI.UI.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameUI.UI.GameEngine
{
    public sealed class GameSessionSavedData
    {

        
        private GameSessionSavedData()
            {
            }

        public static GameSessionSavedData Instance { get { return Nested.instance; } }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly GameSessionSavedData instance = new GameSessionSavedData();
        }

        public List<StarSystem> GameSessionSystems { get; set; }
        
    }
}
