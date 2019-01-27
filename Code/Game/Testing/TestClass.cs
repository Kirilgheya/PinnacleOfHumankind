using System;
using System.Collections.Generic;
using System.Text;
using Game.Applicazione.DataModel;
using Game.Applicazione.Engine;

namespace Game.Applicazione
{
    class TestClass
    {
	
		public static void Main(string[] Args)
		{
			DataEngine engine = new DataEngine();
			Star star = engine.getPresetStarData(245);

			int x = 0;
		}
    }
}
