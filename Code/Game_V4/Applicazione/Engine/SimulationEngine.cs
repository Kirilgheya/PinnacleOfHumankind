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

        public static Planet createGasGiant(ChemicalComposition _chemicalComposition)
        {
            Planet planet;
            List<double> distribution = new List<double>();
            Random random = new Random();
            double previousNumber=0;

            previousNumber = random.NextDouble() * (80 - 60) + 60;
            distribution.Add(previousNumber);

            distribution.Add(random.NextDouble() * (previousNumber - 60) + 60);

            //Gas giant model: 2 main gasses 5 gasses 3 metals 
            ChemicalElement[] otherGasses = new ChemicalElement[5];
            ChemicalElement[] mainGasses = new ChemicalElement[2];
            ChemicalElement[] metals = new ChemicalElement[3];
            mainGasses[0] = _chemicalComposition.getRandomElement_PerType(ElementState.Gas);
            mainGasses[1] = _chemicalComposition.getRandomElement_PerType(ElementState.Gas);
            while(mainGasses[1] == mainGasses[0])
            {
                mainGasses[1] = _chemicalComposition.getRandomElement_PerType(ElementState.Gas);
            }

            otherGasses[0] = _chemicalComposition.getRandomElement_PerType(ElementState.Gas);
            otherGasses[1] = _chemicalComposition.getRandomElement_PerType(ElementState.Gas);
            otherGasses[2] = _chemicalComposition.getRandomElement_PerType(ElementState.Gas);
            otherGasses[3] = _chemicalComposition.getRandomElement_PerType(ElementState.Gas);
            otherGasses[4] = _chemicalComposition.getRandomElement_PerType(ElementState.Gas);

            metals[0] = _chemicalComposition.getRandomElement_PerType(ElementState.Solid);
            metals[1] = _chemicalComposition.getRandomElement_PerType(ElementState.Solid);
            metals[2] = _chemicalComposition.getRandomElement_PerType(ElementState.Solid);
            



        }
    }
}
