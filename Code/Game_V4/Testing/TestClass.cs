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
            List<double> percentageList = new List<double>();
        

            ChemicalElement element = PeriodicTable.findByName("Hydrogen");
            List<ChemicalElement> chemicalElements = new List<ChemicalElement>();
            chemicalElements.Add(element);
            percentageList.Add(73.4);

            element = PeriodicTable.findByName("Helium");
            chemicalElements.Add(element);
            percentageList.Add(24.8);

            element = PeriodicTable.findByName("Oxygen");
            chemicalElements.Add(element);
            percentageList.Add(1);

            element = PeriodicTable.findByName("Carbon");
            chemicalElements.Add(element);
            percentageList.Add(0.8);

            SimulationEngine.generateStars(100, chemicalElements, percentageList);
            List<Star> stars = SimulationEngine.resultOfGenerateStar;

            int i = 0;
        }


     

       
    }
}
