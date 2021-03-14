using MainGame.Applicazione.DataModel.Utils;
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
        double diffenceFromPrevious;
        double temperatureAtAngle;
        const double perpendicularAngleConstant = 90.0;
        ModifierList modifiers;
        public LatitudinalRegion(double _angle)
        {
            this.angle = _angle;
        }

        public LatitudinalRegion(double _angle, double _temperatureAtAngle, double _differenceFromPrevious = 30)
        {

            this.angle = _angle;
            this.temperatureAtAngle = _temperatureAtAngle;
            this.diffenceFromPrevious = _differenceFromPrevious;
            this.modifiers = new ModifierList();
        }

        public LatitudinalRegion()
        {
        }

        public double getFactor()
        {

            return this.diffenceFromPrevious;
        }

        public double getRegionAngle()
        {

            return angle;
        }

        public double getTemperature()
        {

            double totalModifier = this.modifiers.getSumOfValues();
            return temperatureAtAngle + totalModifier;
        }

        public void AddTemperatureModifier(string _modifierName, double _modifierValue)
        {

            this.modifiers.addModifier(_modifierName, _modifierValue);
        }

        public void resetModifiers()
        {

            this.modifiers.resetModifiers();
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

            modifier = 1 / Math.Pow(angleInverseSquareFactor, 1.0/2.0);

            temperatureAtAngle = _bodyTemperature * modifier;

        }
    }
}
