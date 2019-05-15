using System;
using System.Collections.Generic;
using System.Text;
using MainGame.Applicazione.DataModel;
using MainGame.Applicazione.Engine;

namespace MainGame.Applicazione
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
