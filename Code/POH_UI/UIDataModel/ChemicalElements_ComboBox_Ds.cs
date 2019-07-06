using Applicazione.DataModel;
using MainGame.Applicazione.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainGame.UI.DataModel
{
    class ChemicalElements_ComboBox_Ds
    {
        private IList<ChemicalElement> table = new List<ChemicalElement>();
        public double quantity { get; set; }
        public ChemicalElement element { get; set; }
        public IList<ChemicalElement> periodicTable_UI { get
            {
                if (!this.table.Any())
                {
                    this.initPeriodicTable();
                }
                return this.table;
            }
            set { this.initPeriodicTable(); }
        }


        public ChemicalElements_ComboBox_Ds(ChemicalElement element, double distribution)
        {
            this.element = element;
            this.quantity = distribution;

        }

        public ChemicalElements_ComboBox_Ds()
        {
        }

        public void initPeriodicTable()
        {
          
          

            List<Applicazione.DataModel.ChemicalElement> periodic= PeriodicTable.GetChemicalElements();

            foreach (Applicazione.DataModel.ChemicalElement baseElement in periodic)
            {
                ChemicalElement derivedElement = new ChemicalElement();
                derivedElement.initElementDataFromFather(baseElement);
                this.table.Add(derivedElement);
               
            }

        }
    }
}
