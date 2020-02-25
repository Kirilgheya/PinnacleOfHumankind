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
		
        public String toString()
        {
            string formattedInfo = "";

            formattedInfo = "Planet Class: " + this.Planet_Class;

            return formattedInfo;
        }

		public PlanetClass()
		{
			
			this.className = PlanetClassification.Planet;
			
		}

		public PlanetClass(string _className)
		{

            PlanetClassification classification;
            Enum.TryParse<PlanetClassification>(_className, out classification);
            this.className = classification;

        }
	}
}
