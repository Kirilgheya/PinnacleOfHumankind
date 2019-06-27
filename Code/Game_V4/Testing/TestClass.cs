using System;
using System.Collections.Generic;
using System.Text;
using MainGame.Applicazione.DataModel;
using MainGame.Applicazione.Engine;
using org.mariuszgromada.math.mxparser;

using System.Globalization;
using System.Diagnostics;
using System.Linq;
using Applicazione.DataModel;

namespace MainGame.Applicazione
{
    class TestClass
    {
	
		public static void Main(string[] Args)
		{

            SimulationEngine.mustShowInfo = true;
            PeriodicTable.init();
            List<int> percentageList = new List<int>();
         

            ChemicalElement element = PeriodicTable.findElement("Hydrogen");
            List <ChemicalElement> chemicalElements = new List<ChemicalElement>();
            chemicalElements.Add(element);
            percentageList.Add(92);

            element = PeriodicTable.findElement("Helium");
            chemicalElements.Add(element);
            percentageList.Add(8);


            

            SimulationEngine.generateStars(100, chemicalElements, percentageList);
            List<Star> stars = SimulationEngine.resultOfGenerateStar;

            int i = 0;
        }


     

       
    }
}
