using MainGame.Applicazione.DataModel;
using MainGame.Applicazione.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applicazione.DataModel
{
    static class PeriodicTable
    {

        private static DataEngine engine = new DataEngine();

        public static  void init()
        {
            if(engine == null)
            {
                engine = new DataEngine();
            }
            
            engine.setPeriodicTable(0);
        }

        public static ChemicalElement findByName(string _name)
        {

            return engine.findByName(_name);
        }
    }
}
