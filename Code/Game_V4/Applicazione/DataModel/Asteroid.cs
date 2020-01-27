using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainGame.Applicazione.DataModel
{
    class Asteroid : Body
    {

        private Core asteroidCore;

        protected String name { get; set; }
        private double planetMass;
        public double mass
        {
            get { return planetMass; }
            set { this.planetMass = value; this.relativeMass = (value / 100) / Science.m_t; }
        }

        public double relCoretemperature { get; set; }
        private double meanDensity;
        protected double asteroidRadius;
        protected double distance_from_star;

        public Asteroid(ChemicalComposition _chemical, double radius_Km, double distance_from_star)
        {
            this.asteroidRadius = radius_Km;
            this.asteroidCore = new Core();
            body_composition = _chemical;
            this.distance_from_star = distance_from_star;

        }

        public void initAsteroid(double _densityMul = 1.0, double rel_mass = 1.0, List<double> percentage = null)
        {
          
            double molecularWeight = 0.0;
            double sumofElement = 0.0;
            double pressione;
            double surfaceArea;
            this.meanDensity = 0;

            foreach (ChemicalElement element in body_composition.get_elements())
            {

                double currentElement = body_composition.get_percentage_per_element(element);

                sumofElement = sumofElement + currentElement;
                molecularWeight = (molecularWeight + (element.mass)
                                               );
            }

            molecularWeight = molecularWeight / sumofElement;

            this.Volume = (Math.Pow(this.asteroidRadius, 3) * 4 / 3 * Math.PI); //k3

            this.mass = rel_mass * ParametriUtente.Science.m_t;

            this.meanDensity = ((this.mass / Volume) * Math.Pow(10, -12)) * _densityMul;

            pressione = ((ParametriUtente.Science.G / 100
                              * mass
                              * this.meanDensity * Math.Pow(10, 12))
                        / (this.asteroidRadius * this.asteroidRadius));

            surfaceArea = Math.Pow(this.asteroidRadius, 2) * Math.PI * 4;

            this.setRelativeValues();
            
        }

        private void setRelativeValues()
        {

            this.relativeRadius = this.asteroidRadius / ParametriUtente.Science.r_t;
            this.relativeAvgDensity = this.meanDensity / ParametriUtente.Science.avg_d_t;
            this.relativeMass = this.mass / ParametriUtente.Science.m_t;
            
            this.relativeVolume = this.Volume / ParametriUtente.Science.v_t;
            //this.relluminosity = this.luminosity / ParametriUtente.Science.lum_sun;
            //this.relSurfacetemperature = this.Surface_temperature / ParametriUtente.Science.surfacetemp_sun;
            //this.setMetallicity();
        }

    }
}
