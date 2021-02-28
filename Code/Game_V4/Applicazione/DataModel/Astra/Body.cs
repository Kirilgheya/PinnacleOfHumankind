using MainGame.Applicazione.DataModel;
using System;
using System.Collections.Generic;

/// <summary>
/// Generic Astral Body
/// </summary>
public abstract class Body
{
    protected double relativeMass;
    protected double relativeVolume;
    protected double _relativeRadius;
    public double relativeRadius { get; set; }

    protected double _relativeg;
    public double relativeg { get { return _relativeg; } set { _relativeg = value; } }
    
    protected double _relativeAvgDensity;


    protected double bodyAge;
    protected double _Core_temperature;
    public double Core_temperature { get { return _Core_temperature; } set { _Core_temperature = value; } }
    protected double _surface_temperature;
    public double Surface_temperature { get { return _surface_temperature; } set { _surface_temperature = value; } }
    protected double Core_density;
    protected double Surface_density;
    protected double Volume;

    protected ChemicalComposition body_composition;
    

    public Body()
    {
        body_composition = new ChemicalComposition();
    }

    public void initBody()
    {
     
    }

    public double getSchwarzschildRadius()
    {

        return -1.0;
    }




}
