using System;
using System.Collections.Generic;
using System.Linq;

namespace MainGame.Applicazione.DataModel
{
    public class ChemicalComposition
    {

        //lista di oggetti nella forma elemento, percentuale
        public List<element_percentage> elements_percentage_list = new List<element_percentage>();
        static Random randomElementNumber = new Random();
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

        internal List<ChemicalElement> get_elementsForState(ElementState _state)
        {

            return elements_percentage_list.Where(x => x.el.state == _state).ToList().Select(x => x.el).ToList();
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

        public ChemicalComposition getCompositionFromElements(List<ChemicalElement> _elements)
        {
            ChemicalComposition newComposition = new ChemicalComposition();
            for (int n = 0; n < _elements.Count; n++)
            {
                newComposition.addElementToComposition(_elements[n], this.get_percentage_per_element(_elements[n]));
            }

            return newComposition;
        }

        internal ChemicalElement getElementFromName(string _name)
        {
            if(elements_percentage_list.Where(x => x.el.name == _name).FirstOrDefault() != null)
            {
                return elements_percentage_list.Where(x => x.el.name == _name).FirstOrDefault().el;
            }

            return null;
        }

        //ritorna percentuale di elementi fluidi
        public double get_fluid_elements_percentage()
        {
            double perc = elements_percentage_list.Where(x => x.el.state == ElementState.Gas 
                                            || x.el.state == ElementState.Liquid).Sum(y => y.percentage);
            return perc;
        }

        //ritorna percentuale elementi solidi
        public double get_solid_elements_percentage()
        {
            double perc = elements_percentage_list.Where(x => x.el.state == ElementState.Solid).Sum(y => y.percentage);
            return perc;
        }

        //ritorna elementi fluidi
        public List<ChemicalElement> get_gas_elements()
        {
            return elements_percentage_list.Where(x => x.el.state == ElementState.Gas 
                                        || x.el.state == ElementState.Liquid).Select(y => y.el).ToList();
        }

        //ritorna elementi solidi
        public List<ChemicalElement> get_solid_elements()
        {
            return elements_percentage_list.Where(x => x.el.state == ElementState.Solid).Select(y => y.el).ToList();
        }

        //ritorna un elemento random per oggni tipo
        public ChemicalElement getRandomElement_PerType(ElementState _state)
        {

            ChemicalElement chemicalElement = null;
            int elementNumber = 0;
            
            switch (_state)
            {
                case ElementState.Plasma:
                case ElementState.Gas:
                    if (this.get_gas_elements().Count > 0)
                    { 
                        elementNumber = randomElementNumber.Next(1, this.get_gas_elements().Count);
                        chemicalElement = this.get_gas_elements().ElementAt(elementNumber - 1);
                    }
                    break;
                case ElementState.Solid:
                case ElementState.Liquid:
                    elementNumber = randomElementNumber.Next(1, this.get_solid_elements().Count);
                    chemicalElement = this.get_solid_elements().ElementAt(elementNumber - 1);
                    break;
            }

            return chemicalElement;


        }

        //imposta la classe del pianeta in base alle percentuali di solidi/ fluidi
        public PlanetClass GetPlanetClass()
        {

            double percRatio = this.get_fluid_elements_percentage() / this.get_solid_elements_percentage();
            string planetClassificationString;
            if (this.get_solid_elements_percentage() > 50.0)
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

        public void mergeCompositions(ChemicalComposition _composition)
        {
            if(_composition == null)
            {

                return;
            }
            List<ChemicalElement> toBeMergedElements = _composition.get_elements(), newChemicalElements = new List<ChemicalElement>();
            List<double> toBeMergedPercentage = _composition.get_percentage();
         
            List<element_percentage> newcomposition = new List<element_percentage>();
            int totalePerc = 200,c=0;
            double perc = 0;
            foreach(ChemicalElement element in toBeMergedElements)
            {

                if(this.getElementFromName(element.name)!=null)
                {

                    perc = toBeMergedPercentage.ElementAt(c) + this.get_percentage_per_element(element);
                    newChemicalElements.Add(element);
                }
                else
                {
                    perc = toBeMergedPercentage.ElementAt(c);
                    newChemicalElements.Add(element);
                }

                newcomposition.Add(new element_percentage(element,  100 / (totalePerc / perc ) 
                                    ));
                c++;
            }

            c = 0;

            foreach (ChemicalElement element in this.get_elements())
            {

                if (newChemicalElements.Where(x => x.name == element.name).FirstOrDefault() == null)
                {

                    perc = this.get_percentage_per_element(element);
                    newChemicalElements.Add(this.get_elements().ElementAt(c));
                }
                else
                {

                    perc = this.get_percentage_per_element(element);
                    perc = perc + newcomposition.Where(x => x.el.name == element.name).FirstOrDefault().percentage;
                    newcomposition.Remove(newcomposition.Where(x => x.el.name == element.name).FirstOrDefault());
                }

                newcomposition.Add(new element_percentage(element, (100 / (totalePerc / perc))
                        ));
                c++;
            }

            this.elements_percentage_list = newcomposition;
        }

        //to string
        public override String ToString()
        {
            IEnumerable<element_percentage> list =  elements_percentage_list.OrderByDescending(perc => perc.el.density);
            string formattedInfo = String.Empty;
            formattedInfo += "Composition:\n\t{";

            foreach (element_percentage perc  in list)
            {
                formattedInfo = formattedInfo + "\n\t\t" + perc.ToString();
            }
            /*
            for (int n = 0; n < elements_percentage_list.Count; n++)
            {
                formattedInfo = formattedInfo + "\n \t" + elements_percentage_list[n].ToString();
            }
            */
            formattedInfo += "\n\t}";
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
