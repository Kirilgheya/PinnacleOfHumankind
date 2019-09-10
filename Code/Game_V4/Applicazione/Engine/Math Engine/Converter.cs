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
        new Unità_di_misura("Millimetro","mm", null,10),
        new Unità_di_misura("Centimetro","cm",10,10),
        new Unità_di_misura("Decimetro","dm",10,10),
        new Unità_di_misura("Metro","dm",10,1000),
        new Unità_di_misura("Kilometro","Km",1000,149597870,700),
        new Unità_di_misura("Unità Astronomica","UA",UA_to_km, UA_to_LY),
        new Unità_di_misura("Anno luce", "LY",LY_to_UA,null)
        );


}

public static class Unità_di_misura
{

    public string nome;  //inglese o ita?
    public string sigla;  //come sopra
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
