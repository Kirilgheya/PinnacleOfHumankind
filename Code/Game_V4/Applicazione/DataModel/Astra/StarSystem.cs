using MainGame.Applicazione.DataModel.Astra;
using MainGame.Applicazione.Engine;
using System;
using System.Collections.Generic;


namespace MainGame.Applicazione.DataModel
{
    public class StarSystem
    {
        private StarSystem sibling=null;
        private double distanceFromSibling = 0.0;
        protected StarSystemCenter stars;
        
        //alpha
        public String Name { get { return stars.getFullName(); } }
        public List<Planet> Planets { get { return planets; } }
        protected List<Planet> planets = new List<Planet>();

        public List<Asteroid> asteroids { get { return asteroidBelt; } }
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

        public Star[] getStars()
        {

           return this.stars.getStars();
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
                throw new Exception("Il sistema contentente: " + this.stars.getFullName() + " ha già una stella sorella");
            }

            this.sibling = sibling;
            this.distanceFromSibling = distance;

            if (!sibling.hasSibling())
            { 
                sibling.setSibling(this, distance);
            }
        }

        

        private void createStarCenter()
        {
            Random_Extension random = new Random_Extension();
            Star star = new Star(this.starRadius, 0, this.composition.get_elements());


            star.initStar(star_densityMul, this.starRelativeMass, this.composition.get_percentage());

            int seed = random.Next(0, 100);

            if(seed >= 90)
            {
                this.stars = new TernaryStarSystemCenter();
            }
            else if (seed >= 70)
            {
                this.stars = new BinaryStarSystemCenter();
            }
            else
            {
                this.stars = new UnaryStarSystemCenter();
            }


         
         
            this.stars.addStar(star);
            

           

            while(stars.canHaveMore()>0)
            {
                star = new Star(ParametriUtente.Science.r_sun / 3, 0, this.composition.get_elements());
                star.initStar(star_densityMul, 1.0 / 2.0, this.composition.get_percentage());
                this.stars.addStar(star);
            }
           
          
            this.stars.setBarycenter();
        }

        public void createStarSystem()
        {
            int supportedPlanets;

            double habitableZone_min;
            double habitableZone_max;
            double jupMass_EarthRadii = 11.209;
            double multiplierFactor = 1;
            double radiiMultiplierFactor = 1;
            double asteroidBeltDistanceMax = 0.0, asteroidBeltDistanceMin =0.0;
            int[] supportedAsteroids = new int[] { 100, 2000 };
            Random_Extension randomSeed = new Random_Extension();

            this.createStarCenter();
            

            //This is measured in AU 
            habitableZone_min = Math.Sqrt(this.stars.getRelLuminosity()/1.1);
            habitableZone_max = Math.Sqrt((this.stars.getRelLuminosity()/ 0.53));
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

                Random_Extension re = new Random_Extension();

                if (re.Next(0, 10) >= 7)
                {
                    c++;
                }

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

            formattedInfo = this.stars.getFullName();

            formattedInfo = "\n Distance A-B: " + this.stars.getDistances()[0] + " - " + this.stars.getDistances()[1];

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
