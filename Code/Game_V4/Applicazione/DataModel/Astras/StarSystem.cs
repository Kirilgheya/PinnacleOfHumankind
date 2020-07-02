﻿using MainGame.Applicazione.Engine;
using System;
using System.Collections.Generic;


namespace MainGame.Applicazione.DataModel
{
    public class StarSystem
    {
        private StarSystem sibling=null;
        private double distanceFromSibling = 0.0;
        protected Star star;
        
        protected List<Planet> planets = new List<Planet>();
        protected List<Asteroid> asteroidBelt = new List<Asteroid>();

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
        public bool hasSibling()
        {

            bool returnValue = this.sibling == null ? false : true;

            return returnValue;
        }
        public void setSibling(StarSystem sibling,double distance)
        {
            if(this.sibling != null )
            {
                throw new Exception("Il sistema contentente: " + this.star.FullName + " ha già una stella sorella");
            }

            this.sibling = sibling;
            this.distanceFromSibling = distance;

            if (!sibling.hasSibling())
            { 
                sibling.setSibling(this, distance);
            }
        }

        public void createStarSystem()
        {
            int minSupportedPlanet = 0, supportedPlanets;

            double habitableZone_min;
            double habitableZone_max;
            double jupMass_EarthRadii = 11.209;
            double multiplierFactor = 1;
            double radiiMultiplierFactor = 1;
            double asteroidBeltDistanceMax = 0.0, asteroidBeltDistanceMin =0.0;
            int[] supportedAsteroids = new int[] { 100, 2000 };
            Random_Extension randomSeed = new Random_Extension();

            Star star = new Star(this.starRadius, 0, this.composition.get_elements());
            

            star.initStar(star_densityMul, this.starRelativeMass, this.composition.get_percentage());
            this.star = star;

            //This is measured in AU 
            habitableZone_min = Math.Sqrt(this.star.relluminosity/1.1);
            habitableZone_max = Math.Sqrt((this.star.relluminosity/0.53));
            //everything beyond habitableZone_max has (should have) less than 0° surface temp and be either rocky(frozen) or gas giant
            //everything beyond habitableZone_min has (should have) more than 40° surfacete temp and can be only a rocky barren planet.

            randomSeed = new Random_Extension();
            
            asteroidBeltDistanceMax = randomSeed.NextDouble(habitableZone_min * 0.3, habitableZone_max * 10) + ((habitableZone_max-habitableZone_min)/2);
            asteroidBeltDistanceMin = asteroidBeltDistanceMax - (habitableZone_max - habitableZone_min);
            Double radii;
            
            int c = 0;


            Double distance = 0;

            multiplierFactor = 5;
            c = 0;


            while (c< maxSupportedPlanets)
            {

                radii = randomSeed.NextDouble() 
                                * ( (jupMass_EarthRadii  * radiiMultiplierFactor) - 0.5 ) + 0.5 ;
               
                c++;
               
                
                if (radii > 10)
                {
                    radiiMultiplierFactor = 0.7;
                }

                if(c>= (int)(this.maxSupportedPlanets/2))
                {
                    multiplierFactor = 2;
                }

                distance = (randomSeed.NextDouble() * ((multiplierFactor * habitableZone_max) - 0.1) + (0.1));



                ChemicalComposition chemicalComposition;

                if (distance > habitableZone_max)
                {
                    //more chances of a cold gas giant lesser chance of a cold icy planet
                    int planetType = randomSeed.Next(1, 5);
                    if(planetType < 2)
                    {
                        chemicalComposition = new ChemicalComposition(DataEngine.rockyPlanetSeed
                                            , SimulationEngine.generateNPercentages(DataEngine.rockyPlanetSeed.Count));
                        //icy
                    }
                    else
                    {
                        chemicalComposition = new ChemicalComposition(DataEngine.gasPlanetSeed
                                            , SimulationEngine.generateNPercentages(DataEngine.gasPlanetSeed.Count));
                       
                        //gas giant
                    }

                }
                else if(distance < habitableZone_min)
                {
                    //more chances of hot small-to-medium rocky-metallic planet
                    int planetType = randomSeed.Next(1, 5);
                    if (planetType < 2)
                    {
                        chemicalComposition = new ChemicalComposition(DataEngine.rockyPlanetSeed
                                            , SimulationEngine.generateNPercentages(DataEngine.rockyPlanetSeed.Count));
                        if(radii>5)
                        {
                            radii = radii / (randomSeed.Next(2, 5));
                        }
                        //rocky with atmosphere
                    }
                    else
                    {
                        chemicalComposition = new ChemicalComposition(DataEngine.rockyPlanetSeed
                                            , SimulationEngine.generateNPercentages(DataEngine.rockyPlanetSeed.Count));
                        //rocky without atmosphere
                    } 
                }
                else
                {
                    //slight more chances of a planet with liquid H2O
                    int planetType = randomSeed.Next(1, 5);
                    if (planetType < 2)
                    {
                        chemicalComposition = new ChemicalComposition(DataEngine.rockyPlanetSeed
                                            , SimulationEngine.generateNPercentages(DataEngine.rockyPlanetSeed.Count));
                        //generic rocky planet
                    }
                    else
                    {
                        chemicalComposition = new ChemicalComposition(DataEngine.ironPlanetSeed
                                            , SimulationEngine.generateNPercentages(DataEngine.ironPlanetSeed.Count));
                        //h20
                    }
                }

                Planet createdPlanet;

                if (radii<9)
                {
                    createdPlanet = SimulationEngine.createPlanet(chemicalComposition, radii, distance);
                }
                else
                {
                    createdPlanet = SimulationEngine.createGasGiant(chemicalComposition, radii, distance);
                }
                
                this.planets.Add(createdPlanet);
                c++;
            }

            int numberOfAsteroid = randomSeed.Next(supportedAsteroids[0], supportedAsteroids[1]);

            for(int i = 0;i<numberOfAsteroid;i++)
            {

                ChemicalComposition chemicalComposition = null;
                chemicalComposition = new ChemicalComposition(DataEngine.carbonAsteroidSeed,
                                             SimulationEngine.generateNPercentages(DataEngine.carbonAsteroidSeed.Count,50));
                
                int seedGen = randomSeed.Next(1, 20);
                
                if(seedGen < 19 && seedGen>15)
                {

                    chemicalComposition = new ChemicalComposition(DataEngine.siliconAsteroidSeed,
                                             SimulationEngine.generateNPercentages(DataEngine.carbonAsteroidSeed.Count, 50));
                }
                else
                {

                    chemicalComposition = new ChemicalComposition(DataEngine.carbonAsteroidSeed,
                                             SimulationEngine.generateNPercentages(DataEngine.carbonAsteroidSeed.Count, 50));
                }
               

                int radius_Km = randomSeed.Next(1, 500);
                double relMassSeed = 0.0004;
                relMassSeed = relMassSeed / (500 % radius_Km);
                Asteroid asteroid = new Asteroid(chemicalComposition, radius_Km, randomSeed.NextDouble(asteroidBeltDistanceMin,asteroidBeltDistanceMax));
                asteroid.initAsteroid(1, relMassSeed);
                this.asteroidBelt.Add(asteroid);
            }

            Console.WriteLine("AU min: " + habitableZone_min + " \n \tAU max: " + habitableZone_max);


        }

        public string toString()
        {
            string formattedInfo = "";

            formattedInfo = this.star.toString();

            foreach(Planet planet in this.planets)
            {
                formattedInfo = formattedInfo + "\n" + planet.ToString();
            }

            foreach(Asteroid asteroid in this.asteroidBelt)
            {

                formattedInfo = formattedInfo + "\n" + asteroid.ToString();
            }

            return formattedInfo;
        }
    }
}
