using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainGame.Applicazione.Engine
{
    public class Random_Extension : Random
    {

        public Random_Extension(int Seed) : base(Seed)
        {


        }

        public double NextDouble(double min, double max)
        {
            double num;

            num = this.NextDouble() * (max - min) + min;

            return num;

        }
    }
}
