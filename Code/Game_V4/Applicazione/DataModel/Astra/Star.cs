using MainGame.Applicazione.Engine;
using MainGame.Applicazione.Engine.Math_Engine;
using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
namespace MainGame.Applicazione.DataModel
{
    public class Star : Body
	{

		// Luminosity, Size and temperature returns the "apparent colour" of the Star
		protected double luminosity;
		public double relluminosity { get; set; }
        protected double relCoretemperature;
        protected double relSurfacetemperature;
        protected ChemicalComposition starComposition;
        private double metallicity;
		protected double age;
        protected double meanDensity;
        protected Boolean markAsDegenerateStar = false;
        protected List<ChemicalElement> stellarCompositionMats;
        protected List<double> elementsDistribution;
        static Random_Extension randomSeed = SimulationEngine.random;
        protected double starRadius;
        private double StarMass;
        public double distanceFromCenter { get; set; }
        public double Radius { get { return this.relativeRadius * ParametriUtente.Science.r_sun; } set { this.starRadius = value; } }
        public double AvgDensity { get { return _relativeAvgDensity * ParametriUtente.Science.avg_d_sun; } set { _relativeAvgDensity = value / ParametriUtente.Science.avg_d_sun; } }
        public double RelativeAvgDensity { get { return _relativeAvgDensity; } set { _relativeAvgDensity = value; } }

        public double Mass
        {
            get { return StarMass; }
            set { this.StarMass = value; this.relativeMass = (value / 100) / ParametriUtente.Science.m_sun; }
        }

        public double RelativeMass
        {
            get { return relativeMass; }
            set { this.relativeMass = value; }
        }
        public double equilibriumFactor;

        public ChemicalComposition StarComposition { get { return starComposition; } }
        public OverallStarClassification StarClass { get { return overallClass; } }
        public StarClassification_byColor StarColor { get { return starClassification_ByColor; } }

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
        public StarClassification_byColor starClassification_ByColor;
        
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

        public Boolean isStarABlackHole()
        {

            return this.markAsDegenerateStar;
        }

        public new double getSchwarzschildRadius()
        {
            // Rg = 2GM/c2.

            Double radius=0.0;

            radius = ((2 * ParametriUtente.Science.G * this.Mass) / Math.Pow(ParametriUtente.Science.c,2) ) /1000 ; //km

            return radius;
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
			this.Surface_temperature = _surfaceTemperature;
			this.relluminosity = _relluminosity;
			this.overallClass = (OverallStarClassification)Enum.ToObject(typeof(OverallStarClassification), _class);
		}

        public String ToString_Info()
        {
            string formattedInfo= "";

            formattedInfo+= "\nStar Name: " + this.FullName;
            formattedInfo+= "\n\tRadius: " + this.relativeRadius;
            formattedInfo+= "\n\tMass: " + this.relativeMass + " "+Converter.getUOMFromName("Massa solare");
            formattedInfo+= "\n\tDensity: " + this.meanDensity;
            formattedInfo += "\n\tDistance: " + this.distanceFromCenter + " AU";
            formattedInfo += "\n\tLuminosity: " + this.relluminosity + " L⊙";
            formattedInfo += "\n\tLuminosity Class: " + this.luminosityClass + " ";
            formattedInfo += "\n\tMass Class: " + this.massClass + "";
            formattedInfo += "\n\tTemperature Class: " + this.starClassification_ByColor + "";
            formattedInfo += "\n\tEffective(surf.) Temperature: " + this.Surface_temperature;
            formattedInfo += "\n\tStar Class: " + this.overallClass.ToString();
            formattedInfo += "\n\tVega-relative chromaticity: " + this.starClassification_ByColor.ToString();
            formattedInfo += "\n\t" + this.starComposition.ToString();
           

            return formattedInfo;
        }

        public Star(
				Star _star
			)
		{
			this.Core_temperature = _star.Core_temperature;
			this.stellarCompositionMats = _star.stellarCompositionMats;
			this.starRadius = _star.starRadius;
		}

        public Star()
        {
        }

