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
				double _starRadius,
				double _temperature = 0.0,
				List<ChemicalElement> _stellarCompositionMats = null
				
			)
		{

			this.temperature = _temperature;
			this.stellarCompositionMats = _stellarCompositionMats;
			this.starRadius = _starRadius;
		}

		public void initStar(List<ChemicalElement> _elements = null)
		{

			if(stellarCompositionMats == null)
			{

				stellarCompositionMats = _elements;
			}

			ChemicalElement hydrogen = new ChemicalElement();
			ChemicalElement helium = new ChemicalElement();
			hydrogen.name = "Hydrogen";
			helium.name = "Helium";
			//TODO implement search by name

			hydrogen.density = 0.0899 / 1000.0; // km / m^3 -> g/cm^3
			helium.density = 0.1785 / 1000.0; // km / m^3 -> g/cm^3
			Random gen = new Random();
			double mass = gen.NextDouble() * 100;
			double volume = 4 / 3 * Math.PI * (Math.Pow(this.starRadius, 3.0));
			double density = (mass * ParametriUtente.Science.m_sun) / volume;
			if(density > hydrogen.density)
			{
					
			}
			double hidro_2_helium_rate = 2.50; //70,28,2 Hi,he,stuff
			gen = new Random();

			double hidroperc = (0.70- (gen.NextDouble() * (0.70/7.5)));
			gen = new Random();

			double heliumperc = (0.28 - (gen.NextDouble() * (0.28/4)));
			double hidroMass = (mass * ParametriUtente.Science.m_sun) * hidroperc;
			double heliumMass = (mass * ParametriUtente.Science.m_sun) * heliumperc;

			int x = 0;

			metallicity = 1 - hidroperc - heliumperc;

		}


	}
}
