using System;
using System.Collections.Generic;
using MainGame.Applicazione.DataModel;
using MainGame.Applicazione.Engine;
using Applicazione.DataModel;
using System.Xml.Serialization;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using MainGame.Applicazione.DataModel.Utils;

namespace MainGame.Applicazione
{
    class TestClass
    {
	
		public static void Main(string[] Args)
		{

            SimulationEngine.mustShowInfo = true;

            PeriodicTable.init();
            TestClass.createEarth();
            TestClass.createSun();
            List<double> percentageList = new List<double>();

            createEarth();
            ChemicalElement element;
            List<ChemicalElement> chemicalElements = DataEngine.starSeed;
            PeriodicTable.findElementByName("Water").getMolality();
            percentageList = SimulationEngine.generateDistributionList(90, 70, chemicalElements.Count);

            
            ChemicalComposition chemicalComposition = new ChemicalComposition(chemicalElements,percentageList);

            StarSystem system = new StarSystem();
            system.InitSystemParams(new Double[]{1, ParametriUtente.Science.r_sun, 1 }, chemicalComposition);
            system.createStarSystem();

            string outputFile = system.toString();

            system.InitSystemParams(new Double[] { 1, ParametriUtente.Science.r_sun * 1.71, 2.02 }, chemicalComposition);
            system.createStarSystem();


            PlanetColor color = new PlanetColor(System.Drawing.Color.Blue);
            double ggg = color.getAlbedo();

            outputFile = string.Concat(outputFile,system.toString());
            int x = 0;

            while(x<1000)
            {
              
                x++;
            }

            printToFile(outputFile);
            int i = 0;
        }

        public static void createSun()
        {
            List<double> percentageList = new List<double>();


            ChemicalElement element;
            List<ChemicalElement> chemicalElements = new List<ChemicalElement>();

            element = PeriodicTable.findByName("Hydrogen");
            chemicalElements.Add(element);
            percentageList.Add(73.46);

            element = PeriodicTable.findByName("Helium");
            chemicalElements.Add(element);
            percentageList.Add(24.85);

            element = PeriodicTable.findByName("Oxygen");
            chemicalElements.Add(element);

            percentageList.Add(0.87);
            element = PeriodicTable.findByName("Iron");
            chemicalElements.Add(element);
            percentageList.Add(0.26);
            element = PeriodicTable.findByName("Neon");
            chemicalElements.Add(element);
            percentageList.Add(0.22);
            element = PeriodicTable.findByName("Nitrogen");
            chemicalElements.Add(element);
            percentageList.Add(0.12);
            element = PeriodicTable.findByName("Silicon");
            chemicalElements.Add(element);
            percentageList.Add(0.11);
            element = PeriodicTable.findByName("Magnesium");
            chemicalElements.Add(element);
            percentageList.Add(0.06); ;
            element = PeriodicTable.findByName("Sulfur");
            chemicalElements.Add(element);
            percentageList.Add(0.05);


            ChemicalComposition chemicalComposition = new ChemicalComposition(chemicalElements, percentageList);
            Star star = new Star(ParametriUtente.Science.r_sun, 0, chemicalElements);


            star.initStar(1, 1, chemicalComposition.get_percentage());
            Console.WriteLine("Sun created check debug values");
        }

        public static void createEarth()
        {
            List<double> percentageList = new List<double>();


            ChemicalElement element;
            List<ChemicalElement> chemicalElements = new List<ChemicalElement>();

            element = PeriodicTable.findByName("Nitrogen");
            chemicalElements.Add(element);
            percentageList.Add(78.08);

            element = PeriodicTable.findByName("Oxygen");
            chemicalElements.Add(element);
            percentageList.Add(20.95);

            element = PeriodicTable.findByName("Argon");
            chemicalElements.Add(element);
            percentageList.Add(0.93);
            element = PeriodicTable.findByName("Carbon");
            chemicalElements.Add(element);
            percentageList.Add(0.04);


            ChemicalComposition chemicalComposition = new ChemicalComposition(chemicalElements, percentageList);
            Planet x = new Planet(chemicalComposition, ParametriUtente.Science.r_t, ParametriUtente.Science.AU);
            x.initPlanet();

            Console.WriteLine("Earth created check debug values");
        }

        private static void printToFile(String _content)
        {
            string docPath =
          Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filename = DateTime.Now.ToString();
            Regex digitsOnly = new Regex(@"[^\d]");
            filename = digitsOnly.Replace(filename, "");
            // Write the string array to a new file named "WriteLines.txt".
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, filename+".txt")))
            {
              
                    outputFile.WriteLine(_content);
            }
        }
       
       
    }
}
