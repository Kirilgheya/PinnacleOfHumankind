using Game.Applicazione.DataModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
namespace Game.Applicazione.Engine
{
	class DataEngine
	{

		public Star getPresetStarData(int _index = 0)
		{
			string[] starValues;
			int counter = 0;
			Star generatedStar = null;
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
					double mass;
					double surfaceTemp;
					double luminosity;
					int lumClass;
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
		
	}
}
