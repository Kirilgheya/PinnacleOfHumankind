using System;
using System.Collections.Generic;
using System.Text;
using MainGame;
namespace MainGame.Applicazione.DataModel
{
    public class PlanetClass
    {
		public PlanetClassification className { get; set; }
		
		public string Planet_Class { get { return this.className.ToString(); }
			set
			{
				PlanetClassification classification;
				Enum.TryParse<PlanetClassification>(value,out classification);
				this.className = classification;
			}
		}
		

		public PlanetClass(List<ChemicalElement> _elements)
		{
			
			this.className = PlanetClassification.Planet;
			
		}

		public PlanetClass(string _className)
		{

			this.className = PlanetClassification.Planet;

		}
	}
}
