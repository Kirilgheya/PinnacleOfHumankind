using System;
using System.Collections.Generic;
using System.Text;
using MainGame.Applicazione.DataModel;
using MainGame.Applicazione.Engine;
using org.mariuszgromada.math.mxparser;

using System.Globalization;
using System.Diagnostics;
using System.Linq;

namespace MainGame.Applicazione
{
    class TestClass
    {
	
		public static void Main(string[] Args)
		{

            SimulationEngine.mustShowInfo = true;

            DataEngine engine = new DataEngine();
            string x = ParametriUtente.exeRootFodler;
            List<ChemicalElement> periodicTable = new List<ChemicalElement>();
            List<double> percentageList = new List<double>();
            periodicTable = engine.getPeriodicTable(0);

            ChemicalElement element = periodicTable.ElementAt(0);
            List<ChemicalElement> chemicalElements = new List<ChemicalElement>();
            chemicalElements.Add(element);
            percentageList.Add(74.9);

            element = periodicTable.ElementAt(2);
            chemicalElements.Add(element);
            percentageList.Add(23.8);

            element = periodicTable.ElementAt(3);
            chemicalElements.Add(element);
            percentageList.Add(1.2);

            element = periodicTable.ElementAt(6);
            chemicalElements.Add(element);
            percentageList.Add(0.8);

            SimulationEngine.generateStars(100, chemicalElements, percentageList);
            List<Star> stars = SimulationEngine.resultOfGenerateStar;

            int i = 0;
        }


     

       
    }
}
