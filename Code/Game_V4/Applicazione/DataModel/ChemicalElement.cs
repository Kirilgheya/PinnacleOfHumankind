using System;
using System.Collections.Generic;
using System.Text;

namespace MainGame.Applicazione.DataModel
{
    public class ChemicalElement :IComparable<ChemicalElement>
    {
		public double density;
		public double mass;
        public ElementState state;
        public String name { get; set; }
        public String symbol { get; set; }
        public int numberOfParticles { get; set; }
        public String completeName { get { return symbol + " : " + name; } }

        public ChemicalElement()
        {

            int y= 0;
        }

        public ChemicalElement Self { get { return this; } set { } }
		public void initElementData(double _density, double _mass, string _name, string _symbol)
		{
			this.symbol = _symbol;
			this.name = _name;
			this.mass = _mass;
			this.density = _density; //AT STP Stp= standard temperature and pressure (STP) = (0 °C and 1atm)
        }

        public override string  ToString()
        {
            string formattedInfo = "";

            formattedInfo = this.completeName;

            return formattedInfo;
        }

        public int CompareTo(ChemicalElement other)
        {
            if(other.state < this.state)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
