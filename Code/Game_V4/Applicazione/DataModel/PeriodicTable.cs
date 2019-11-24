using MainGame.Applicazione.DataModel;
using MainGame.Applicazione.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applicazione.DataModel
{
    public static class PeriodicTable
    {

        private static DataEngine engine = new DataEngine();

        public static List<ChemicalElement> GetChemicalElements()
        {
            return engine.GetChemicalElements();
        }

        public static  void init()
        {
            if(engine == null)
            {
                engine = new DataEngine();
            }
            
            engine.setPeriodicTable(0);
            engine.setCompositesTable(0);
            engine.setSeeds();
        }


        public static ChemicalElement findByName(string _name)
        {

            return engine.findByName(_name);
        }
    }
}
