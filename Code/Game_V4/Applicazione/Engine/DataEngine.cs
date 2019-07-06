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
