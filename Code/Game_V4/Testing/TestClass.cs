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
using System.IO;
using System.Text.RegularExpressions;

namespace MainGame.Applicazione
{
    class TestClass
    {
	
		public static void Main(string[] Args)
		{

            SimulationEngine.mustShowInfo = true;

            PeriodicTable.init();
            TestClass.createEarth();
            List<double> percentageList = new List<double>();

            createEarth();
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
            percentageList.Add(1.69);
          
            
            ChemicalComposition chemicalComposition = new ChemicalComposition(chemicalElements,percentageList);

            StarSystem system = new StarSystem();
            system.InitSystemParams(new Double[]{1, ParametriUtente.Science.r_sun, 1 }, chemicalComposition);
            system.createStarSystem();

            string outputFile = system.toString();

            system.InitSystemParams(new Double[] { 1, ParametriUtente.Science.r_sun * 2, 15 }, chemicalComposition);
            system.createStarSystem();

            outputFile = string.Concat(outputFile,system.toString());

            printToFile(outputFile);
            int i = 0;
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
            element = PeriodicTable.findByName("Iron");
            chemicalElements.Add(element);
            percentageList.Add(0.04);


            ChemicalComposition chemicalComposition = new ChemicalComposition(chemicalElements, percentageList);
            Planet x = new Planet(chemicalComposition, ParametriUtente.Science.r_t, ParametriUtente.Science.AU);
            x.initPlanet();
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
