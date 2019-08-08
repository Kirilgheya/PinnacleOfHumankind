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
        private static string extraResourcePath = ParametriUtente.exeRootFodler+"\\\\Risorse Extra\\\\";
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
							in File.ReadLines(@""+extraResourcePath+"stardata.csv"))
			{
				if(counter != _index)
				{

					counter++;
				}
				else
				{

					counter = -1;
					starValues = Lines.Split(';');
					
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
			string[] chemicalValue;
			int counter = 0;
			
			ChemicalElement generatedElement = null;
			double density;
			string name;
			string symbol;
            string state;
            double atomicWeight;
			int atomicNumber;
            NumberStyles style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;
            CultureInfo  culture = CultureInfo.CreateSpecificCulture("en-US");
            foreach (var Lines
							in File.ReadLines(@"" + extraResourcePath + "PeriodicTable.csv"))
			{
				if (_index > 0 && counter != _index)
				{

					counter++;
				}
				else
				{
					if(counter==0 && _index==0)
					{
						counter = -1;
						continue;
						
					}
					
					chemicalValue = Lines.Split(';');

					symbol = chemicalValue[2];
					name = chemicalValue[1];
                    state = chemicalValue[4];
					int.TryParse(chemicalValue[3], out atomicNumber);
					Double.TryParse(chemicalValue[0],style, culture, out density);
                    Double.TryParse(chemicalValue[5],style,culture, out atomicWeight);
					generatedElement = new ChemicalElement();
					generatedElement.density = density;
					generatedElement.name = name;
					generatedElement.mass = atomicWeight;
					generatedElement.symbol = symbol;
                    generatedElement.state = (ElementState)Enum.Parse(typeof(ElementState), state, true);
                    generatedElement.numberOfParticles = atomicNumber;
                    listofElements.Add(generatedElement);
				
				}

			}

			
		}

        public ChemicalElement findByName(string name)
        {
            foreach(ChemicalElement element in this.listofElements)
            {
                if(element.name.Equals(name))
                {
                    return element;
                }
            }

            return null;
        }
	}
}
