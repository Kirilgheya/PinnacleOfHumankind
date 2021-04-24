using GameUI.UI.DataSource;
using GameUI.UI.DataSource.UIItems_DS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Gamecore = MainGame.Applicazione;

namespace GameUI.UI.GameEngine
{
    public static class GameEngine
    {

        public static List<StarSystem> GameSessionSystems { get { return GameSessionHandler.GameSessionSystems; } set { GameSessionHandler.GameSessionSystems = value; } }

        public static void AdvanceInTime(double TimeStep)
        {
            if(TimeStep>0)
            { 

                //Update Game Session TimeStep
                GameSessionHandler.AdvanceTime(TimeStep);

                foreach (StarSystem system in GameEngine.GameSessionSystems)
                {
                    List<Star> StarList = system.Children.Where(x => x is Star).Cast<Star>().ToList<Star>();
                    List<Planet> PlanetList = system.Children.Where(x => x is TreeElementPlanets).First().Children.Where(y => y is Planet).Cast<Planet>().ToList<Planet>();

                    foreach (Star star in StarList)
                    {
                        
                        star.advanceTime(-1, TimeStep);
                    }

                    foreach (Planet planet in PlanetList)
                    {

                        planet.advanceTime(-1, TimeStep);
                    }
                }
            }
            else
            {

                throw new ArgumentOutOfRangeException("Timestep must be > 0");
            }
        }

        public static double GetTimepassed()
        {

            return GameSessionHandler.GetTimePassed();
        }

        public static void CreateStarSystem(bool _forceRecreate = false)
        {
            Applicazione.DataModel.PeriodicTable.init();

            List<Gamecore.DataModel.ChemicalElement> chemicalElements = Gamecore.Engine.DataEngine.starSeed;
            List<double> percentageList = new List<double>();

            percentageList = Gamecore.Engine.SimulationEngine.generateDistributionList(90, 70, chemicalElements.Count);

            Gamecore.DataModel.ChemicalComposition chemicalComposition;

            chemicalComposition = new Gamecore.DataModel.ChemicalComposition(chemicalElements, percentageList);

            Gamecore.DataModel.StarSystem system = new Gamecore.DataModel.StarSystem();
            system.InitSystemParams(new Double[] { 1, Gamecore.ParametriUtente.Science.r_sun, 1 }, chemicalComposition);
            system.createStarSystem();

            if (GameEngine.GameSessionSystems.Count == 0 || _forceRecreate)
            {

                GameEngine.GameSessionSystems.Clear();

                GameEngine.GameSessionSystems.Add(new StarSystem(system));

            }
        }
    }
}
