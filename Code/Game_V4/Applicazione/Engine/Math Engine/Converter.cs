using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
public static class Converter
{
    public static double UA_to_Km(double _UA)
    {
        return _UA * Constants.UA_to_km;
    }

    public static double UA_to_MKm(double _UA)
    {
        return _UA * Constants.UA_to_Mkm;
    }

    public static double UA_to_LY(double _UA)
    {
        return _UA * Constants.UA_to_LY;
    }

    public static double LY_to_UA(double _LY)
    {
        return _LY * Constants.LY_to_UA;
    }

    //lista delle unità di misura della lunghezza
    public static List<Unità_di_misura> legth_units = new List<Unità_di_misura> { 
        new Unità_di_misura("Millimetro","mm", 0,10),
        new Unità_di_misura("Centimetro","cm",10,10),
        new Unità_di_misura("Decimetro","dm",10,10),
        new Unità_di_misura("Metro","m",10,1000),
        new Unità_di_misura("Kilometro","Km",1000,1000),
        new Unità_di_misura("Unità Astronomica","UA", UA_to_Km(1), UA_to_LY(1)),
        new Unità_di_misura("Anno luce", "LY",LY_to_UA(1),0)
        };

    public static List<Unità_di_misura> mass_units = new List<Unità_di_misura> {
        new Unità_di_misura("Milligrammo","mg", 0,10),
        new Unità_di_misura("Centigrammo","cg",10,10),
        new Unità_di_misura("Decigrammo","dg",10,10),
        new Unità_di_misura("Grammo","g",10,10),
        new Unità_di_misura("Decagrammo","dag",10,10),
        new Unità_di_misura("Ettogrammo","hg",10,10),
        new Unità_di_misura("Kilogrammo","kg",10,10),
        new Unità_di_misura("Quintale","q",100,10),
        new Unità_di_misura("Tonnellata","t",10,0),
        };

    public static List<Unità_di_misura> astraUnits = new List<Unità_di_misura> {
        new Unità_di_misura("Massa solare","⊙︎", 0,0),
        new Unità_di_misura("Massa terrestre","⊕",0,0),
        new Unità_di_misura("Massa gioviana","MJ",0,0),
        new Unità_di_misura("Raggio solare","R⊙︎", 0,0),
        new Unità_di_misura("Raggio terrestre","R⊕",0,0),
        new Unità_di_misura("Raggio gioviana","RJ",0,0),
        };

    public static List<Unità_di_misura> densityMeters_units = new List<Unità_di_misura> {
        new Unità_di_misura("Grammo/Centrimetro cubo","g/cm3", 0,1000),
        new Unità_di_misura("Kilogrammo/Metro cubo","kg/m3",1000,0),
        };
    public static List<Unità_di_misura> densityLiters_units = new List<Unità_di_misura> {
        new Unità_di_misura("Kilogrammo/Litro","kg/L",0,1000),
        new Unità_di_misura("Grammo/Litro","g/L",1000,0),
        };

    public static double gcm3_to_gL(double cm3)
    {
        return cm3 * Constants.gcm3_to_gL;
    }

    public static double gcm3_to_kgm3(double gcm3)
    {
        return gcm3 * Constants.gcm3_to_kgm3;
    }

    public static double gL_to_gcm3(double L)
    {
        return L * Constants.gL_to_gcm3;
    }

    public static double kgm3_to_kgL(double m3)
    {
        return m3 * Constants.kgm3_to_kgL;
    }

    public static double kgL_to_kgm3(double L)
    {
        return L * Constants.kgL_to_kgm3;
    }

    public static List<Unità_di_misura> densityLitersGrams_units = new List<Unità_di_misura> {
        new Unità_di_misura("Kilogrammo/Litro","kg/L",0,1000),
        new Unità_di_misura("Grammo/Litro","g/L",1000,0),
        };

    public static string getUOMFromName(string _name)
    {
        if (Converter.legth_units.Where(x => x.nome.Equals(_name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault() != null)
        {
            return Converter.legth_units.Where(x => x.nome.Equals(_name,StringComparison.OrdinalIgnoreCase)).FirstOrDefault().sigla;
        }
        else if(Converter.mass_units.Where(x => x.nome.Equals(_name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault() != null)
        {

            return Converter.mass_units.Where(x => x.nome.Equals(_name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault().sigla;
        }
        return null;
    }
}

public class Unità_di_misura
{

    public string nome;  //inglese o ita?
    public string sigla;  //come sopra
    public double al_precedente;  //se nullo è il più piccolo
    public double al_sucessivo; //se nullo è il più grande
    public Unità_di_misura(string _nome, string _sigla, double _al_precedente, double _al_sucessivo)
        {
            nome = _nome;
            sigla = _sigla;
            al_precedente = _al_precedente;
            al_sucessivo = _al_sucessivo;
        }
}
