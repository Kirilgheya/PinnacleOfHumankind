using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainGame

{
	class EnumClass
	{
	}
	public enum PlanetClassification
	{
		Satellite = 0,
		Planet = 1,
		Metallic_Planet = 2
	}

	public enum NucleusClassification
	{
		Solid = 0,
		Liquid = 1,
		Gas = 2
	}

    public enum ElementState 
    {
        Solid = 0,
        Plasma = 1,
        Liquid = 2,
        Gas = 3,
        Synthetic = 4
    }
    public enum LuminosityClassification
    {
        None = 0,
        Supergiganti = 1,
        Giganti_brillanti = 2,
        Giganti = 3,
        Sotto_giganti = 4,
        Standard = 5
    }

    public enum StarClassification
    {
        O = 0,
        B = 1,
        A = 2,
        F = 3,
        G = 4,
        K = 5,
        M = 6
    }

}
