using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Applicazione.DataModel
{
	class Star : Body
	{

		// Luminosity, Size and temperature returns the "apparent colour" of the Star
		private double luminosity;
		private double metallicity;
		private double age;

		private double temperature;
		private List<ChemicalElement> stellarCompositionMats;
		private double starRadius;

		public Star(
				double _temperature,
				List<ChemicalElement> _stellarCompositionMats,
				double _starRadius
			)
		{

			this.temperature = _temperature;
			this.stellarCompositionMats = _stellarCompositionMats;
			this.starRadius = _starRadius;
		}

		public void initStart()
		{


		}


	}
}
