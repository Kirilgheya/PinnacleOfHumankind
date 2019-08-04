using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using org.mariuszgromada.math.mxparser;
namespace MainGame.Applicazione.DataModel
{
    public class Star : Body
	{

		// Luminosity, Size and temperature returns the "apparent colour" of the Star
		protected double luminosity;
		public double relluminosity { get; set; }
        protected double relCoretemperature;
        protected double relSurfacetemperature;
        private double metallicity;
		protected double age;
        public double meanDensity;
        protected List<ChemicalElement> stellarCompositionMats;
        protected List<double> elementsDistribution;
        protected double starRadius;
        private double StarMass;
        private string baseName;
        private string extendedName;
        public string FullName { get {
                if (String.IsNullOrEmpty(extendedName))
                {
                    this.finalizeStar();
                }
                return this.extendedName;
            }
            set { this.finalizeStar(); }
        }

        public StarClassification_byLum luminosityClass;
        public StarClassification_byMass massClass;
        public OverallStarClassification overallClass;
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
	
        public override string ToString()
        {
            
            return extendedName;

        }

		private void setMetallicity()
        {
            int c = 1;
            foreach(double perc in this.elementsDistribution)
            {
                if(c<=2)
                {
                    c++;
                    continue;
                }

                this.metallicity += perc;
                
            }
            
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
                double _solarMasses,
                double _solarRadii,
                List<ChemicalElement> _stellarCompositionMats,
                List<double> distribution

            )
        {

       
            this.stellarCompositionMats = _stellarCompositionMats;
            this.StarMass = _solarMasses*ParametriUtente.Science.m_sun;
            this.starRadius = _solarRadii * ParametriUtente.Science.r_sun;
            this.initStar(1, _solarMasses, distribution);
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
			this.overallClass = (OverallStarClassification)Enum.ToObject(typeof(OverallStarClassification), _class);
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
                                        ((this.meanDensity * Math.Pow(10, 8)) 
                                                * (8.314462618 / (molecularWeight))*4.8
                                                ) ) 
                                            ; // - K to get °
            this.Surface_temperature = this.Core_temperature / 2543.37;
            double surfaceArea = Math.Pow(this.starRadius, 2) * Math.PI * 4;
            this.luminosity = ( (Math.Pow(10,-4)* 5.670374419) * (Math.Pow(this.Surface_temperature, 4)) * surfaceArea);

            this.setRelativeValues();

            this.InitStarClassification();

            this.finalizeStar();
        }

        private void setRelativeValues()
        {
            this.relativeAvgDensity = this.meanDensity / ParametriUtente.Science.avg_d_sun;
            this.relativeMass = this.mass / ParametriUtente.Science.m_sun;
            this.relCoretemperature = this.Core_temperature / ParametriUtente.Science.coretemp_sun;
            this.relativeVolume = this.Volume/ ParametriUtente.Science.v_sun;
            this.relluminosity = this.luminosity / ParametriUtente.Science.lum_sun;
            this.relSurfacetemperature = this.Surface_temperature / ParametriUtente.Science.surfacetemp_sun;
            this.setMetallicity();
        }

        private void InitStarClassification()
        {

            int temperature = (int) this.Surface_temperature;
            this.luminosityClass = Star.FindStarClass(temperature);

            int relmassClass = (int)(this.relativeMass * 100);
            this.massClass = Star.FindStarMassClass(relmassClass);

            this.overallClass = Star.mapClassificationsToOverallClassification(this.massClass, this.luminosityClass);
           
        }

        private void finalizeStar()
        {
            if(String.IsNullOrEmpty(baseName))
            {
                Random random = new Random();
                baseName = ((int)(random.NextDouble() * 10000))+" ";
            }
            string extendedName = baseName
                                + this.luminosityClass.ToString() + "/"
                                + this.massClass.ToString() + " "
                                + this.overallClass.ToString()
                                + " Star";
            this.extendedName = extendedName;
        }

        public static StarClassification_byLum FindStarClass(int lumLevel)
        {

            return Enum.GetValues(typeof(StarClassification_byLum)).Cast<int>().Where(stellarClass 
                                    => lumLevel >= stellarClass).Cast<StarClassification_byLum>().Last<StarClassification_byLum>();
        }

        public static StarClassification_byMass FindStarMassClass(int relMass)
        {

            return Enum.GetValues(typeof(StarClassification_byMass)).Cast<int>().Where(stellarClass
                                    => relMass >= stellarClass).Cast<StarClassification_byMass>().Last<StarClassification_byMass>();
        }

        public static OverallStarClassification mapClassificationsToOverallClassification(StarClassification_byMass massClass,
                                StarClassification_byLum luminosityClass)
        {

            OverallStarClassification overallClass = OverallStarClassification.None;

            switch (massClass)
            {
                case StarClassification_byMass.T:
                case StarClassification_byMass.L:
                case StarClassification_byMass.M:
                    if (luminosityClass > StarClassification_byLum.M)
                    {
                        overallClass = OverallStarClassification.WhiteDwarf;
                    }
                    else
                    {
                        overallClass = OverallStarClassification.BrownDwarf;
                    }
                    break;

                case StarClassification_byMass.K:
                    if (luminosityClass > StarClassification_byLum.K)
                    {
                        overallClass = OverallStarClassification.MainSequenceDwarf;
                    }
                    else
                    {
                        overallClass = OverallStarClassification.WhiteDwarf;
                    }
                    break;

                case StarClassification_byMass.G:
                    if (luminosityClass > StarClassification_byLum.G)
                    {
                        overallClass = OverallStarClassification.LesserGiant;
                    }
                    else
                    {
                        overallClass = OverallStarClassification.MainSequenceDwarf;
                    }
                    break;

                case StarClassification_byMass.F:
                    if (luminosityClass > StarClassification_byLum.F)
                    {
                        overallClass = OverallStarClassification.Giant;
                    }
                    else
                    {
                        overallClass = OverallStarClassification.LesserGiant;
                    }
                    break;

                case StarClassification_byMass.A:
                    if (luminosityClass > StarClassification_byLum.A)
                    {
                        overallClass = OverallStarClassification.BrightGiant;
                    }
                    else
                    {
                        overallClass = OverallStarClassification.Giant;
                    }
                    break;

                case StarClassification_byMass.B:
                    if (luminosityClass > StarClassification_byLum.B)
                    {
                        overallClass = OverallStarClassification.SuperGiant;
                    }
                    else
                    {
                        overallClass = OverallStarClassification.BrightGiant;
                    }
                    break;

                case StarClassification_byMass.O:
                    if (luminosityClass > StarClassification_byLum.O)
                    {
                        overallClass = OverallStarClassification.HyperGiant;
                    }
                    else
                    {
                        overallClass = OverallStarClassification.SuperGiant;
                    }
                    break;

            }

            return overallClass;
        }
    }
}
