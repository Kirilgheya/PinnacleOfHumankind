using MainGame;
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

            return engine.findBaseElementByName(_name);
        }

        public static ChemicalElement findElementByName(string _name)
        {

            return engine.findElementByName(_name);
        }

        public static List<ChemicalElement> getListOfElementsByState(ElementState _statefilter,Boolean _iscomposite = false)
        {

            return engine.getListOfElementsByState(_statefilter, _iscomposite);
        }
    }
}
