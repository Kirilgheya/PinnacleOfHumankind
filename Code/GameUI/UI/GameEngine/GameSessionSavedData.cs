using GameUI.UI.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameUI.UI.GameEngine
{
    [Serializable]
    public sealed class GameSessionSavedData
    {

        public double TimePassed { get; set; }

        public List<StarSystem> GameSessionSystems { get; set; }
        
    }
}
