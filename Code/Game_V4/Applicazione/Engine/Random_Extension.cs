using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainGame.Applicazione.Engine
{
    class Random_Extension : Random
    {

        public double NextDouble(double min, double max)
        {
            double num;

            num = this.NextDouble() * (max - min) + min;

            return num;

        }
    }
}
