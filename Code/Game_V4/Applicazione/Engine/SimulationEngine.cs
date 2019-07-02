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
              
            

            

                

                star = new Star(radius, 0, chemicalElements);
                star.initStar(densityMul,1, percentage);
                resultOfGenerateStar.Add(star);
            }


            if (mustShowInfo && watch.IsRunning)
            {

                double seconds = ((double)watch.ElapsedMilliseconds / 1000);

                Console.WriteLine("Time for calc: " + seconds );
             
                watch.Reset();
            }

        }
    }
}
