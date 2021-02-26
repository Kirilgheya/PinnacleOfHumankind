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

    public enum ChemicalElementClassification
    {
        Simple=0,
        Composite=1
    }

	public enum PlanetClassification
	{
		Satellite = 0,
		Planet = 1,
		Metallic_Planet = 2,
        Gasseous_Planet = 3
	}

	public enum NucleusClassification
	{
		Solid = 0,
		Liquid = 1,
		Gas = 2
	}

    public enum ElementState 
    {
        Gas = 0,
        Liquid = 1,
        Solid = 2,
        Plasma = 3,
        Synthetic = 4,
        Molecule = 5,
        Material = 6
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
        BrownDwarf = 8,
        BlackHole = 9
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
        T = 0,
        BlackHole = -1,
    }

    public enum StarClassification_byColor
    {
        //This determines the spectral type (the colour)
        
        Blue = 30000, // 30,000 K  
        Blue_White = 10000, //10,000–30,000 K
        White = 7500, //7,500–10,000 K     
        Yellow_White = 6000, //6,000–7,500 K 
        Yellow = 5200, //5,200–6,000 K 
        Light_Orange = 3700, //3,700–5,200 K 
        Orange = 2400, //2,400–3,700 K
        Orange_Red = 1300, //these are brown dwarves basically stars where fusion cannot occur
        None  = 0,
        BlackHole = -1
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
        T = 0,
        BlackHole = -1
    }

}
