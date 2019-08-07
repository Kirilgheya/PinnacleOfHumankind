using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainGame.Applicazione.DataModel
{
    class StarSystem
    {

        protected Star star;
        
        protected List<Planet> planets;

        double star_densityMul = 1;
        double starRadius = ParametriUtente.Science.r_sun;
        double starRelativeMass = 1;
        ChemicalComposition composition;
        int maxSupportedPlanets = 10;
        public void InitSystemParams(double[] _parameters, ChemicalComposition _composition)
        {

            this.star_densityMul = _parameters[0];
            this.starRadius = _parameters[1];
            this.starRelativeMass = _parameters[2];
            this.composition = _composition;
        }

        public void createStarSystem()
        {
            int minSupportedPlanet = 0;
            int supportedPlanets;
            Star star = new Star(this.starRadius, 0, this.composition.stellarCompositionMats);
            double metallicityFactor;

            star.initStar(star_densityMul, this.starRelativeMass, this.composition.elementsDistribution);
            this.star = star;

            metallicityFactor = this.star.Metallicity;

            Random randomNumberOfplanets = new Random();

            supportedPlanets = randomNumberOfplanets.Next(minSupportedPlanet, this.maxSupportedPlanets);

            double habitableZone_min;
            double habitableZone_max;

            //This is measured in AU 
            habitableZone_min = Math.Sqrt(this.star.relluminosity/1.1);
            habitableZone_max = Math.Sqrt((this.star.relluminosity/0.53));
            //everything beyond habitableZone_max has (should have) less than 0° surface temp and be either rocky(frozen) or gas giant
            //everything beyond habitableZone_min has (should have) more than 40° surfacete temp and can be only a rocky barren planet.

            randomNumberOfplanets = new Random();

            Double[] radii = new double[this.maxSupportedPlanets];
            double jupMass_EarthRadii = 11.209, incremento = 15;
            int c = 0;
            while(c<radii.Length)
            {

                radii[c] = (randomNumberOfplanets.NextDouble() * (((jupMass_EarthRadii ) * incremento) - (0.5 )) + (0.5 ));
                                                        
                c++;
                if (radii[c - 1] > 10)
                {
                    incremento = 1.2;
                }
            }

            Console.WriteLine("AU min: " + habitableZone_min + " \n \tAU max: " + habitableZone_max);


        }
    }
}
