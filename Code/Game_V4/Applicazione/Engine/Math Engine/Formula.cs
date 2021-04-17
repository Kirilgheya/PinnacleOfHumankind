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

        public static double OrbitalPeriod(double _starMass, double _planetMass, double _orbitRadius)
        {
            //T² = 4 * π² * a³ / μ
            double μ = (ParametriUtente.Science.G ) * (_starMass + _planetMass);
            double a = _orbitRadius * 1000;

            double T = Math.Sqrt( (4 * Math.Pow(Math.PI, 2) * Math.Pow(a, 3) / μ) );
            T = T / 60 / 60 / 24; //seconds to days
            return T;
        }
    }
}
