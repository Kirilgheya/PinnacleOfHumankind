using MainGame.Applicazione.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainGame.Applicazione.DataModel
{
    public class ChemicalComposition
    {

        public List<ChemicalElement> stellarCompositionMats;
        public List<double> elementsDistribution;

        protected List<ChemicalElement> heavyElements = new List<ChemicalElement>();
        protected List<double> heavyElementsDistribution = new List<double>();
        protected double heavyElementsPercentage;

        protected List<ChemicalElement> gasElements = new List<ChemicalElement>();
        protected List<double> gasElementsDistribution = new List<double>();
        protected double gasElementsPercentage;

        public ChemicalComposition(List<ChemicalElement> _stellarCompositionMats
                                , List<double> _elementsDistribution)
        {

            this.elementsDistribution = _elementsDistribution;
            this.stellarCompositionMats = _stellarCompositionMats;

            int c = 0;

            foreach (ChemicalElement element in stellarCompositionMats)
            {

                
                double perc = elementsDistribution.ElementAt(c);
                c++;
                if (element.state == ElementState.Gas || element.state == ElementState.Liquid)
                {
                    gasElements.Add(element);
                    gasElementsDistribution.Add(perc);
                    gasElementsPercentage = gasElementsPercentage + perc;
                }
                else if(element.state == ElementState.Solid)
                {

                    heavyElements.Add(element);
                    heavyElementsDistribution.Add(perc);
                    heavyElementsPercentage = heavyElementsPercentage + perc;
                }
                
            }

        }

        public PlanetClass GetPlanetClass()
        {

            double percRatio = this.gasElementsPercentage / this.heavyElementsPercentage;
            string planetClassificationString;
            if(this.heavyElementsPercentage > 50.0)
            {
                planetClassificationString = "Metallic_Planet";
            }
            else
            {
                planetClassificationString = "Gasseous_Planet";
            }

            PlanetClass planetClass = new PlanetClass(planetClassificationString);

            return planetClass;

        }
    }
}
