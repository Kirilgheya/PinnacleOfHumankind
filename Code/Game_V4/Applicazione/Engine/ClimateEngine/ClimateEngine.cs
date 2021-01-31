using MainGame.Applicazione.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainGame.Applicazione.Engine.ClimateEngine
{
    class ClimateEngine
    {

        public static List<LatitudinalRegion> createLatitudinalRegions(int _numberOf, double _defaultTemp)
        {
            //create even number of regions and one default one
            List<LatitudinalRegion> bands = new List<LatitudinalRegion>();
            double currentAngle = 0,currentOppositAngle = 0, halfValue;
            LatitudinalRegion region;
            if (_numberOf % 2 != 0)
            {
                throw new ArgumentException("Number of Regions created must be even");
            }

            region = new LatitudinalRegion(90, _defaultTemp);
            bands.Add(region);
            halfValue = _numberOf / 2;
            for (int i = 0; i< halfValue; i++)
            {

                double currentAngleFract = ((90.0 / (halfValue + 1)) * (i+1));
                currentAngle = 90 - currentAngleFract;
                currentOppositAngle =  90 + currentAngleFract;

                region = new LatitudinalRegion(currentAngle, _defaultTemp, (-1*currentAngleFract));
                region.setTemperatureAtAngle(0, _defaultTemp);

                bands.Add(region);

                region = new LatitudinalRegion(currentOppositAngle, _defaultTemp, currentAngleFract);
                region.setTemperatureAtAngle(0, _defaultTemp);

                bands.Add(region);
            }

            return bands;
        }

    }
}
