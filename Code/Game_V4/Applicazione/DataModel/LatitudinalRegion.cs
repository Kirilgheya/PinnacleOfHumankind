using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainGame.Applicazione.DataModel
{
    class LatitudinalRegion
    {
        double angle;
        double temperatureAtAngle;
        const double perpendicularAngleConstant = 90.0;

        public LatitudinalRegion (double _angle)
        {
            this.angle = _angle;
        }

        public LatitudinalRegion(double _angle, double _temperatureAtAngle)
        {
         
            this.angle = _angle;
            this.temperatureAtAngle = _temperatureAtAngle;
        }

        public void setTemperatureAtAngle(double _axialTilt, double _bodyTemperature)
        {
            double modifier;
            double angleInverseSquareFactor;
            double angleValue;
            if(angle > perpendicularAngleConstant)
            {
                angleValue = angle - perpendicularAngleConstant;
            }
            else
            {

                angleValue = perpendicularAngleConstant - angle;
            }

            angleInverseSquareFactor = 1 + (angleValue / (perpendicularAngleConstant + _axialTilt));

            modifier = 1 / Math.Pow(angleInverseSquareFactor, 1);

            temperatureAtAngle = _bodyTemperature * modifier;

        }
    }
}
