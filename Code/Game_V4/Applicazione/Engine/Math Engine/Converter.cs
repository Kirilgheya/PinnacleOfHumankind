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
    public static List<Unità_di_misura> leght_units = new List<Unità_di_misura>(
        new Unità_di_misura("Millimetre","mm", null,10),
        new Unità_di_misura("Centimetre","cm",10,10),
        new Unità_di_misura("Decimetre","dm",10,10),
        new Unità_di_misura("Metre","dm",10,1000),
        new Unità_di_misura("Kilometre","Km",1000,149597870,700),
        new Unità_di_misura("Astronomical Unit","AU",UA_to_km, UA_to_LY),
        new Unità_di_misura("Light year", "LY",LY_to_UA,null)
        );

     public static List<Unità_di_misura> time_units = new List<Unità_di_misura>(
        new Unità_di_misura("Millisecond","ms", null,10),
        new Unità_di_misura("Centisecond","cs",10,10),
        new Unità_di_misura("Decisecond","ds",10,10),
        new Unità_di_misura("Second","s",10,60),
        new Unità_di_misura("Minute","min",60,60),
        new Unità_di_misura("Hour","h",60,24),
        new Unità_di_misura("Terrestrial day", "day",24,365),
        new Unità_di_misura("Terrestrial year", "year",365,null)
        );

     public static List<Unità_di_misura> weight_units = new List<Unità_di_misura>(
        new Unità_di_misura("Milligram","mg", null,10),
        new Unità_di_misura("Centigram","cg",10,10),
        new Unità_di_misura("Decigram","dg",10,10),
        new Unità_di_misura("Gram","gr",10,1000),
        new Unità_di_misura("Kilogram","Kg",1000,1000),
        new Unità_di_misura("Tons","tons",1000,null)
         );




}

public static class Unità_di_misura
{

    public string nome;  //inglese
    public string sigla; 
    public double al_precedente;  //se nullo è il più piccolo
    public double al_sucessivo; //se nullo è il più grande
    Unità_di_misura(string _nome, string _sigla, double _al_precedente, double _al_sucessivo)
        {
            nome = _nome;
            sigla = _sigla;
            al_precedente = _al_precedente;
            al_sucessivo = _al_sucessivo;
        }
}
