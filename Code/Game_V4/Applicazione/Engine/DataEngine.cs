using MainGame.Applicazione.DataModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
namespace MainGame.Applicazione.Engine
{
    public class DataEngine
    {
        private static string extraResourcePath = ParametriUtente.exeRootFodler + "\\\\Risorse Extra\\\\";
        private List<ChemicalElement> listofElements = new List<ChemicalElement>();
        private List<ChemicalElement> listofComposites = new List<ChemicalElement>();
        public static List<ChemicalElement> starSeed = new List<ChemicalElement>();
        public static List<ChemicalElement> ironPlanetSeed = new List<ChemicalElement>();
        public static List<ChemicalElement> rockyPlanetSeed = new List<ChemicalElement>();
        public static List<ChemicalElement> gasPlanetSeed = new List<ChemicalElement>();
        public static List<ChemicalElement> carbonAsteroidSeed = new List<ChemicalElement>();
        public static List<ChemicalElement> siliconAsteroidSeed = new List<ChemicalElement>();
        public List<ChemicalElement> GetChemicalElements()
        {
            return this.listofElements;
        }

        public static void Shuffle<T>(IList<T> list, Random rnd)
        {
            for (var i = list.Count; i > 0; i--)
                DataEngine.Swap(list,0, rnd.Next(0, i));
        }

