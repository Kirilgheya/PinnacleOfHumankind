using System;
using System.Collections.Generic;
using System.Text;

namespace MainGame.Applicazione.DataModel
{
    public class ChemicalElement
    {
		public double density;
		public double mass;
		public String name;
		public String symbol;
		public void initElementData(double _density, double _mass, string _name, string _symbol)
		{
			this.symbol = _symbol;
			this.name = _name;
			this.mass = _mass;
			this.density = _density;
		}

        public override string ToString()
        {
            return name;
        }
    }
}
