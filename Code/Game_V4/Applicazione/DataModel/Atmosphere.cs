using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainGame.Applicazione.DataModel
{
    class Atmosphere
    {

        ChemicalComposition greenHouseComposition = new ChemicalComposition();
        ChemicalComposition non_greenHouseComposition = new ChemicalComposition();

        public Atmosphere(ChemicalComposition _composition)
        {
            foreach (ChemicalElement element in _composition.get_elements())
            {

                if(Atmosphere.GreenhouseGases.greenHouseGases.Contains(element.name))
                {

                    greenHouseComposition.addElementToComposition(element, _composition.get_percentage_per_element(element));
                }
                else
                {
                    non_greenHouseComposition.addElementToComposition(element, _composition.get_percentage_per_element(element));
                }
            }
           
        }

        protected static class GreenhouseGases
        {

            public static List<String>  greenHouseGases = new List<String>() { "Water vapor", "Carbon dioxide", "Methane", "Ozone"};
        }
    }
}
