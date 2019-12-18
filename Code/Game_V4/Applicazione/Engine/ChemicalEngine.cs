using Applicazione.DataModel;
using MainGame.Applicazione.DataModel;
using MainGame.Applicazione.Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainGame.Applicazione
{
	class ChemicalEngine
	{
		public double[] defaultElemDensity = new double[]{0.0000899, 0.0001785,0.534, 1.848, 2.34, 2.26, 0.0012506, 0.001429, 0.001696, 0.0009
												, 0.971, 1.738, 2.702, 2.33, 1.82, 2.07, 0.003214, 0.0017824,0.862, 1.55
												, 2.99, 4.54, 6.11, 7.19, 7.43, 7.874, 8.9, 8.9, 8.96, 7.13
												, 5.907, 5.323, 5.72, 4.79, 3.119, 0.00375, 1.63, 2.54, 4.47, 6.51
												, 8.57, 10.22, 11.5, 12.37, 12.41, 12.02, 10.5, 8.65, 7.31, 7.31
												, 6.684, 6.24, 4.93, 0.0059, 1.873, 3.59, 6.15, 6.77, 6.77, 7.01
												, 7.3, 7.52, 5.24, 7.895, 8.23, 8.55, 8.8, 9.07, 9.32, 6.9
												, 9.84, 13.31, 16.65, 19.35, 21.04, 22.6, 22.4, 21.45, 19.32,13.546
												, 11.85, 11.35, 9.75, 9.3, 0.0, 0.00973, 0.0, 5.5, 10.07, 11.724
												, 15.4, 18.95, 20.2, 19.84, 13.67, 14.78, 15.1, 0.0, 0.0
												, 0.0, 0.0, 0.0 };
		//85-87-99-100-101-102-103

		public String[] defaultElements = new String[]{"Hydrogen", "Helium", "Lithium", "Beryllium", "Boron"
													, "Carbon", "Nitrogen", "Oxygen", "Fluorine", "Neon"
													, "Sodium", "Magnesium", "Aluminium", "Silicon", "Phosphorus"
													, "Sulfur", "Chlorine", "Argon", "Potassium", "Calcium"
													, "Scandium", "Titanium", "Vanadium", "Chromium", "Manganese"
													, "Iron", "Cobalt", "Nickel", "Copper", "Zinc"
													, "Gallium", "Germanium", "Arsenic", "Selenium", "Bromine"
													, "Krypton", "Rubidium", "Strontium", "Yttrium", "Zirconium"
													, "Niobium", "Molybdenum", "Technetium", "Ruthenium", "Rhodium"
													, "Palladium", "Silver", "Cadmium", "Indium", "Tin"
													, "Antimony", "Tellurium", "Iodine", "Xenon", "Cesium"
													, "Barium", "Lanthanum", "Cerium", "Praseodynium", "Neodymium"
													, "Promethium", "Samarium", "Europium", "Gadolinium", "Terbium"
													, "Dysprosium", "Holmium", "Erbium", "Thulium", "Ytterbium"
													, "Lutetium", "Hafnium", "Tantalum", "Tungsten", "Rhenium"
													, "Osmium", "Iridium", "Platinum", "Gold", "Mercury"
													, "Thallium", "Lead", "Bismuth", "Polonium", "Astatine"
													, "Radon", "Francium", "Radium", "Actinium", "Thorium"
													, "Protactinium", "Uranium", "Neptunium", "Plutonium", "Americium"
													, "Curium", "Berkelium", "Californium", "Einsteinium", "Fermium"
													, "Mendelevium", "Nobelium", "Lawrencium"};
		public void initDefaultPeriodicTable()
		{
			List<DataModel.ChemicalElement> periodicTable = ParametriUtente.Science.knownElements;

			DataModel.ChemicalElement element = new DataModel.ChemicalElement();
			element.density = 0.0;
			element.mass = 0.0;
			element.name = "";

			for (int i = 0; i < defaultElemDensity.Length;i++)
			{
				element.density = defaultElemDensity[i];
				element.name = defaultElements[i];
				periodicTable.Add(element);
			}

			ParametriUtente.Science.knownElements = periodicTable;
		}


        public void generateComposites(int _numberOfIterations, ChemicalComposition _composition)
        {
            List<ChemicalElement> chemicalElements,validElements;
            if(_numberOfIterations<=0)
            {

                return;
            }
            validElements = new List<ChemicalElement>();
            chemicalElements = PeriodicTable.getListOfElementsByState(ElementState.Molecule);
            foreach(ChemicalElement molecule in chemicalElements)
            {

                foreach (string _component in molecule.components)
                {
                    ChemicalElement chemicalElement = _composition.getElementFromName(_component);
                    if (chemicalElement == null)
                    {

                        validElements.Clear();
                        break;
                    }

                    validElements.Add(chemicalElement);
                }
            }
            
        }

        
    }
}


