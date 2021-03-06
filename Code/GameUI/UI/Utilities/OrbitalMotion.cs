using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace GameUI.UI
{
    class OrbitalMotion
    {

        public double[] getXYFromRadiant(double _radiants,double C_x, double C_y, double w, double h)
        {

            double t = _radiants;
      
                double X = C_x + (w / 2) * Math.Cos(t);
                double Y = C_y + (h / 2) * Math.Sin(t);
            // Do what you want with X & Y here 

            return new double[] { X, Y };
        }

        public double[] getXYFromRadiant(double _radiants, Ellipse _ellipse)
        {

            return this.getXYFromRadiant(_radiants, _ellipse.Width / 2, _ellipse.Height / 2, _ellipse.Width, _ellipse.Height);
        }
    }
}
