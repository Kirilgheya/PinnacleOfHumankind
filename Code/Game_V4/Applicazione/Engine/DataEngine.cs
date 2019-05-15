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
							in File.ReadLines(@"C:\Users\andre\source\repos\PinnacleOfHumankind\Code\Game\Risorse Extra\stardata.csv"))
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

		public List<ChemicalElement> getPeriodiTable(int _index = 0)
		{
			string[] chemicalValue;
			int counter = 0;
			List<ChemicalElement> listofElements = new List<ChemicalElement>();
			ChemicalElement generatedElement = null;
			double density;
			string name;
			string symbol;
			int atomicNumber;

			foreach (var Lines
							in File.ReadLines(@"C:\Users\andre\source\repos\PinnacleOfHumankind\Code\Game_V4\Risorse Extra\PeriodicTable.csv"))
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
					int.TryParse(chemicalValue[3], out atomicNumber);
					Double.TryParse(chemicalValue[0], out density);
					generatedElement = new ChemicalElement();
					generatedElement.density = density;
					generatedElement.name = name;
					generatedElement.mass = atomicNumber;
					generatedElement.symbol = symbol;
					listofElements.Add(generatedElement);
				
				}

			}

			return listofElements;
		}
	}
}
