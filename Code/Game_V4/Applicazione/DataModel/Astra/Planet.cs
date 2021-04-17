using MainGame.Applicazione.DataModel.Climate;
using MainGame.Applicazione.Engine.ClimateEngine;
using MainGame.Applicazione.Engine.CreatureEngine;
using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using static MainGame.Applicazione.Engine.CreatureEngine.CreatureEngine;

namespace MainGame.Applicazione.DataModel
{
    public class Planet : Body
    {
        private PlanetClass planetClass;
        private Atmosphere Atmosphere;
        private bool hasAtmosphere;
        private double averageTemperature;
        private Core planetCore;
        private double waterBoilingPoint;
        private double waterMeltingPoint;
        public ElementState waterState { get { return _waterState; } set { _waterState = value; } }
        private ElementState _waterState;
        private List<LatitudinalRegion> planetRegions;
        private ClimateModel climateModel;
        public String name { get; set; }
        private double planetMass;
        private double distanceFromBarycenter;
        public Color PlanetColor { get; set; }
        public double mass
        {
            get { return planetMass; }
            set { this.planetMass = value; this.relativeMass = (value / 100) / ParametriUtente.Science.m_t; }
        }

        public double Radius { get { return this.planetRadius; } }

        public double relCoretemperature { get; set; }

        private double meanDensity;
        protected double surfaceGravity;
        protected double planetRadius;

        protected bool ringed = false;

        public double distance_from_star
        {
            get
            { return distanceFromBarycenter; }
            set
            { distanceFromBarycenter = value; }
        }

        public double SurfaceG { get { return this.relativeg * ParametriUtente.Science.g_t; } }
        public double Density { get { return meanDensity; } }

        public PlanetClass PlanetClass { get { return this.planetClass; } }

        public List<Creature> Ecosystem;

        public double relativeRevolutionTime;


        public Planet(ChemicalComposition _chemical, double radius_Km, double distance_from_star)
        {
            this.planetRadius = radius_Km;
            this.planetCore = new Core();
            this.planetClass = new PlanetClass("Metallic_Planet");
            body_composition = _chemical;

            this.name = generate_planet_name();

            this.distance_from_star = distance_from_star;
        }

        public Planet()
        {
        }

        public void initPlanet(double _densityMul = 1.0, double rel_mass = 1.0, List<double> percentage = null)
        {
            //elementsDistribution = percentage;
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            Function hydrostaticEquilibrium = ParametriUtente.Science.hydrostaticEquilibrium;
            //mass in grammi / 18.015 = moles
            double pressione;

            this.Volume = ((Math.Pow(this.planetRadius, 3) * (4.0 / 3.0)) * Math.PI); //km3
            this.mass = rel_mass * ParametriUtente.Science.m_t;
            this.meanDensity = (this.mass * 1000 / (Math.Pow(10, 15) * Volume)) * _densityMul;

            pressione = ((ParametriUtente.Science.G
                                * mass
                                * (this.meanDensity * 1000))
                          / (this.planetRadius * this.planetRadius));

            this.Core_temperature = ((0.84 * Math.Pow(10, -27)) * pressione)
                                    / (this.meanDensity * (1.380649 * Math.Pow(10, -23)));

            this.Surface_temperature = this.Core_temperature / 2543.37;

            double areaOfAbstoRadiationArea_ratio = 4; // 4 for fast rotation 2 for slow/tidal lock
            surfaceGravity = (ParametriUtente.Science.G * this.mass) / Math.Pow(this.planetRadius * 1000, 2);
            //https://en.wikipedia.org/wiki/Effective_temperature

            this.setRelativeValues();
            this.initAtmoSphere();
            this.planetRegions = ClimateEngine.createLatitudinalRegions(4, this.Surface_temperature);

            if (this.hasAtmosphere)
            {
                for (int i = 0; i < 2; i++)
                {
                    this.applyChemicalBonds();
                }
                Atmosphere = new Atmosphere(this.body_composition.getCompositionFromElements(body_composition.get_gas_elements()));
                Atmosphere.get_set_Pressure(ParametriUtente.Science.atm_t * (this.surfaceGravity / ParametriUtente.Science.g_t));

                double atmMass = (4 * Math.PI * Math.Pow(this.planetRadius * 1000, 2) * Converter.atm_to_PA(Atmosphere.get_set_Pressure()))
                                    / this.surfaceGravity;
                Atmosphere.get_set_Masspercentage(this.planetMass / atmMass);

                //TODO: Create ClimateModel programmatically
                this.climateModel = new ClimateModel_TerrestrialPlanet(this.Surface_temperature, 0, this.planetRegions);
                this.climateModel.distributeHeat();
                this.averageTemperature = this.climateModel.getRealTemperature();
                this.waterBoilingPoint = ChemicalEngine.getElementBoilingPoint(null, Converter.K_to_C(this.averageTemperature), Converter.atm_to_mmHg(this.Atmosphere.get_set_Pressure()));
                this.waterBoilingPoint = Converter.C_to_K(this.waterBoilingPoint);
                this.waterMeltingPoint = ChemicalEngine.getWaterMeltingPoint_AtP(this.Atmosphere.get_set_Pressure());
            }

            this.InitPlanetClassification();


            Ecosystem = CreatureEngine.GenerateEcoSystem(this);

            //terza legge di Keplero vieni a me
          
            //relativeRevolutionTime = Math.Sqrt(Math.Pow(distanceFromBarycenter,3));
        }

