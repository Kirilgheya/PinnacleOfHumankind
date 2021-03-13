using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainGame.Applicazione.DataModel
{
    class Atmosphere
    {

        public ChemicalComposition greenHouseComposition = new ChemicalComposition();
        public ChemicalComposition non_greenHouseComposition = new ChemicalComposition();
        double pressure;
        double parentBodyMasspercentage;
        public Atmosphere(ChemicalComposition _composition)
        {
            foreach (ChemicalElement element in _composition.get_elements())
            {

                if(Atmosphere.GreenhouseGases.greenHouseGases.Where(x => x.Equals(element.name,
                                        StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault() != null)
                {

                    greenHouseComposition.addElementToComposition(element, _composition.get_percentage_per_element(element));
                }
                else
                {
                    non_greenHouseComposition.addElementToComposition(element, _composition.get_percentage_per_element(element));
                }
            }
           
        }

        public double getAtmosphericDensity()
        {
            double countG = greenHouseComposition.elements_percentage_list.Count();
            countG = countG + non_greenHouseComposition.elements_percentage_list.Count();
            double greenhouse = greenHouseComposition.elements_percentage_list.Average(x => x.el.density * (x.percentage/100));
            double antigreen = non_greenHouseComposition.elements_percentage_list.Average(x => x.el.density * (x.percentage/100));

            return (greenhouse + antigreen) / countG;
        }

        public double get_set_Masspercentage(double _percentage = 0)
        {

            if (_percentage != default)
            {

                this.parentBodyMasspercentage = _percentage;
            }

            return parentBodyMasspercentage;
        }

        public double get_set_Pressure(double _pressure = 0)
        {

            if(_pressure != default)
            {

                this.pressure = _pressure;
            }

            return pressure;
        }
        protected static class GreenhouseGases
        {

            public static List<String>  greenHouseGases = new List<String>() { "Water vapor", "Carbon dioxide", "Methane", "Ozone"};
        }
    }
}
