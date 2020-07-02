using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainGame.Applicazione.DataModel.Climate
{
    class ClimateModel_TerrestrialPlanet : ClimateModel
    {

        double baseTemperature;
        double albedoFactor;
        List<LatitudinalRegion> temperatureBands;
        const double HeatDistributionFactor = 3.0;
        public ClimateModel_TerrestrialPlanet(double _baseTemperature, double _albedo, List<LatitudinalRegion> _temperatureBands)
        {

            this.baseTemperature = _baseTemperature;
            this.albedoFactor = _albedo;
            this.temperatureBands = _temperatureBands;
        }

        public  override double getRealTemperature(double _degree = 0)
        {

            double average= 0;
            
            foreach (LatitudinalRegion  region  in this.temperatureBands)
            {

                if(average == 0)
                {

                    average = this.getSingleTemperature(region.getRegionAngle());
                }
                else
                {

                    average = (average + this.getSingleTemperature(region.getRegionAngle())) 
                            / 2;
                }
            }

            return average;
        }

        private double getSingleTemperature(double _degree)
        {
            double temp= this.temperatureBands.Where(x => x.getRegionAngle() == _degree).First().getTemperature();
            
            return temp - (temp * albedoFactor);
        }

        public override void distributeHeat()
        {
            double maxtemp = temperatureBands.Max(x => x.getTemperature());
            double heatDistributionModifier;

            LatitudinalRegion sourceRegion = this.temperatureBands.Where(x => x.getTemperature() == maxtemp).FirstOrDefault();

            sourceRegion.AddTemperatureModifier(ParametriUtente.Science.TemperatureGradientModifierName, (-1 * HeatDistributionFactor));

            for(int i = 0; i< 2; i++)
            {
                LatitudinalRegion targetRegion;
    
                if ((sourceRegion.getRegionAngle()
                        +(sourceRegion.getFactor()*(i+1))
                        )<180 &&
                        (sourceRegion.getRegionAngle()
                        - (sourceRegion.getFactor() * (i + 1))
                        ) > 0
                    )
                {

                    targetRegion = this.temperatureBands.Where(x => x.getRegionAngle() == sourceRegion.getRegionAngle()
                        + (sourceRegion.getFactor() * (i + 1))).FirstOrDefault();
                    
                    if(targetRegion.getRegionAngle() > sourceRegion.getRegionAngle())
                    {

                        heatDistributionModifier = HeatDistributionFactor * ((HeatDistributionFactor - (i+1)) / HeatDistributionFactor);
                    }
                    else
                    {

                        heatDistributionModifier = HeatDistributionFactor * ((HeatDistributionFactor - (i+1)) / HeatDistributionFactor);
                    }



                    targetRegion.AddTemperatureModifier(ParametriUtente.Science.TemperatureGradientModifierName, heatDistributionModifier);
                }
            }
        }
    }
}
