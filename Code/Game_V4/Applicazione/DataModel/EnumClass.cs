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
    public enum OverallStarClassification
    {
        None = 0,
        HyperGiant=1,
        SuperGiant = 2,
        BrightGiant = 3,
        Giant = 4,
        LesserGiant = 5,
        MainSequenceDwarf = 6,
        WhiteDwarf = 7,
        BrownDwarf = 8
    }

    public enum StarClassification_byLum
    {
        //This determines the spectral type (the colour)
        O = 30000, // 30,000 K  
        B = 10000, //10,000–30,000 K
        A = 7500, //7,500–10,000 K     
        F = 6000, //6,000–7,500 K 
        G = 5200, //5,200–6,000 K 
        K = 3700, //3,700–5,200 K 
        M = 2400, //2,400–3,700 K
        L = 1300, //these are brown dwarves basically stars where fusion cannot occur
        T = 0 
    }

    public enum StarClassification_byMass
    {
        //This determines the  type (the biggggness * 100)
        O = 1600, // 30,000 K  
        B = 210, //10,000–30,000 K
        A = 140, //7,500–10,000 K     
        F = 104, //6,000–7,500 K 
        G = 80, //5,200–6,000 K 
        K = 45, //3,700–5,200 K 
        M = 8, //2,400–3,700 K
        L = 5, //these are brown dwarves basically stars where fusion cannot occur
        T = 0
    }

}
