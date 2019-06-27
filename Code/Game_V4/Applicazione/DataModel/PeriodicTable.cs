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

        private static List<ChemicalElement> periodicTable = new List<ChemicalElement>();

        public static  void init()
        {

            DataEngine engine = new DataEngine();
            periodicTable = engine.getPeriodicTable(0);
        }

        public static ChemicalElement findElement(string _name)
        {


            foreach(ChemicalElement element in periodicTable)
            {

                if(element.name.Equals(_name))
                {
                    return element;
                }
            }

            return null;
        }
    }
}
