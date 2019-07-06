using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainGame.UI.DataModel
{
    class StarSeed
    {

        public List<ChemicalElements_ComboBox_Ds> seed { get; set; }

        public StarSeed(List<Applicazione.DataModel.ChemicalElement> composition,List<double> distribution)
        {
            int index = 0;
            foreach(ChemicalElement element in composition)
            {
                
                ChemicalElements_ComboBox_Ds partialSeed = new ChemicalElements_ComboBox_Ds(element, distribution.ElementAt(index));
                if(seed==null)
                {
                    seed = new List<ChemicalElements_ComboBox_Ds>();
                }
                seed.Add(partialSeed);
                index++;
            }
        }
    }
}
