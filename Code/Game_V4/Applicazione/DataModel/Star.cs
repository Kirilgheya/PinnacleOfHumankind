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
        protected double reltemperature;
        private double metallicity;
		protected double age;
        public double meanDensity;
        protected List<ChemicalElement> stellarCompositionMats;
        protected List<double> elementsDistribution;
        protected double starRadius;
        private double StarMass;
        public StarClassification starClass;
        public double Metallicity { get {
                                        if (this.metallicity <= 0.0)
                                        {
                                            this.setMetallicity();

                                        }
                                        return this.metallicity;
                                    }
                                    set {
                                        this.setMetallicity();
                                        }
            
                                  }
        public double mass { get { return StarMass; }
                             set { this.StarMass = value; this.relativeMass = (value/100) / Science.m_sun; }
                            }
        public double equilibriumFactor;
	

		private void setMetallicity()
        {
            int c = 1;
            foreach(int perc in this.elementsDistribution)
            {
                if(c<=2)
                {
                    c++;
                    continue;
                }

                this.metallicity = perc;
                
            }
            
        }


		public LuminosityClassification luminosityClass;
		

		


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
			this.luminosityClass = (LuminosityClassification)Enum.ToObject(typeof(LuminosityClassification), _class);
		}

		public Star(
				Star _star
			)
		{
			this.Core_temperature = _star.Core_temperature;
			this.stellarCompositionMats = _star.stellarCompositionMats;
			this.starRadius = _star.starRadius;
		}

		public void initStar(double _densityMul = 1.0,double rel_mass=1.0,List<double> percentage = null)
		{
            elementsDistribution = percentage;
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            Function hydrostaticEquilibrium = ParametriUtente.Science.hydrostaticEquilibrium;
            //mass in grammi / 18.015 = moles
            //ideal gas law
            double molecularWeight = 0.0;
            double sumofElement=0.0;
            
            double pressione;
           
            this.meanDensity = 0;

            foreach(ChemicalElement element in stellarCompositionMats)
            {
                double currentElement = elementsDistribution.ElementAt(stellarCompositionMats.IndexOf(element));
                sumofElement = sumofElement + currentElement;
                molecularWeight = (molecularWeight + (element.mass* currentElement)
                                                ) / sumofElement;
            }


            this.Volume = (Math.Pow(this.starRadius, 3) * (4 / 3) * Math.PI); //k3

            this.mass = rel_mass * ParametriUtente.Science.m_sun;

            this.meanDensity = ((this.mass/ Volume)*Math.Pow(10,-12)) * _densityMul;
       
            
            pressione = ((ParametriUtente.Science.G 
                                * mass
                                * this.meanDensity * Math.Pow(10, 3))
                                / (this.starRadius * this.starRadius))*this.starRadius;

            this.Core_temperature = (pressione / 
                                        ((this.meanDensity * Math.Pow(10, 6)) 
                                                * (8.314462618 / (molecularWeight))
                                                ) ) 
                                            ; // - K to get °
           
            this.setRelativeValues();
        }

        private void setRelativeValues()
        {
            this.relativeAvgDensity = this.meanDensity / ParametriUtente.Science.avg_d_sun;
            this.relativeMass = this.mass / ParametriUtente.Science.m_sun;
            this.reltemperature = this.Core_temperature / ParametriUtente.Science.coretemp_sun;
            this.relativeVolume = this.Volume/ ParametriUtente.Science.v_sun;
        }

	

	}
}
