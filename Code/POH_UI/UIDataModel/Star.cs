using System;
using System.Collections.Generic;
using System.Text;
using MainGame.Applicazione.DataModel;
namespace MainGame.UI.DataModel
{
	public class Star : MainGame.Applicazione.DataModel.Star
	{

		// Luminosity, Size and temperature returns the "apparent colour" of the Star
		public string Luminosity { get; set; }
	
		public string Metallicity { get; set; }
		public string Age { get; set; }
		public string Radius { get; set; }

		public string Temperature;
		public Star (
				double _starRadius,
				double _temperature = 0.0,
				List<Applicazione.DataModel.ChemicalElement> _stellarCompositionMats = null

			) : base(_starRadius, _temperature, _stellarCompositionMats)

		{
			this.Temperature = _temperature.ToString();
			
			this.Radius = _starRadius.ToString();
			
		}

		public Star(MainGame.Applicazione.DataModel.Star _star) :  base(_star)
			

		{
			this.temperature = base.temperature.ToString();
			this.Luminosity = base.luminosityClass.ToString();
			this.Age = base.age.ToString();
			this.Radius = base.starRadius.ToString();

		}


		public Star(
				double _relluminosity,
				double _surfaceTemperature,
				double _relmass,
				int _class
			) : base(_relluminosity, _surfaceTemperature, _relmass, _class)
		{

			this.relativeMass = _relmass;
			this.temperature = _surfaceTemperature.ToString();
		
			this.luminosityClass = (StarClass)Enum.ToObject(typeof(StarClass), _class);
		}


		private string temperature;
		private List<ChemicalElement> stellarCompositionMats;
		public string starRadius;


		
			public new void initStar()
		{

			
		}

		

	}
}
