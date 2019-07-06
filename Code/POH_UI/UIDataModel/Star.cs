using System;
using System.Collections.Generic;

namespace MainGame.UI.DataModel
{
	public class Star : Applicazione.DataModel.Star
	{

		// Luminosity, Size and temperature returns the "apparent colour" of the Star
		public string Luminosity { get { return this.luminosityClass.ToString(); } set { } }
	
		public string Age { get; set; }
		public string Radius { get { return this.relativeRadius.ToString(); }  }

        public string Mass { get { return this.relativeMass.ToString()+" Solar Masses"; }  }

        public string Type { get { return this.overallClass.ToString(); } set { } }

        public string Temperature;
		public Star (
				double _starRadius,
				double _temperature = 0.0,
				List<Applicazione.DataModel.ChemicalElement> _stellarCompositionMats = null

			) : base(_starRadius, _temperature, _stellarCompositionMats)

		{
			this.Temperature = _temperature.ToString();
			
		
			
		}

		public Star(Applicazione.DataModel.Star _star) :  base(_star)
			

		{
			this.Temperature = base.relSurfacetemperature.ToString();
			this.Luminosity = base.luminosityClass.ToString();
			this.Age = base.age.ToString();
			
            this.Type = base.overallClass + "/Luminosity:" + this.luminosityClass + "/Mass:" + this.massClass;
		}

        public Star(
               double _solarMasses,
               double _solarRadii,
               List<Applicazione.DataModel.ChemicalElement> _stellarCompositionMats,
               List<double> distribution

           ) : base( _solarMasses,
                _solarRadii,
                _stellarCompositionMats,
               distribution)
        {


            
        }


        public Star(
				double _relluminosity,
				double _surfaceTemperature,
				double _relmass,
				int _class
			) : base(_relluminosity, _surfaceTemperature, _relmass, _class)
		{

			this.relativeMass = _relmass;
			this.Temperature = _surfaceTemperature.ToString();
		
			this.luminosityClass = (StarClassification_byLum)Enum.ToObject(typeof(StarClassification_byLum), _class);
		}


	
		

	}
}
