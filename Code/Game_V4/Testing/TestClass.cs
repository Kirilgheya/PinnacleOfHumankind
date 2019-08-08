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
          
            
            ChemicalComposition chemicalComposition = new ChemicalComposition(chemicalElements,percentageList);

            StarSystem system = new StarSystem();
            system.InitSystemParams(new Double[]{1, ParametriUtente.Science.r_sun*3, 8 }, chemicalComposition);
            system.createStarSystem();


            printToFile(system.toString(),"nulla");
            int i = 0;
        }


     
        private static void printToFile(String _content,String _path)
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