        public void setRelativeRevolutionTime(double orbitalPeriod)
        {
            this.relativeRevolutionTime = orbitalPeriod / 365;
        }

        private void initAtmoSphere(Boolean _isBlackBody = true, int iterations = 10)
        {
            double R, T, M, m;
            R = 8.314462618;
            T = this.Surface_temperature;
            ChemicalComposition composition = new ChemicalComposition();

            double escapevelocity = Math.Pow((2 * ParametriUtente.Science.G * this.mass) / (this.planetRadius * 1000), (1.0 / 2.0));
            ChemicalElement element = null;
            for (int i = 0; i < 3; i++)
            {
                ChemicalElement local_element = this.body_composition.getRandomElement_PerType(ElementState.Gas);
                if ((element != null && element.name.Equals(local_element.name)) || (local_element == null))
                {
                    continue;
                }

                element = local_element;

                double percentage = this.body_composition.get_percentage_per_element(element) / 100;

                if (composition.getElementFromName(element.name) == null)
                {
                    composition.addElementToComposition(element, percentage);
                }

                m = element.mass / 1000;
                M = m;
                double meanVelocityForElement = Math.Pow((2 * R * T) / M, (1.0 / 2.0));

                if (meanVelocityForElement > escapevelocity)
                {
                    composition.removeElementFromComposition(element, percentage);
                    this.body_composition.removeElementFromComposition(element, percentage);
                }
            }
            //prendi la lista di gas
            //scegli 3 gas
            // sqrt(2*R*T/M) dove R = gas constant T = Temperature K e M = mass dell'elemento/1000

            if (composition.elements_percentage_list.Count > 0)
            {
                hasAtmosphere = true;
            }

            this.applyChemicalBonds();
        }

        public void applyChemicalBonds()
        {
            Random random = new Random();
            List<ChemicalElement> chemicalElements = this.body_composition.get_elements();
            List<ChemicalElement> chosenOnes = new List<ChemicalElement>();
            List<int> chosenIndexes = new List<int>();
            int elementToCompositeRatio = 1000000;
            double compositeMass = this.planetMass / elementToCompositeRatio;
            List<ChemicalElement> generatedElements = new List<ChemicalElement>();
            int numberOfElements = chemicalElements.Count();

            ChemicalComposition composites = ChemicalEngine.generateComposites(1, this.body_composition);

            this.body_composition.mergeCompositions(composites);
        }

        private void InitPlanetClassification()
        {
            this.planetClass = this.body_composition.GetPlanetClass();

            if (this.Surface_temperature > this.waterBoilingPoint)
            {
                waterState = ElementState.Gas;
            }
            else if (this.Surface_temperature > this.waterMeltingPoint)
            {
                waterState = ElementState.Liquid;
            }
            else
            {
                waterState = ElementState.Solid;
            }
        }

        public void setPlanetColor()
        {

            if(this.planetClass.className == PlanetClassification.Gasseous_Planet)
            {

                if(this.Surface_temperature <= 150)
                {
                    
                    this.PlanetColor = ColorTranslator.FromHtml("#f0daad");
                }
                else if(this.Surface_temperature <= 250)
                {

                    this.PlanetColor = ColorTranslator.FromHtml("#e3e3e3");
                }
                else if(this.Surface_temperature <= 850)
                {

                    this.PlanetColor = ColorTranslator.FromHtml("#4e7bd4");
                }
                else if(this.Surface_temperature <= 1400)
                {

                    this.PlanetColor = ColorTranslator.FromHtml("#35355f");
                }
                else if(this.Surface_temperature > 1400)
                {

                    this.PlanetColor = ColorTranslator.FromHtml("#b5d9cd");
                }
            }
        }

        public void removeElement(string _elementName, double _percentageRemoved = 100.0)
        {
            if (_percentageRemoved > 100.0)
            {
                throw new ArgumentException("% cannot be higher than 100%");
            }
            ChemicalElement chemicalElement = this.body_composition.getElementFromName(_elementName);
            double actualPerc = this.body_composition.get_percentage_per_element(chemicalElement);
            _percentageRemoved = _percentageRemoved * (actualPerc / 100);
            this.body_composition.removeElementFromComposition(chemicalElement, _percentageRemoved);
        }

        public void setPlanetStats()
        {
            NotImplementedException e = new NotImplementedException("set planet Stats not implemented");
            /* this.mass = UOMHandler.getPlanetMass(this.relativeMass);
             this.volume = UOMHandler.getPlanetVolume(this.relativeVolume);
             this.radius = UOMHandler.getPlanetRadius(this.relativeRadius);
             this.g_atSeaLevel = UOMHandler.getPlanetG(this.relativeg);
             this.average_density = UOMHandler.getPlanetDensity(this.relativeAvgDensity);*/
            throw e;
        }

