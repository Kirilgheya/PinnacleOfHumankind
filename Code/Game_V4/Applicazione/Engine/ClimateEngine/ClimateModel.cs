using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainGame.Applicazione.DataModel.Climate
{
    abstract class ClimateModel
    {

        public abstract double getRealTemperature(double _degree = 0);


        public abstract void distributeHeat();
        

    }
}
