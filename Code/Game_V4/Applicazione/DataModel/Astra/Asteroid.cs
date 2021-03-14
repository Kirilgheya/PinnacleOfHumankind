using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainGame.Applicazione.DataModel
{
    public class Asteroid : Body
    {

        private Core asteroidCore;

        protected String name { get; set; }
        private double planetMass;
        public double mass
        {
            get { return planetMass; }
            set { this.planetMass = value; this.relativeMass = (value / 100) / ParametriUtente.Science.m_t; }
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
            this.name = this.generate_planet_name();
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

            this.meanDensity = ((this.mass / Volume) * Math.Pow(10, -16)) * _densityMul;

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
            this._relativeAvgDensity = this.meanDensity / ParametriUtente.Science.avg_d_t;
            this.relativeMass = this.mass / ParametriUtente.Science.m_t;
            
            this.relativeVolume = this.Volume / ParametriUtente.Science.v_t;
            //this.relluminosity = this.luminosity / ParametriUtente.Science.lum_sun;
            //this.relSurfacetemperature = this.Surface_temperature / ParametriUtente.Science.surfacetemp_sun;
            //this.setMetallicity();
        }

        public override string ToString()
        {

            string formattedInfo = "";

            formattedInfo += "\n";
            formattedInfo += "Asteroid Name: " + this.name;
         
            formattedInfo += " - Radius:(km) " + this.asteroidRadius;
            formattedInfo += " - Mass:(kg) " + this.mass;
            formattedInfo += " - Density: " + this._relativeAvgDensity;
 
            formattedInfo += " - Distance from star:(km)" + this.distance_from_star.ToString();
       
            formattedInfo += " - \n" + this.body_composition.ToString().Trim('\n');



            return formattedInfo;
        }

        public String generate_planet_name()
        {
            Random r = new Random(Guid.NewGuid().GetHashCode());
            int rannum = 0;
            String name = String.Empty;

            for (int i = 0; i < 3; i++)
            {
                r = new Random(Guid.NewGuid().GetHashCode());
                rannum = r.Next(104);
                switch (rannum)
                {
                    case 0: name = name + ("a"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 1: name = name + ("i"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 2: name = name + ("u"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 3: name = name + ("e"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 4: name = name + ("o"); if (rannum % 7 == 0) { name = name + "-"; } break;

                    case 5: name = name + ("ka"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 6: name = name + ("ki"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 7: name = name + ("ku"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 8: name = name + ("ke"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 9: name = name + ("ko"); if (rannum % 7 == 0) { name = name + "-"; } break;

                    case 10: name = name + ("gar"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 11: name = name + ("gi"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 12: name = name + ("jir"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 13: name = name + ("zir"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 14: name = name + ("nash"); if (rannum % 7 == 0) { name = name + "-"; } break;

                    case 15: name = name + ("sa"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 16: name = name + ("shi"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 17: name = name + ("su"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 18: name = name + ("se"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 19: name = name + ("so"); if (rannum % 7 == 0) { name = name + "-"; } break;

                    case 20: name = name + ("za"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 21: name = name + ("ji"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 22: name = name + ("zu"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 23: name = name + ("ze"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 24: name = name + ("zo"); if (rannum % 7 == 0) { name = name + "-"; } break;

                    case 25: name = name + ("ta"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 26: name = name + ("chi"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 27: name = name + ("tsu"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 28: name = name + ("te"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 29: name = name + ("to"); if (rannum % 7 == 0) { name = name + "-"; } break;

                    case 30: name = name + ("da"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 31: name = name + ("ji"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 32: name = name + ("zu"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 33: name = name + ("de"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 34: name = name + ("do"); if (rannum % 7 == 0) { name = name + "-"; } break;

                    case 35: name = name + ("na"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 36: name = name + ("ni"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 37: name = name + ("nu"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 38: name = name + ("ne"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 39: name = name + ("no"); if (rannum % 7 == 0) { name = name + "-"; } break;

                    case 40: name = name + ("ha"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 41: name = name + ("hi"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 42: name = name + ("fu"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 43: name = name + ("he"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 44: name = name + ("ho"); if (rannum % 7 == 0) { name = name + "-"; } break;

                    case 45: name = name + ("rin"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 46: name = name + ("kar"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 47: name = name + ("we"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 48: name = name + ("ser"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 49: name = name + ("nash"); if (rannum % 7 == 0) { name = name + "-"; } break;

                    case 50: name = name + ("ar"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 51: name = name + ("ur"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 52: name = name + ("ush"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 53: name = name + ("shin"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 54: name = name + ("zar"); if (rannum % 7 == 0) { name = name + "-"; } break;

                    case 55: name = name + ("ma"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 56: name = name + ("mi"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 57: name = name + ("mu"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 58: name = name + ("me"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 59: name = name + ("mo"); if (rannum % 7 == 0) { name = name + "-"; } break;

                    case 60: name = name + ("ra"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 61: name = name + ("ri"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 62: name = name + ("ru"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 63: name = name + ("re"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 64: name = name + ("ro"); if (rannum % 7 == 0) { name = name + "-"; } break;

                    case 65: name = name + ("ya"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 66: name = name + ("yu"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 67: name = name + ("yo"); if (rannum % 7 == 0) { name = name + "-"; } break;

                    case 68: name = name + ("wa"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 69: name = name + ("wu"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 70: name = name + ("n"); if (rannum % 7 == 0) { name = name + "-"; } break;

                    case 71: name = name + ("val"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 72: name = name + ("sal"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 73: name = name + ("shir"); if (rannum % 7 == 0) { name = name + "-"; } break;

                    case 74: name = name + ("ral"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 75: name = name + ("rhin"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 76: name = name + ("shi"); if (rannum % 7 == 0) { name = name + "-"; } break;

                    case 77: name = name + ("sha"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 78: name = name + ("shu"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 79: name = name + ("sho"); if (rannum % 7 == 0) { name = name + "-"; } break;

                    case 80: name = name + ("ja"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 81: name = name + ("ju"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 82: name = name + ("jo"); if (rannum % 7 == 0) { name = name + "-"; } break;

                    case 83: name = name + ("cha"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 84: name = name + ("chu"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 85: name = name + ("cho"); if (rannum % 7 == 0) { name = name + "-"; } break;

                    case 86: name = name + ("nya"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 87: name = name + ("nyu"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 88: name = name + ("nyo"); if (rannum % 7 == 0) { name = name + "-"; } break;

                    case 89: name = name + ("hya"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 90: name = name + ("hyu"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 91: name = name + ("hyo"); if (rannum % 7 == 0) { name = name + "-"; } break;

                    case 92: name = name + ("kir"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 93: name = name + ("des"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 94: name = name + ("gor"); if (rannum % 7 == 0) { name = name + "-"; } break;

                    case 95: name = name + ("nar"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 96: name = name + ("shir"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 97: name = name + ("nur"); if (rannum % 7 == 0) { name = name + "-"; } break;

                    case 98: name = name + ("mya"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 99: name = name + ("myu"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 100: name = name + ("myo"); if (rannum % 7 == 0) { name = name + "-"; } break;

                    case 101: name = name + ("rya"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 102: name = name + ("ryu"); if (rannum % 7 == 0) { name = name + "-"; } break;
                    case 103: name = name + ("ryo"); if (rannum % 7 == 0) { name = name + "-"; } break;

                }
            }

            //prima maiuscola
            name = name.First().ToString().ToUpper() + name.Substring(1);

            // niente trattino nelle ultima sillaba
            name = name.Substring(0, name.Length - 2) + name.Substring(name.Length - 2).Replace("-", String.Empty);

            return name;
        }

    }

}
 

