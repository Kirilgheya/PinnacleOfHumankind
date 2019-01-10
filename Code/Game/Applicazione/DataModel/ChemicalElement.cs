using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Applicazione.DataModel
{
    class ChemicalElement
    {
		public double density;
		public double mass;
		public String name;
		public void initElementData(double _density, double _mass)
		{
			this.mass = _mass;
			this.density = _density;
		}
    }
}
