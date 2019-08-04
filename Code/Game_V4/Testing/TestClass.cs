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


            ChemicalElement element;
            List<ChemicalElement> chemicalElements = new List<ChemicalElement>();
            
            element = PeriodicTable.findByName("Hydrogen");
            chemicalElements.Add(element);
            percentageList.Add(80.1);

            element = PeriodicTable.findByName("Helium");
            chemicalElements.Add(element);
            percentageList.Add(18.9);

            element = PeriodicTable.findByName("Iron");
            chemicalElements.Add(element);
            percentageList.Add(1.0);

            /*
            element = PeriodicTable.findByName("Sulfur");
            chemicalElements.Add(element);
            percentageList.Add(2.9);

            element = PeriodicTable.findByName("Nickel");
            chemicalElements.Add(element);
            percentageList.Add(1.8);

            element = PeriodicTable.findByName("Calcium");
            chemicalElements.Add(element);
            percentageList.Add(1.5);

            element = PeriodicTable.findByName("Aluminium");
            chemicalElements.Add(element);
            percentageList.Add(1.4);

            element = PeriodicTable.findByName("Nitrogen");
            chemicalElements.Add(element);
            percentageList.Add(1.2);*/

            /*SimulationEngine.generateStars(100, chemicalElements, percentageList);
            List<Star> stars = SimulationEngine.resultOfGenerateStar;

            StarClassification_byLum f = Star.FindStarClass(50000);*/

            ChemicalComposition chemicalComposition = new ChemicalComposition(chemicalElements,percentageList);

            StarSystem system = new StarSystem();
            system.InitSystemParams(new Double[]{1, ParametriUtente.Science.r_sun, 1 }, chemicalComposition);
            system.createStarSystem();


            int i = 0;
        }


     

       
    }
}
