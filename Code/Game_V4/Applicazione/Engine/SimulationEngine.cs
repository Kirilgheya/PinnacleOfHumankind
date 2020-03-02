using MainGame.Applicazione.DataModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainGame.Applicazione.Engine
{
    class SimulationEngine
    {
        private static int numeroDecimaliDefault = 7;
        public static Boolean mustShowInfo = false;
        public static List<Star>  resultOfGenerateStar;
        protected static Stopwatch watch;
        static Random random = new Random();
        public static void generateStars(int _number=0,List<ChemicalElement> chemicalElements = null
                                            , List<double> percentage = null)
        {

            if(mustShowInfo)
            {
                watch = new Stopwatch();
            }

            resultOfGenerateStar = new List<Star>();
            
            for (int i = 0;i<_number;i++)
            {

                if (mustShowInfo && !watch.IsRunning)
                {
                    watch.Start();
                }
                double densityMul = 1;
                double radius = 695700;
                double mass = 1;

                Star star = new Star(radius,0, chemicalElements);
              
                star.initStar(densityMul, mass, percentage);
                
                resultOfGenerateStar.Add(star);
            }


            if (mustShowInfo && watch.IsRunning)
            {

                double seconds = ((double)watch.ElapsedMilliseconds / 1000);

                Console.WriteLine("Time for calc: " + seconds );
             
                watch.Reset();
            }

        }

        public static Planet createGasGiant(ChemicalComposition _chemicalComposition,double _earthRadii, double distance_from_star)
        {
            Planet planet=null;
            

            int relMass = random.Next(50, 120 * 3);

            planet = new Planet(_chemicalComposition, (_earthRadii*ParametriUtente.Science.r_t), distance_from_star);
            planet.initPlanet(1, relMass);
            return planet;
        }

        public static Planet createPlanet(ChemicalComposition _chemicalComposition, double _earthRadii, double distance_from_star)
        {
            Planet planet = null;
        
            double relMass;
            if (_earthRadii>2)
            {
                relMass = random.NextDouble() * (45 - 1) + 1;
            }
            else
            {
                relMass = random.NextDouble() * (7 - _earthRadii) + _earthRadii;
            }
            
             
            planet = new Planet(_chemicalComposition, (_earthRadii * ParametriUtente.Science.r_t), distance_from_star);
            planet.initPlanet(1, relMass);
            return planet;
        }

        public static List<double> generateDistributionList(int startMax =60, int startMin = 50,
                                                   int _numberOfDistributions = 2,
                                                                double varianzaMinima = 1 / 10000000)
        {

            List<double> distribution = new List<double>();
            List<double> supportList = new List<double>();

            double sum = 100;
    
            double lastGeneratedNum;
            double partialSum = 0,accumulatedSum = 0;
    



            lastGeneratedNum = random.NextDouble() * (startMax - startMin) + startMin;
            sum = 100 - lastGeneratedNum;
            distribution.Add(lastGeneratedNum);
            

            for (int i = 0; i < (_numberOfDistributions - 1); i++)
            { 
                

                //lastGeneratedNum = random.NextDouble() * ((100 - partialSum) - ((100 - partialSum) / varianzaMinima)) + ((100 - partialSum) / varianzaMinima);
                lastGeneratedNum = random.NextDouble() ;
                supportList.Add(lastGeneratedNum);
                partialSum = partialSum + lastGeneratedNum;
             }
        

            for (int i = 0; i< supportList.Count; i++)
            {

                if( (i+1) == supportList.Count)
                {
                    distribution.Add(sum - accumulatedSum);
                }
                else
                { 
                    //lastGeneratedNum = random.NextDouble() * ((100 - partialSum) - ((100 - partialSum) / varianzaMinima)) + ((100 - partialSum) / varianzaMinima);
                    lastGeneratedNum = supportList.ElementAt(i) / partialSum;
                    accumulatedSum = accumulatedSum + lastGeneratedNum * sum;
                    distribution.Add(lastGeneratedNum * sum);
                }

            }

            //uncomment and breakpoint to debug distributionList
            //sum = 0;
            //for (int i = 0; i < distribution.Count; i++)
            //{

            //    sum = sum + distribution.ElementAt(i);
            //}

            return distribution;
        }

        public static List<double> generateNPercentages(int _numberOfDistributions = 2,
                                                    
                                                    int startingMinPercentage =60
                                                   )
        {

            List<double> distribution = new List<double>();
            
            distribution = generateDistributionList(90,
                                                    startingMinPercentage,
                                                   _numberOfDistributions
                                                    );
                      

            return distribution;
        }


        public static double getLuminosityFromMass(double _objectMass)
        {

            double relValueToSun = _objectMass / ParametriUtente.Science.m_sun;
            double luminosity;
            //https://en.wikipedia.org/wiki/Mass-luminosity_relation
            double coefficient = 0;
            double power;
            if (relValueToSun < 0.43)
            {
                coefficient = 0.23;
                power = 2.3;

            }
            else if (relValueToSun < 2)
            {

                coefficient = 1;
                power = 4;
            }
            else if (relValueToSun < 55)
            {

                coefficient = 1.4;
                power = 3.5;
            }
            else
            {

                coefficient = 32000;
                power = 1;
            }

            luminosity = Math.Pow(relValueToSun, power) * coefficient * ParametriUtente.Science.lum_sun;

            return luminosity;
        }

        public static double getTemperatureFromLumRadiusRatio(double _objectRadius,double _objectLuminosity)
        {

            double relValueToSun =  ParametriUtente.Science.r_sun / _objectRadius;
            
         
            double lumValue = Math.Pow(_objectLuminosity / ParametriUtente.Science.lum_sun, 0.5);
            
            double temperature = Math.Pow((relValueToSun * lumValue* Math.Pow(ParametriUtente.Science.surfacetemp_sun,2)),0.5);
    
            
           

            return temperature;
        }
    }

    
}
