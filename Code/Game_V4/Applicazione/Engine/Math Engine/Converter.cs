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
}