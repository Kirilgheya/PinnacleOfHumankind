using System;
using System.Collections.Generic;
using System.Text;

namespace MainGame.UI.DataModel
{
    public class ChemicalElement : MainGame.Applicazione.DataModel.ChemicalElement
    {
	
		public void initElementDataFromFather(double _density, double _mass)
		{
			this.mass = _mass;
			this.density = _density;
		}
    }
}
