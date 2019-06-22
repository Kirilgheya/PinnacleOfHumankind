using System;
using System.Collections.Generic;
using System.Text;

namespace MainGame.UI.DataModel
{
    public class ChemicalElement : MainGame.Applicazione.DataModel.ChemicalElement
    {
       
        public void initElementDataFromFather(Applicazione.DataModel.ChemicalElement _father)
		{
			this.mass = _father.mass;
			this.density = _father.density;
            this.name = _father.name;
            this.symbol = _father.symbol;
           
		}

    }
}
