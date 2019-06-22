using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using MainGame.Applicazione.Engine;
using org.mariuszgromada.math.mxparser;
namespace MainGame.Applicazione.DataModel
{
	public class Star : Body
	{

		// Luminosity, Size and temperature returns the "apparent colour" of the Star
		protected double luminosity;
		protected double relluminosity;
		protected double metallicity;
		protected double age;
        public double meanDensity;
        protected List<ChemicalElement> stellarCompositionMats;
        protected double starRadius;
        private double StarMass;
        public double mass { get { return StarMass; }
                             set { this.StarMass = value; this.relativeMass = (value/100) / Science.m_sun; }
                            }
        public double equilibriumFactor;
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

		


		public Star(
				double _starRadius,
				double _temperature = 0.0,
				List<ChemicalElement> _stellarCompositionMats = null
				
			)
		{

			this.Core_temperature = _temperature;
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
			this.Core_temperature = _surfaceTemperature;
			this.relluminosity = _relluminosity;
			this.luminosityClass = (StarClass)Enum.ToObject(typeof(StarClass), _class);
		}

		public Star(
				Star _star
			)
		{
			this.Core_temperature = _star.Core_temperature;
			this.stellarCompositionMats = _star.stellarCompositionMats;
			this.starRadius = _star.starRadius;
		}

		public void initStar(double _densityMul = 1.0,List<int> percentage = null)
		{

            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            Function hydrostaticEquilibrium = ParametriUtente.Science.hydrostaticEquilibrium;

            double h = 6;
            meanDensity = 2;

            //mass in grammi / 18.015 = moles
            //ideal gas law
            double molecularWeight = 0.0;
            double weight = 1;
            foreach(ChemicalElement element in stellarCompositionMats)
            {
                weight = weight + percentage.ToArray()[stellarCompositionMats.IndexOf(element)];
                meanDensity = meanDensity + element.density * (percentage.ToArray()[stellarCompositionMats.IndexOf(element)]) 
                                                / weight;
                molecularWeight = molecularWeight + element.mass / 2;
            }
            meanDensity = meanDensity* _densityMul;
          
            double mass = ((Math.Pow(this.starRadius*1000*100, 3) * (4 / 3) * Math.PI) * meanDensity);
            this.mass = mass;

            double pressione = (ParametriUtente.Science.G 
                                * mass
                                * meanDensity
                                / this.starRadius);
       
            h = starRadius; //metri
            

            meanDensity = mass /((4 / 3) * Math.PI * (Math.Pow(this.starRadius * 1000 * 100, 3)));

            this.Core_temperature = molecularWeight * pressione / meanDensity / 1000 * 8.314462618;

        }

		public bool isInEquilibrium(double tolerance = 0.0)
        {

            bool isInEq = false;

            if(this.equilibriumFactor < tolerance && this.equilibriumFactor > (tolerance*(-1)))
            {

                isInEq = true;
            }

            return isInEq;
        }

	}
}
