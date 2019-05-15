using System;
using System.Collections.Generic;
using System.Text;

namespace MainGame.Applicazione.DataModel
{
	public class Star : Body
	{

		// Luminosity, Size and temperature returns the "apparent colour" of the Star
		protected double luminosity;
		protected double relluminosity;
		protected double metallicity;
		protected double age;
	
		public StarClass luminosityClass;
		public enum StarClass
		{
			None = 0,
			Supergiganti = 1,
			Giganti_brillanti = 2,
			Giganti = 3,
			Sotto_giganti = 4,
			Standard = 5
		}

		protected double temperature { get; set; }
		protected List<ChemicalElement> stellarCompositionMats;
		protected double starRadius;


		public Star(
				double _starRadius,
				double _temperature = 0.0,
				List<ChemicalElement> _stellarCompositionMats = null
				
			)
		{

			this.temperature = _temperature;
			this.stellarCompositionMats = _stellarCompositionMats;
			this.starRadius = _starRadius;
		}

		public Star(
				double _relluminosity,
				double _surfaceTemperature,
				double _relmass,
				int _class
			)
		{

			this.relativeMass = _relmass;
			this.temperature = _surfaceTemperature;
			this.relluminosity = _relluminosity;
			this.luminosityClass = (StarClass)Enum.ToObject(typeof(StarClass), _class);
		}

		public Star(
				Star _star
			)
		{
			this.temperature = _star.temperature;
			this.stellarCompositionMats = _star.stellarCompositionMats;
			this.starRadius = _star.starRadius;
		}

		public void initStar()
		{

			
		}

		

	}
}
