using MainGame.Applicazione.DataModel;
using System;
using System.Collections.Generic;

/// <summary>
/// Summary description for Class1
/// </summary>
public abstract class Body
{
    public double relativeMass;
    public double relativeVolume;
    public double relativeRadius;
    public double relativeg;
    public double relativeAvgDensity;
    public double bodyAge;
    public double Core_temperature;
    public double Surface_temperature;
    public double Core_density;
    public double Surface_density;
    public double Volume;

    public ChemicalComposition body_composition;


    public Body()
    {
        body_composition = new ChemicalComposition();
    }

    public void initBody()
    {
     
    }

    

 

}
