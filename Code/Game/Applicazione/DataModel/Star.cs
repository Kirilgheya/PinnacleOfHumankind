using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Applicazione.DataModel
{
	class Star : Body
	{

		// Luminosity, Size and temperature returns the "apparent colour" of the Star
		private double luminosity;
		protected double relluminosity;
		private double metallicity;
		private double age;

		private double temperature;
		private List<ChemicalElement> stellarCompositionMats;
		private double starRadius;


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
				double _relmass
			)
		{

			this.relativeMass = _relmass;
			this.temperature = _surfaceTemperature;
			this.relluminosity = _relluminosity;
			
		}

			public void initStar()
		{

			
		}


	}
}
