using MainGame.Applicazione.Engine;
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
            double metallicityFactor;
            double habitableZone_min;
            double habitableZone_max;
            double jupMass_EarthRadii = 11.209;
            double multiplierFactor = 1;

            Random randomSeed = new Random();

            Star star = new Star(this.starRadius, 0, this.composition.get_elements());
            

            star.initStar(star_densityMul, this.starRelativeMass, this.composition.get_percentage());
            this.star = star;

            metallicityFactor = this.star.Metallicity;

           

            supportedPlanets = randomSeed.Next(minSupportedPlanet, this.maxSupportedPlanets);

           

            //This is measured in AU 
            habitableZone_min = Math.Sqrt(this.star.relluminosity/1.1);
            habitableZone_max = Math.Sqrt((this.star.relluminosity/0.53));
            //everything beyond habitableZone_max has (should have) less than 0° surface temp and be either rocky(frozen) or gas giant
            //everything beyond habitableZone_min has (should have) more than 40° surfacete temp and can be only a rocky barren planet.

            randomSeed = new Random();

            Double[] radii = new double[this.maxSupportedPlanets];
            
            int c = 0;
            while(c< this.maxSupportedPlanets)
            {

                radii[c] = randomSeed.NextDouble() 
                                * ( (jupMass_EarthRadii  * multiplierFactor) - 0.5 ) + 0.5 ;
                                                        
                c++;
                if (radii[c - 1] > 10)
                {
                    multiplierFactor = 0.7;
                }
            }
           
            Double[] distance = new double[this.maxSupportedPlanets];

            multiplierFactor = 5;
            c = 0;

            while (c < this.maxSupportedPlanets)
            {

                if(c>= (int)(this.maxSupportedPlanets/2))
                {
                    multiplierFactor = 2;
                }

                distance[c] = (randomSeed.NextDouble() * ((multiplierFactor * habitableZone_max) - 0.1) + (0.1));



                ChemicalComposition chemicalComposition = null;

                if (distance[c] > habitableZone_max)
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
                else if(distance[c] < habitableZone_min)
                {
                    //more chances of hot small-to-medium rocky-metallic planet
                    int planetType = randomSeed.Next(1, 5);
                    if (planetType < 2)
                    {
                        chemicalComposition = new ChemicalComposition(DataEngine.rockyPlanetSeed
                                            , SimulationEngine.generateNPercentages(DataEngine.rockyPlanetSeed.Count));
                        if(radii[c]>5)
                        {
                            radii[c] = radii[c] / (randomSeed.Next(2, 5));
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

                Planet x;

                if (radii[c]<9)
                {
                    x = SimulationEngine.createPlanet(chemicalComposition, radii[c], distance[c]);
                }
                else
                {
                    x = SimulationEngine.createGasGiant(chemicalComposition, radii[c], distance[c]);
                }
                
                this.planets.Add(x);
                c++;
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

            return formattedInfo;
        }
    }
}