        private void setRelativeValues()
        {
            this.relativeRadius = this.planetRadius / ParametriUtente.Science.r_t;
            this._relativeAvgDensity = this.meanDensity / ParametriUtente.Science.avg_d_t;
            this.relativeMass = this.mass / ParametriUtente.Science.m_t;
            this.relCoretemperature = this.Core_temperature / ParametriUtente.Science.coretemp_t;
            this.relativeVolume = this.Volume / ParametriUtente.Science.v_t;
            this.relativeg = this.surfaceGravity / ParametriUtente.Science.g_t;
        }

        public override string ToString()
        {
            string formattedInfo = "";

            formattedInfo += "\n";
            formattedInfo += "Planet Name: " + this.name;
            formattedInfo += "\n\t" + this.planetClass.toString();
            formattedInfo += "\n\tRadius: " + Math.Round(this.relativeRadius,2);
            formattedInfo += "\n\tMass: " + Math.Round(this.relativeMass,2);
            formattedInfo += "\n\tDensity: " + Math.Round(this._relativeAvgDensity,2);
            formattedInfo += "\n\tGravity is " + Math.Round(this.relativeg,2) + " times the Earth's";
            formattedInfo += "\n\tAverage Temperature: " + Math.Round(this.averageTemperature,2) + "K ("
                                + Math.Round(Converter.K_to_C(this.averageTemperature),2) + " C°)";
            formattedInfo += "\n\tDistance from star: " + Math.Round(this.distance_from_star,2) + " AU";
            if (ringed)
            {
                formattedInfo += "\n\tRinged: Yes";
            }
            else
            {
                formattedInfo += "\n\tRinged: No";
            }

            formattedInfo += "\n\t Revolution time is " + Math.Round(relativeRevolutionTime * 365, 1) + " terrestrial days";

            formattedInfo += "\n\t" + this.body_composition.ToString();
            formattedInfo += "\n\tWater on this planet has a Boiling point of:"
                                + " " + Math.Round(Converter.K_to_C(this.waterBoilingPoint),2) + " C°"
                                + "\n\t\tand a Freezing point of:"
                                + " " + Math.Round(Converter.K_to_C(this.waterMeltingPoint),2) + " C°"
                                + "\n\tSo water would be " + this.waterState.ToString();

            return formattedInfo;
        }

        public static String generate_planet_name()
        {
            Random r;
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
#pragma warning disable IDE0054 // Use compound assignment
                    case 31: name = name + ("ji"); if (rannum % 7 == 0) { name = name + "-"; } break;
#pragma warning restore IDE0054 // Use compound assignment
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

        public String flavour_text()
        {
            String StringAtmosphere = "";
            String StringAtmosphereDensity = "";

            if (this.hasAtmosphere)
            {
                if (this.Atmosphere.getAtmosphericDensity() > 1.2)
                {
                    StringAtmosphereDensity = "dense";
                }
                else if (this.Atmosphere.getAtmosphericDensity() > 3)
                {
                    StringAtmosphereDensity = "incredibly dense";
                }
                else if (this.Atmosphere.getAtmosphericDensity() < 1.2)
                {
                    StringAtmosphereDensity = "thin";
                }

                StringAtmosphere = " with " + StringAtmosphereDensity + " atmosphere, filled with " + this.Atmosphere.greenHouseComposition.elements_percentage_list.OrderByDescending(x => x.percentage).First().el.name;
            }
            else
            {
                StringAtmosphere = " no atmosphere ";
            }

            String pln_comp = this.body_composition.elements_percentage_list.OrderByDescending(x => x.percentage).First().el.name;

            String temp_comp = "It's a";

            if (this.averageTemperature < 260)
            {
                temp_comp = temp_comp + " frozen";
            }

            if (this.averageTemperature >= 260 && this.averageTemperature < 273)
            {
                temp_comp = temp_comp + " cold";
            }
            else if (this.averageTemperature < 273 + 16 && this.averageTemperature > 273 + 10)
            {
                temp_comp = " it has a temperature similar to the Earth";
            }
            else if (this.averageTemperature >= 273 + 16 && this.averageTemperature < 273 + 40)
            {
                temp_comp = temp_comp + "hot";
            }
            else if (this.averageTemperature >= 273 + 40 && this.averageTemperature < 273 + 60)
            {
                temp_comp = temp_comp + "torrid";
            }
            else if (this.averageTemperature >= 273 + 60)
            {
                temp_comp = temp_comp + " furnace-like";
            }

            temp_comp = temp_comp + " planet";

            return this.name + " is  a " + this.planetClass.className.ToString().ToLower().Replace("_", " ") + " mainly composed by " + pln_comp + StringAtmosphere + ". " + temp_comp;
        }
    }
}