﻿using MainGame.Applicazione.DataModel;
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
        public static Boolean mustShowInfo = false;
        public static List<Star>  resultOfGenerateStar;
        protected static Stopwatch watch;
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

        public static Planet createGasGiant(ChemicalComposition _chemicalComposition,double _earthRadii)
        {
            Planet planet=null;

            
            

            
            

            planet = new Planet(_chemicalComposition.stellarCompositionMats, _chemicalComposition.elementsDistribution, (_earthRadii*ParametriUtente.Science.r_t));
            planet.initPlanet(1, 120);
            return planet;
        }

        public static List<double> generateDistributionList(int _numberOfDistributions = 2, int varianzaMinima = 2)
        {

            List<double> distribution = new List<double>();
            Random random = new Random();

            double maxSum = 100;
            double minNum = 0.00001;
            double lastGeneratedNum;
            double partialSum = 0;
            double minGenerated = _numberOfDistributions;
            lastGeneratedNum = random.NextDouble() * (60 - 50) + 50;

            distribution.Add(lastGeneratedNum);
            partialSum = partialSum + lastGeneratedNum;

            for (int i = 0; i < (_numberOfDistributions - 1); i++)
            { 
                if (minNum > lastGeneratedNum)
                {
                    lastGeneratedNum = maxSum - partialSum;
                    distribution.Add(lastGeneratedNum);
                    partialSum = partialSum + lastGeneratedNum;
                }
                else
                {
                    //lastGeneratedNum = random.NextDouble() * ((100 - partialSum) - ((100 - partialSum) / varianzaMinima)) + ((100 - partialSum) / varianzaMinima);
                    lastGeneratedNum = random.NextDouble() * ((100 - partialSum) - ((100 - partialSum) / minGenerated)) + ((100 - partialSum) / minGenerated);
                    minGenerated--;
                    distribution.Add(lastGeneratedNum);
                    partialSum = partialSum + lastGeneratedNum;
                }
            }

            return distribution;
        }
    }
}
