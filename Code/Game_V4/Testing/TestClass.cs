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
        

            ChemicalElement element = PeriodicTable.findByName("Iron");
            List<ChemicalElement> chemicalElements = new List<ChemicalElement>();
            chemicalElements.Add(element);
            percentageList.Add(32.1);

            element = PeriodicTable.findByName("Oxygen");
            chemicalElements.Add(element);
            percentageList.Add(30.1);

            element = PeriodicTable.findByName("Silicon");
            chemicalElements.Add(element);
            percentageList.Add(15.1);

            element = PeriodicTable.findByName("Magnesium");
            chemicalElements.Add(element);
            percentageList.Add(13.9);

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
            percentageList.Add(1.2);

            /*SimulationEngine.generateStars(100, chemicalElements, percentageList);
            List<Star> stars = SimulationEngine.resultOfGenerateStar;

            StarClassification_byLum f = Star.FindStarClass(50000);*/

            Planet planet = new Planet(chemicalElements,ParametriUtente.Science.r_t);
          
            planet.initPlanet(1, 1, percentageList);

            int i = 0;
        }


     

       
    }
}
