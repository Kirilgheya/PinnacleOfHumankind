using System;
using System.Collections.Generic;
using System.Text;

namespace MainGame.Applicazione.DataModel
{
    public class ChemicalElement
    {
		public double density;
		public double mass;
        public ElementState state;
        public String name { get; set; }
        public String symbol { get; set; }
        public int numberOfParticles { get; set; }
        public String completeName { get { return symbol + " : " + name; } }

        public ChemicalElement Self { get { return this; } }
		public void initElementData(double _density, double _mass, string _name, string _symbol)
		{
			this.symbol = _symbol;
			this.name = _name;
			this.mass = _mass;
			this.density = _density; //AT STP Stp= standard temperature and pressure (STP) = (0 °C and 1atm)
        }

       
    }
}