        public static void Swap<T>(IList<T> list, int i, int j)
        {
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
        public Star getPresetStarData(int _index = 0)
        {
            string[] starValues;
            int counter = 0;
            Star generatedStar = null;
            double mass;
            double surfaceTemp;
            double luminosity;
            int lumClass;

            foreach (var Lines
                            in File.ReadLines(@"" + extraResourcePath + "stardata.csv"))
            {
                if (counter != _index)
                {

                    counter++;
                }
                else
                {

                    counter = -1;
                    starValues = Lines.Split(';',',');

                    Double.TryParse(starValues[4], out mass);
                    Double.TryParse(starValues[1], out surfaceTemp);
                    Double.TryParse(starValues[2], out luminosity);
                    int.TryParse(starValues[0], out lumClass);
                    generatedStar = new Star(luminosity, surfaceTemp, mass, lumClass);
                    break;
                }

            }

            return generatedStar;
        }
        
        public void setSeeds()
        {
            starSeed.Add(findBaseElementByName("Hydrogen"));
            starSeed.Add(findBaseElementByName("Helium"));
            starSeed.Add(findBaseElementByName("Lithium"));
            starSeed.Add(findBaseElementByName("Beryllium"));
            starSeed.Add(findBaseElementByName("Boron"));
            starSeed.Add(findBaseElementByName("Carbon"));
            starSeed.Add(findBaseElementByName("Nitrogen"));
            starSeed.Add(findBaseElementByName("Oxygen"));
            starSeed.Add(findBaseElementByName("Fluorine"));
            starSeed.Add(findBaseElementByName("Neon"));
            starSeed.Add(findBaseElementByName("Sodium"));
            starSeed.Add(findBaseElementByName("Calcium"));
            starSeed.Add(findBaseElementByName("Scandium"));
            starSeed.Add(findBaseElementByName("Titanium"));
            starSeed.Add(findBaseElementByName("Vanadium"));
            starSeed.Add(findBaseElementByName("Chromium"));
            starSeed.Add(findBaseElementByName("Iron"));
            


            gasPlanetSeed.Add(findBaseElementByName("Hydrogen"));
            gasPlanetSeed.Add(findBaseElementByName("Helium"));
            gasPlanetSeed.Add(findBaseElementByName("Calcium"));
            gasPlanetSeed.Add(findBaseElementByName("Oxygen"));
            gasPlanetSeed.Add(findBaseElementByName("Silver"));
            gasPlanetSeed.Add(findBaseElementByName("Carbon"));
            gasPlanetSeed.Add(findBaseElementByName("Silicon"));
            gasPlanetSeed.Add(findBaseElementByName("Lithium"));
            gasPlanetSeed.Add(findBaseElementByName("Tin"));
            gasPlanetSeed.Add(findBaseElementByName("Sulfur"));
            gasPlanetSeed.Add(findBaseElementByName("Nickel"));
            gasPlanetSeed.Add(findBaseElementByName("Copper"));
            gasPlanetSeed.Add(findBaseElementByName("Cobalt"));
            gasPlanetSeed.Add(findBaseElementByName("Sodium"));
            gasPlanetSeed.Add(findBaseElementByName("Argon"));

            ironPlanetSeed.Add(findBaseElementByName("Iron"));
            
            ironPlanetSeed.Add(findBaseElementByName("Nickel"));
            ironPlanetSeed.Add(findBaseElementByName("Calcium"));
            ironPlanetSeed.Add(findBaseElementByName("Silver"));
            ironPlanetSeed.Add(findBaseElementByName("Carbon"));
            ironPlanetSeed.Add(findBaseElementByName("Silicon"));
            ironPlanetSeed.Add(findBaseElementByName("Nitrogen"));
            ironPlanetSeed.Add(findBaseElementByName("Lithium"));
            ironPlanetSeed.Add(findBaseElementByName("Oxygen"));
            ironPlanetSeed.Add(findBaseElementByName("Tin"));
            ironPlanetSeed.Add(findBaseElementByName("Sulfur"));
            ironPlanetSeed.Add(findBaseElementByName("Nickel"));
            ironPlanetSeed.Add(findBaseElementByName("Copper"));
            ironPlanetSeed.Add(findBaseElementByName("Cobalt"));
            ironPlanetSeed.Add(findBaseElementByName("Sodium"));
       
            ironPlanetSeed.Add(findBaseElementByName("Argon"));

            rockyPlanetSeed.Add(findBaseElementByName("Carbon"));
            rockyPlanetSeed.Add(findBaseElementByName("Silicon"));
            rockyPlanetSeed.Add(findBaseElementByName("Nitrogen"));
            rockyPlanetSeed.Add(findBaseElementByName("Iron"));
            rockyPlanetSeed.Add(findBaseElementByName("Silver"));
            rockyPlanetSeed.Add(findBaseElementByName("Carbon"));
            rockyPlanetSeed.Add(findBaseElementByName("Oxygen"));
            rockyPlanetSeed.Add(findBaseElementByName("Lithium"));
            rockyPlanetSeed.Add(findBaseElementByName("Tin"));
            rockyPlanetSeed.Add(findBaseElementByName("Sulfur"));
            rockyPlanetSeed.Add(findBaseElementByName("Nickel"));
            rockyPlanetSeed.Add(findBaseElementByName("Copper"));
            rockyPlanetSeed.Add(findBaseElementByName("Cobalt"));
            rockyPlanetSeed.Add(findBaseElementByName("Sodium"));
            rockyPlanetSeed.Add(findBaseElementByName("Argon"));

            carbonAsteroidSeed.Add(findBaseElementByName("Carbon"));
            carbonAsteroidSeed.Add(findBaseElementByName("Nitrogen"));
            carbonAsteroidSeed.Add(findBaseElementByName("Silver"));
            carbonAsteroidSeed.Add(findBaseElementByName("Oxygen"));
            carbonAsteroidSeed.Add(findBaseElementByName("Cobalt"));
            carbonAsteroidSeed.Add(findBaseElementByName("Sodium"));
            carbonAsteroidSeed.Add(findBaseElementByName("Argon"));

            siliconAsteroidSeed.Add(findBaseElementByName("Iron"));
            siliconAsteroidSeed.Add(findBaseElementByName("Silicon"));
            siliconAsteroidSeed.Add(findBaseElementByName("Silver"));
            siliconAsteroidSeed.Add(findBaseElementByName("Platinum"));
            siliconAsteroidSeed.Add(findBaseElementByName("Copper"));
            siliconAsteroidSeed.Add(findBaseElementByName("Gold"));
            siliconAsteroidSeed.Add(findBaseElementByName("Argon"));

            

        }

        public void setPeriodicTable(int _index = 0)
        {
            NumberStyles style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");


            var lines = File.ReadLines(@"" + extraResourcePath + "PeriodicTable.csv");

            //per togliere l'intestazione
            lines = lines.Skip(1);

            listofElements = lines.Select(x =>
           {
               var chemicalValue = x.Split(',', ';');
               ElementState state = (ElementState)Enum.Parse(typeof(ElementState), chemicalValue[4], true);
               double _density;
               Double.TryParse(chemicalValue[0], style, culture, out _density);
               if(state == ElementState.Gas)
               {
                   _density = Converter.gL_to_gcm3(_density);
               }
               double _atomicWeight;
               Double.TryParse(chemicalValue[5], style, culture, out _atomicWeight);

               return new ChemicalElement()
               {
                   density = _density,
                   name = chemicalValue[1],
                   symbol = chemicalValue[2],
                   numberOfParticles = Int32.Parse(chemicalValue[3].Trim()),
                   state = state,
                   mass = _atomicWeight,
                   type = ChemicalElementClassification.Simple,


               };

           }).ToList();
        }


        public void setCompositesTable(int _index = 0)
        {
            NumberStyles style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");

            
            var lines = File.ReadLines(@"" + extraResourcePath + "ElementTable.csv");
            
            //per togliere l'intestazione
            lines = lines.Skip(1);

           foreach(var line in lines)
            { 
                var chemicalValue = line.Split(',', ';');
                List<String> components = new List<string>();
                double atomicWeight = 0;
                double _density;
                ChemicalElement chemicalElement, molecule;
                ElementState state = (ElementState)Enum.Parse(typeof(ElementState), chemicalValue[3], true);
                Double.TryParse(chemicalValue[0], style, culture, out _density);
                
                if (state == ElementState.Gas)
                {
                    _density = Converter.gL_to_gcm3(_density);
                }
                
                for (int i = 4; i < chemicalValue.Length; i++)
                {
                    string value = chemicalValue[i];
                    if(!value.Equals(""))
                    {
                        chemicalElement = this.findElementByName(value);
                      
                        atomicWeight = atomicWeight + chemicalElement.mass;
                        components.Add(value);
                    }
                    
                }
                molecule = new ChemicalElement()
                {
                    density = _density,
                    name = chemicalValue[1],
                    symbol = chemicalValue[2],
                    state = state,
                    mass = atomicWeight,
                    type = ChemicalElementClassification.Composite,
                    components = components,

                };


                 listofComposites.Add(molecule);
            }
        }

        public ChemicalElement findElementByName(string _name)
        {
            ChemicalElement element = this.listofElements.Where(x => x.name.Equals(_name)).FirstOrDefault();
            if (element == null)
            {

                element = this.listofComposites.Where(x => x.name.Equals(_name)).FirstOrDefault();
                if (element == null)
                {
                    element = this.listofComposites.Where(x => x.symbol.Equals(_name)).FirstOrDefault();
                }

            }


            return element;
        }

        public ChemicalElement findBaseElementByName(string _name)
        {
            return this.listofElements.Where(x => x.name.Equals(_name)).FirstOrDefault();
        }

        public List<ChemicalElement> getListOfElementsByState(ElementState _statefilter)
        {

            if(_statefilter > ElementState.Solid)
            {

                return new List<ChemicalElement>(this.listofComposites.Where(x => x.state.Equals(_statefilter)));
            }
            else
            {

                return new List<ChemicalElement>(this.listofElements.Where(x => x.state.Equals(_statefilter)));
            }
        }
    }
}
