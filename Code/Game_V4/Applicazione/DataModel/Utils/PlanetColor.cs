using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainGame.Applicazione.DataModel.Utils
{
    class PlanetColor 
    {
        System.Drawing.Color color;

        public PlanetColor()
        {
        }

        public PlanetColor(System.Drawing.Color _color)
        {

            color = _color;
        }

        public double getAlbedo()
        {

            int R = color.R;
            int G = color.G;
            int B = color.B;

            double total = 255 * 3;

            double albedo = (100 * (R + G + B)) / total;

            return albedo;
        }
    }
}