        public void initStar(double _densityMul = 1.0,double rel_mass=1.0,List<double> percentage = null)
		{
           
            ChemicalComposition chemicalComposition = new ChemicalComposition(this.stellarCompositionMats, percentage);
            OverallStarClassification forcedClass;
       
            int randomGenForNonOrdinaryStars;
            double pressione;
           
            this.meanDensity = 0;
            this.starComposition = chemicalComposition;
            elementsDistribution = percentage;

            randomGenForNonOrdinaryStars = randomSeed.Next(0, 100);


            if (randomGenForNonOrdinaryStars > 90)
            {

                this.starRadius = this.getSchwarzschildRadius();
                this.markAsDegenerateStar = true;
                forcedClass = OverallStarClassification.BlackHole;
            }
            else if (randomGenForNonOrdinaryStars > 70)
            {

                this.markAsDegenerateStar = true;
                this.starRadius = this.starRadius / 10;
                rel_mass = randomSeed.NextDouble(0.17, 1.4);
                this.Radius = randomSeed.NextDouble(0.008, 0.02) * ParametriUtente.Science.r_sun;
                forcedClass = OverallStarClassification.WhiteDwarf;
            }
            else
            {

                forcedClass = OverallStarClassification.None;
            }

            this.Volume = Formula.VolumeOfSphere(this.starRadius); //km3

            this.Mass = rel_mass * ParametriUtente.Science.m_sun;

            this.luminosity = SimulationEngine.getLuminosityFromMass(this.Mass);

            this.meanDensity = Formula.Density(this.Mass*1000, (Math.Pow(10,15)*Volume)) * _densityMul;
       
            
            pressione = ((ParametriUtente.Science.G 
                                * Mass
                                * (Converter.gcm3_to_kgm3(this.meanDensity)))
                          / (this.starRadius*1000));

            this.Core_temperature = ((0.84 * Math.Pow(10, -27)) * pressione)
                                    / ((Converter.gcm3_to_kgm3(this.meanDensity))
                                            * (1.380649 * Math.Pow(10, -23)));

            this.Core_temperature = this.Core_temperature * 1.3;


            this.Surface_temperature = SimulationEngine.getTemperatureFromLumRadiusRatio(this.starRadius, this.luminosity);//this.Core_temperature / (2717.203184);//2543.37;

            this.setRelativeValues();

            this.InitStarClassification(forcedClass);

            this.finalizeStar();
        }

        private void setRelativeValues()
        {
            this.relativeRadius = this.starRadius / ParametriUtente.Science.r_sun;
            this._relativeAvgDensity = this.meanDensity / ParametriUtente.Science.avg_d_sun;
            this.relativeMass = this.Mass / ParametriUtente.Science.m_sun;
            this.relCoretemperature = this.Core_temperature / ParametriUtente.Science.coretemp_sun;
            this.relativeVolume = this.Volume/ ParametriUtente.Science.v_sun;
            this.relluminosity = this.luminosity / ParametriUtente.Science.lum_sun;
            this.relSurfacetemperature = this.Surface_temperature / ParametriUtente.Science.surfacetemp_sun;
            this.setMetallicity();
        }

        

        private void InitStarClassification(OverallStarClassification forcedClass = OverallStarClassification.None)
        {

            int temperature = (int) this.Surface_temperature;
            this.luminosityClass = Star.FindStarClass(this.relluminosity);

            int relmassClass = (int)(this.relativeMass * 100);
            
            if(this.starRadius > this.getSchwarzschildRadius())
            { 
                this.massClass = Star.FindStarMassClass(relmassClass);
              
                this.overallClass = Star.mapClassificationsToOverallClassification(this.massClass, this.luminosityClass, forcedClass);

                this.starClassification_ByColor = Star.FindStarColor(temperature);

            }
            else
            {

                this.massClass = StarClassification_byMass.BlackHole;
                this.starClassification_ByColor = StarClassification_byColor.BlackHole;
                this.overallClass = OverallStarClassification.BlackHole;
            }

        }

        private static StarClassification_byColor FindStarColor(int lumLevel)
        {
            return Enum.GetValues(typeof(StarClassification_byColor)).Cast<int>().Where(stellarClass
                            => lumLevel >= stellarClass).Cast<StarClassification_byColor>().Max<StarClassification_byColor>();

        }

        public StarClassification_byColor getColor()
        {
            return this.starClassification_ByColor;
        }

        private void finalizeStar()
        {
            if(String.IsNullOrEmpty(baseName))
            {
                Random random = SimulationEngine.random;
                baseName = ((int)(random.NextDouble() * 10000))+" ";
            }
            string extendedName = baseName
                                + this.luminosityClass.ToString() + "/"
                                + this.massClass.ToString() + " "
                                + this.overallClass.ToString()
                                + " Star";
            this.extendedName = extendedName;
        }

        public static StarClassification_byLum FindStarClass(double lumLevel)
        {
            double localLumlevel = lumLevel * 100;
            return Enum.GetValues(typeof(StarClassification_byLum)).Cast<int>().Where(stellarClass 
                                    => (localLumlevel) >= stellarClass).Cast<StarClassification_byLum>().Max();
        }

        public static StarClassification_byMass FindStarMassClass(int relMass)
        {

            return Enum.GetValues(typeof(StarClassification_byMass)).Cast<int>().Where(stellarClass
                                    => relMass >= stellarClass).Cast<StarClassification_byMass>().Max();
        }

        public static OverallStarClassification mapClassificationsToOverallClassification(StarClassification_byMass massClass,
                                StarClassification_byLum luminosityClass, OverallStarClassification forcedClass = OverallStarClassification.None)
        {

            OverallStarClassification overallClass = OverallStarClassification.None;


            if (forcedClass == OverallStarClassification.None)
            {

                switch (massClass)
                {
                    case StarClassification_byMass.T:
                        int i = 0;
                        break;
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
                            overallClass = OverallStarClassification.WhiteDwarf;
                        }
                        else
                        {
                            overallClass = OverallStarClassification.MainSequenceDwarf;
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
                    case StarClassification_byMass.BlackHole:
                        if (luminosityClass == StarClassification_byLum.BlackHole)
                        {

                            overallClass = OverallStarClassification.BlackHole;
                        }
                        break;

                }
            }
            else
            {

                overallClass = forcedClass;
            }

            return overallClass;
        }
    }
}
