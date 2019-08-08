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

            Random randomSeed = new Random();

            supportedPlanets = randomSeed.Next(minSupportedPlanet, this.maxSupportedPlanets);

            double habitableZone_min;
            double habitableZone_max;

            //This is measured in AU 
            habitableZone_min = Math.Sqrt(this.star.relluminosity/1.1);
            habitableZone_max = Math.Sqrt((this.star.relluminosity/0.53));
            //everything beyond habitableZone_max has (should have) less than 0° surface temp and be either rocky(frozen) or gas giant
            //everything beyond habitableZone_min has (should have) more than 40° surfacete temp and can be only a rocky barren planet.

            randomSeed = new Random();

            Double[] radii = new double[this.maxSupportedPlanets];
            double jupMass_EarthRadii = 11.209, incremento = 15;
            int c = 0;
            while(c<radii.Length)
            {

                radii[c] = (randomSeed.NextDouble() * (((jupMass_EarthRadii ) * incremento) - (0.5 )) + (0.5 ));
                                                        
                c++;
                if (radii[c - 1] > 10)
                {
                    incremento = 1.2;
                }
            }

            Double[] distance = new double[this.maxSupportedPlanets];
            
            c = 0;
            while (c < radii.Length)
            {

                distance[c] = (randomSeed.NextDouble() * (((10*habitableZone_max) - 0.1) + (0.1)));

                c++;



                ChemicalComposition chemicalComposition = new ChemicalComposition(this.star.starComposition.stellarCompositionMats
                                            , this.star.starComposition.elementsDistribution); ;

                if (distance[c] > habitableZone_max)
                {
                    //more chances of a cold gas giant less chance of a cold icy planet
                    int planetType = randomSeed.Next(1, 5);
                    if(planetType < 1)
                    {
                        //call simulationEngine.createGasGiant()
                        //icy
                    }
                    else
                    {
                        //call simulationEngine.createGasGiant()
                        //gas giant
                    }
                    
                }
                else if(distance[c] < habitableZone_min)
                {
                    //more chances of hot small-to-medium rocky-metallic planet
                    int planetType = randomSeed.Next(1, 5);
                    if (planetType < 2)
                    {
                        //call simulationEngine.createMediumRocky-gas()
                        //icy
                    }
                    else
                    {
                        //call simulationEngine.createMediumRocky()
                        //gas giant
                    } 
                }
                else
                {
                    //slight more chances of a planet with liquid H2O
                    int planetType = randomSeed.Next(1, 5);
                    if (planetType < 2)
                    {
                        //call simulationEngine.createGenericPlanet()
                        //icy
                    }
                    else
                    {
                        //call simulationEngine.createHabitablePlanet()
                        //gas giant
                    }
                }
            }

            
            Console.WriteLine("AU min: " + habitableZone_min + " \n \tAU max: " + habitableZone_max);


        }
    }
}
