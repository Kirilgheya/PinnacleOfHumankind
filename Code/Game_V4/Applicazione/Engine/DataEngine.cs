using MainGame.Applicazione.DataModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
namespace MainGame.Applicazione.Engine
{
    public class DataEngine
    {
        private static string extraResourcePath = ParametriUtente.exeRootFodler + "\\\\Risorse Extra\\\\";
        private List<ChemicalElement> listofElements = new List<ChemicalElement>();
        public static List<ChemicalElement> starSeed = new List<ChemicalElement>();
        public static List<ChemicalElement> ironPlanetSeed = new List<ChemicalElement>();
        public static List<ChemicalElement> rockyPlanetSeed = new List<ChemicalElement>();
        public static List<ChemicalElement> gasPlanetSeed = new List<ChemicalElement>();
        public List<ChemicalElement> GetChemicalElements()
        {
            return this.listofElements;
        }

        public Star getPresetStarData(int _index = 0)
        {
            string[] starValues;
            int counter = 0;
            Star generatedStar = null;
            double mass;
            double surfaceTemp;
            double luminosity;
            int lumClass;

            foreach (var Lines
                            in File.ReadLines(@"" + extraResourcePath + "stardata.csv"))
            {
                if (counter != _index)
                {

                    counter++;
                }
                else
                {

                    counter = -1;
                    starValues = Lines.Split(';',',');

                    Double.TryParse(starValues[4], out mass);
                    Double.TryParse(starValues[1], out surfaceTemp);
                    Double.TryParse(starValues[2], out luminosity);
                    int.TryParse(starValues[0], out lumClass);
                    generatedStar = new Star(luminosity, surfaceTemp, mass, lumClass);
                    break;
                }

            }

            return generatedStar;
        }

        public void setSeeds()
        {
            starSeed.Add(findByName("Hydrogen"));
            starSeed.Add(findByName("Helium"));
            starSeed.Add(findByName("Argon"));
            starSeed.Add(findByName("Oxygen"));
            starSeed.Add(findByName("Carbon"));
            starSeed.Add(findByName("Iron"));
            starSeed.Add(findByName("Xenon"));

            gasPlanetSeed.Add(findByName("Hydrogen"));
            gasPlanetSeed.Add(findByName("Helium"));
            gasPlanetSeed.Add(findByName("Hydrogen"));
            gasPlanetSeed.Add(findByName("Calcium"));
            gasPlanetSeed.Add(findByName("Oxygen"));
            gasPlanetSeed.Add(findByName("Silver"));
            gasPlanetSeed.Add(findByName("Carbon"));
            gasPlanetSeed.Add(findByName("Silicon"));
            gasPlanetSeed.Add(findByName("Lithium"));
            gasPlanetSeed.Add(findByName("Tin"));
            gasPlanetSeed.Add(findByName("Sulfur"));
            gasPlanetSeed.Add(findByName("Nickel"));
            gasPlanetSeed.Add(findByName("Copper"));
            gasPlanetSeed.Add(findByName("Cobalt"));
            gasPlanetSeed.Add(findByName("Sodium"));
            gasPlanetSeed.Add(findByName("Argon"));

            ironPlanetSeed.Add(findByName("Iron"));
            ironPlanetSeed.Add(findByName("Oxygen"));
            ironPlanetSeed.Add(findByName("Hydrogen"));
            ironPlanetSeed.Add(findByName("Calcium"));
            ironPlanetSeed.Add(findByName("Hydrogen"));
            ironPlanetSeed.Add(findByName("Silver"));
            ironPlanetSeed.Add(findByName("Carbon"));
            ironPlanetSeed.Add(findByName("Silicon"));
            ironPlanetSeed.Add(findByName("Lithium"));
            ironPlanetSeed.Add(findByName("Tin"));
            ironPlanetSeed.Add(findByName("Sulfur"));
            ironPlanetSeed.Add(findByName("Nickel"));
            ironPlanetSeed.Add(findByName("Copper"));
            ironPlanetSeed.Add(findByName("Cobalt"));
            ironPlanetSeed.Add(findByName("Sodium"));
            ironPlanetSeed.Add(findByName("Argon"));

            rockyPlanetSeed.Add(findByName("Carbon"));
            rockyPlanetSeed.Add(findByName("Silicon"));
            rockyPlanetSeed.Add(findByName("Hydrogen"));
            rockyPlanetSeed.Add(findByName("Iron"));
            rockyPlanetSeed.Add(findByName("Hydrogen"));
            rockyPlanetSeed.Add(findByName("Silver"));
            rockyPlanetSeed.Add(findByName("Carbon"));
            rockyPlanetSeed.Add(findByName("Oxygen"));
            rockyPlanetSeed.Add(findByName("Lithium"));
            rockyPlanetSeed.Add(findByName("Tin"));
            rockyPlanetSeed.Add(findByName("Sulfur"));
            rockyPlanetSeed.Add(findByName("Nickel"));
            rockyPlanetSeed.Add(findByName("Copper"));
            rockyPlanetSeed.Add(findByName("Cobalt"));
            rockyPlanetSeed.Add(findByName("Sodium"));
            rockyPlanetSeed.Add(findByName("Argon"));


        }

        public void setPeriodicTable(int _index = 0)
        {
            NumberStyles style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");


            var lines = File.ReadLines(@"" + extraResourcePath + "PeriodicTable.csv");

            //per togliere l'intestazione
            lines = lines.Skip(1);

            listofElements = lines.Select(x =>
           {
               var chemicalValue = x.Split(',', ';');

               double _density;
               Double.TryParse(chemicalValue[0], style, culture, out _density);

               double _atomicWeight;
               Double.TryParse(chemicalValue[5], style, culture, out _atomicWeight);

               return new ChemicalElement()
               {
                   density = _density,
                   name = chemicalValue[1],
                   symbol = chemicalValue[2],
                   numberOfParticles = Int32.Parse(chemicalValue[3].Trim()),
                   state = (ElementState)Enum.Parse(typeof(ElementState), chemicalValue[4], true),
                   mass = _atomicWeight,



               };

           }).ToList();
        }


        public ChemicalElement findByName(string _name)
        {
            return this.listofElements.Where(x => x.name.Equals(_name)).FirstOrDefault();
        }
    }
}
