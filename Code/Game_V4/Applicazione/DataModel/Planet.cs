using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MainGame.Applicazione.DataModel
{
    public class Planet : Body
    {
		private PlanetClass planetClass;

        private Core planetCore;

        protected List<ChemicalElement> stellarCompositionMats;
        protected List<double> elementsDistribution;
        protected String name { get; set; }
        private double planetMass;
        public double mass
        {
            get { return planetMass; }
            set { this.planetMass = value; this.relativeMass = (value / 100) / Science.m_sun; }
        }
        private double meanDensity;
        private double volume;
		private double radius;
		private double g_atSeaLevel;
		private double average_density;
        protected double planetRadius;

        public Planet(List<ChemicalElement> composition,double radius_Km)
        { 
            this.planetRadius = radius_Km;
            this.stellarCompositionMats = composition;
            this.planetCore = new Core();
            this.planetClass = new PlanetClass("Metallic_Planet");
        }

        public void initStar(double _densityMul = 1.0, double rel_mass = 1.0, List<double> percentage = null)
        {
            elementsDistribution = percentage;
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            Function hydrostaticEquilibrium = ParametriUtente.Science.hydrostaticEquilibrium;
            //mass in grammi / 18.015 = moles
            //ideal gas law
            double molecularWeight = 0.0;
            double sumofElement = 0.0;

            double pressione;

            this.meanDensity = 0;

            foreach (ChemicalElement element in stellarCompositionMats)
            {
                double currentElement = elementsDistribution.ElementAt(stellarCompositionMats.IndexOf(element));

                sumofElement = sumofElement + currentElement;
                molecularWeight = (molecularWeight + (element.mass * currentElement)
                                                ) / sumofElement;
            }
            double f = (this.planetRadius * this.planetRadius * this.planetRadius) * (4 / 3) * Math.PI;

            this.Volume = (Math.Pow(this.planetRadius, 3) * 4 / 3 * Math.PI); //k3

            this.mass = rel_mass * ParametriUtente.Science.m_t;

            this.meanDensity = ((this.mass / Volume) * Math.Pow(10, -12)) * _densityMul;
            
            pressione = ((ParametriUtente.Science.G
                                * mass
                                * this.meanDensity * Math.Pow(10, 3))
                                / (this.planetRadius * this.planetRadius)) * this.planetRadius;

            this.Core_temperature = (pressione /
                                        ((this.meanDensity * Math.Pow(10, 8))
                                                * (8.314462618 / (molecularWeight)) * 4.8
                                                ))
                                            ; // - K to get °
            this.Surface_temperature = this.Core_temperature / 2543.37;
            double surfaceArea = Math.Pow(this.planetRadius, 2) * Math.PI * 4;
            //this.luminosity = (5.670374419 * (Math.Pow(this.Surface_temperature, 4)) * surfaceArea) / surfaceArea;

          
            //this.InitStarClassification();

            this.setRelativeValues();
        }

        public void setPlanetStats()
		{
            
            this.mass = UOMHandler.getPlanetMass(this.relativeMass);
			this.volume = UOMHandler.getPlanetVolume(this.relativeVolume);
			this.radius = UOMHandler.getPlanetRadius(this.relativeRadius);
			this.g_atSeaLevel = UOMHandler.getPlanetG(this.relativeg);
			this.average_density = UOMHandler.getPlanetDensity(this.relativeAvgDensity);
		}

        private void setRelativeValues()
        {
            this.relativeAvgDensity = this.meanDensity / ParametriUtente.Science.avg_d_t;
            this.relativeMass = this.mass / ParametriUtente.Science.m_t;
            //this.relCoretemperature = this.Core_temperature / ParametriUtente.Science.coretemp_sun;
            this.relativeVolume = this.Volume / ParametriUtente.Science.v_t;
            //this.relluminosity = this.luminosity / ParametriUtente.Science.lum_sun;
            //this.relSurfacetemperature = this.Surface_temperature / ParametriUtente.Science.surfacetemp_sun;
            //this.setMetallicity();
        }
    }
}
