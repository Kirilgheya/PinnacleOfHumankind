using System;
using System.Collections.Generic;
using System.Text;
using Game.Applicazione.DataModel;
namespace Game.Applicazione
{
    class TestClass
    {
	
		public static void Main(string[] Args)
		{
			ChemicalEngine engine = new ChemicalEngine();
			engine.initDefaultPeriodicTable();
		}
    }
}
