using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainGame.Applicazione.Engine.Math_Engine
{
    static class Formula
    {

        public static double VolumeOfSphere(double radius)
        {

            double volume = ((Math.Pow(radius, 3) * (4.0 / 3.0)) * Math.PI);

            return volume;
        }

        public static double Density(double mass, double volume)
        {

            return mass / volume;
        }
    }
}
