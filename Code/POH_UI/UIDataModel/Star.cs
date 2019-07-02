﻿using System;
using System.Collections.Generic;
using System.Text;
using MainGame.Applicazione.DataModel;
namespace MainGame.UI.DataModel
{
	public class Star : Applicazione.DataModel.Star
	{

		// Luminosity, Size and temperature returns the "apparent colour" of the Star
		public string Luminosity { get; set; }
	
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

		public Star(Applicazione.DataModel.Star _star) :  base(_star)
			

		{
			this.temperature = base.Core_temperature.ToString();
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
			this.surface_temperature = _surfaceTemperature.ToString();
		
			this.luminosityClass = (LuminosityClassification)Enum.ToObject(typeof(LuminosityClassification), _class);
		}


		private string surface_temperature;
		private List<ChemicalElement> stellarCompositionMats;
		public string starRadius;


		
			public new void initStar()
		{

			
		}

		

	}
}
