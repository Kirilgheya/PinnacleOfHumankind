using System;
using System.Collections.Generic;
using System.Linq;

namespace MainGame.Applicazione.DataModel
{
    public class ChemicalComposition
    {

        //lista di oggetti nella forma elemento, percentuale
        public List<element_percentage> elements_percentage_list = new List<element_percentage>();
      
        //server solo per istanziale l'oggetto
        public ChemicalComposition()
        {
           
        }

        //costruttore che aggiunge alla lista di composizione degli elementi del corpo tutti gli oggetti nelle percentuali passsate
        public ChemicalComposition(List<ChemicalElement> _el, List<Double> _percentage)
        {
            for (int n = 0; n < _el.Count; n++)
            {
                addElementToComposition(_el[n], _percentage[n]);
            }
        }

        //ritorna la lista di tutti gli elementi del corpo
        internal List<ChemicalElement> get_elements()
        {
            return elements_percentage_list.Select(x => x.el).ToList();
        }

        //ritorna la lista di tutte le percentuali degli elementi del corpo
        internal List<Double> get_percentage()
        {
            return elements_percentage_list.Select(x => x.percentage).ToList();
        }


        //aggiunge alla composizione l'elemento x solo se la percentuale di presenza è maggiore di 0.00001
        public void addElementToComposition(ChemicalElement _chemicalElement, double _distribution)
        {
            if (_distribution > 0.00001)
            {
                elements_percentage_list.Add(new element_percentage(_chemicalElement, _distribution));
            }

        }

        public void removeElementFromComposition(ChemicalElement _chemicalElement, double _distribution)
        {

            if (_distribution > 0.00001)
            {
                element_percentage actualvalue = elements_percentage_list.Where(x => x.el == _chemicalElement).FirstOrDefault();
                if (_distribution == actualvalue.percentage)
                {
                    elements_percentage_list.Remove(actualvalue);
                }
                else if (_distribution < actualvalue.percentage && (actualvalue.percentage - _distribution) > 0.00001)
                {
                    elements_percentage_list.Remove(actualvalue);
                    elements_percentage_list.Add(new element_percentage(_chemicalElement, (actualvalue.percentage - _distribution)));
                }
                
            }
        }
        //ritorna la percentuale dell'elemento scelto
        internal double get_percentage_per_element(ChemicalElement element)
        {
            return elements_percentage_list.Where(x => x.el == element).FirstOrDefault().percentage;
        }

        internal ChemicalElement getElementFromName(string _name)
        {

            return elements_percentage_list.Where(x => x.el.name == _name).FirstOrDefault().el;
        }

        //ritorna percentuale di elementi fluidi
        private double get_gas_elements_percentage()
        {
            double perc = elements_percentage_list.Where(x => x.el.state == ElementState.Gas 
                                            || x.el.state == ElementState.Liquid).Sum(y => y.percentage);
            return perc;
        }

        //ritorna percentuale elementi solidi
        private double get_heavy_elements_percentage()
        {
            double perc = elements_percentage_list.Where(x => x.el.state == ElementState.Solid).Sum(y => y.percentage);
            return perc;
        }

        //ritorna elementi fluidi
        private List<ChemicalElement> get_gas_elements()
        {
            return elements_percentage_list.Where(x => x.el.state == ElementState.Gas 
                                        || x.el.state == ElementState.Liquid).Select(y => y.el).ToList();
        }

        //ritorna elementi solidi
        private List<ChemicalElement> get_heavy_elements()
        {
            return elements_percentage_list.Where(x => x.el.state == ElementState.Solid).Select(y => y.el).ToList();
        }

        //ritorna un elemento random per oggni tipo
        public ChemicalElement getRandomElement_PerType(ElementState _state)
        {

            ChemicalElement chemicalElement = null;
            int elementNumber = 0;
            Random randomElementNumber = new Random();
            switch(_state)
            {
                case ElementState.Plasma:
                case ElementState.Gas:
                    elementNumber = randomElementNumber.Next(1, this.get_gas_elements().Count);
                    chemicalElement = this.get_gas_elements().ElementAt(elementNumber - 1);
                    break;
                case ElementState.Solid:
                case ElementState.Liquid:
                    elementNumber = randomElementNumber.Next(1, this.get_heavy_elements().Count);
                    chemicalElement = this.get_heavy_elements().ElementAt(elementNumber - 1);
                    break;
            }

            return chemicalElement;


        }

        //imposta la classe del pianeta in base alle percentuali di solidi/ fluidi
        public PlanetClass GetPlanetClass()
        {

            double percRatio = this.get_gas_elements_percentage() / this.get_heavy_elements_percentage();
            string planetClassificationString;
            if (this.get_heavy_elements_percentage() > 50.0)
            {
                planetClassificationString = "Metallic_Planet";
            }
            else
            {
                planetClassificationString = "Gasseous_Planet";
            }

            PlanetClass planetClass = new PlanetClass(planetClassificationString);

            return planetClass;

        }

        //to string
        public override String ToString()
        {
            string formattedInfo = String.Empty;
            formattedInfo += "Composition:";
            for (int n = 0; n < elements_percentage_list.Count; n++)
            {
                formattedInfo = formattedInfo + "\n \t" + elements_percentage_list[n].ToString();
            }

            return formattedInfo;
        }
    }


   //oggetto elementi, percentuale
    public class element_percentage
    {

        public ChemicalElement el;
        public double percentage;
        public element_percentage(ChemicalElement _el, double _percentage)
        {
            el = _el;
            percentage = _percentage;
        }
        public override string ToString()
        {
            return el + " " + percentage.ToString("0.00000") + "%";
        }


    }

   
}
