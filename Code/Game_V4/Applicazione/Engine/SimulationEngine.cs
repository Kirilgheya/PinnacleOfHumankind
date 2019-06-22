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
        public static Boolean mustShowInfo = false;
        public static List<Star>  resultOfGenerateStar;
        protected static Stopwatch watch;
        public static void generateStars(int _number=0,List<ChemicalElement> chemicalElements = null
                                            , List<int> percentage = null)
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

                double radius = 700000;
                double startingRadius = radius;

                double tolerance =1E-15;
                Star star = new Star(radius,0, chemicalElements);
                
                
                double densityMul = 1.5;

            

                

                star = new Star(radius, 0, chemicalElements);
                star.initStar(densityMul, percentage);
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
