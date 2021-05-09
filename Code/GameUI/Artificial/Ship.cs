using GameUI.UI.GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GameUI.Artificial
{
    public class Ship :artificialObj
    {



        private Point _destination = new Point(470, 470);

        public Point destination
        {
            get
            {
                return _destination;
            }

            set
            {
                _destination = value;

                this.ShipInfoP.LoadInfo(this);
            }
        }

        public double speed
        {
            get
            {
                return calculateSectorsSpeed();
            }
        }
        public bool selected;

        public List<ComponentsEnumerator> ShipsComponentsList = new List<ComponentsEnumerator>();

         public int totalHP
        {
            get
            {
                return calculateSectorsHP();
            }
        }
        public int calculateSectorsHP()
        {
            int HP = 0;
            foreach (List<ShipSector> secList in Structure)
            {
                foreach (ShipSector s in secList)
                {
                    HP = HP + s.HP;
                }
            }

            return HP;
        }

        public double totalSpeed
        {
            get
            {
                return calculateSectorsSpeed();
            }
        }


        public double calculateSectorsSpeed()
        {
            double Speed = 0;
            foreach (List<ShipSector> secList in Structure)
            {
                foreach (ShipSector s in secList)
                {
                    Speed = Speed + s.SectorComponents.Where(x => x.active).Sum(x => x.SpeedIncrement);
                }
            }

            return Speed;
        }
        public int totalFirePower
        {
            get
            {
                return calculateALLSectorsFirePower();
            }
        }

        public int totalFLACKFirePower
        {
            get
            {
                return calculateFLACKSectorsFirePower();
            }
        }

        public int totalNONFLACKFirePower
        {
            get
            {
                return calculateNONFLACKSectorsFirePower();
            }
        }

        public List<artificialObj> OggettiInradar = new List<artificialObj>(); 

        public double radarRange = 300;
        public bool isRadaring = false;

        public int Mass 
        {
            get
            {
                return getShipMass();
            }
        }

        public int CargoSpace
        {
            get
            {
                return getShipCargoSpace();
            }
        }

        private int _currentShield = -1;
        public int CurrentShield
        {
            get
            {
                return _currentShield;
            }

            set
            {
                _currentShield  = value;
            }
        }

        public int MAXShield
        {
            get
            {
                return getShipMAXShield();
            }
        }

        private int getShipMAXShield()
        {
            int shield = 0;
            foreach (List<ShipSector> secList in Structure)
            {
                foreach (ShipSector s in secList)
                {
                    shield = shield + s.SectorComponents.Where(x => x.active).Sum(x => x.shield);
                }
            }

            return shield;
        }

        public int calculateALLSectorsFirePower(double targetDistance = -1)
        {
            int FirePower = 0;
            foreach (List<ShipSector> secList in Structure)
            {
                foreach (ShipSector s in secList)
                {
                    FirePower = FirePower + s.SectorComponents.Where(x => x.active && x.range >= targetDistance).Sum(x => x.Firepower);
                }
            }

            return FirePower;
        }

        public int calculateFLACKSectorsFirePower(double targetDistance = -1)
        {
            int FirePower = 0;
            foreach (List<ShipSector> secList in Structure)
            {
                foreach (ShipSector s in secList)
                {
                    FirePower = FirePower + s.SectorComponents.Where(x => x.active && x.Flack && x.range >= targetDistance).Sum(x => x.Firepower);
                }
            }

            return FirePower;
        }

        public int calculateNONFLACKSectorsFirePower(double targetDistance = -1)
        {
            int FirePower = 0;
            foreach (List<ShipSector> secList in Structure)
            {
                foreach (ShipSector s in secList)
                {
                    FirePower = FirePower + s.SectorComponents.Where(x => x.active && !x.Flack && x.range >= targetDistance).Sum(x => x.Firepower);
                }
            }

            return FirePower;
        }

        public List<List<ShipSector>> Structure = new List<List<ShipSector>>();
      

        public Ship(String _name = "")
        {
            this.Name = _name;

            Shape = new Ellipse() { Width = 5, Height = 5, Fill = Brushes.LightGray };

            Shape.Tag = this;

            List<ShipSector> ls = new List<ShipSector>();
            ls.Add(new ShipSector("ala sx"));
            ls.Add(new ShipSector("reattore"));
            ls.Add(new ShipSector("ala dx"));

            Structure.Add(ls);

            ls = new List<ShipSector>();
            ls.Add(new ShipSector("missile frontale sx"));
            ls.Add(new ShipSector("muso"));
            ls.Add(new ShipSector("missile frontale dx"));

            Structure.Add(ls);

            ls = new List<ShipSector>();
            ls.Add(new ShipSector("ala sx"));
            ls.Add(new ShipSector("muso quello vero"));
            ls.Add(new ShipSector("ala dx"));
            Structure.Add(ls);

            SetComponenetList();


        }


        internal string getRadarFormattedInfo()
        {
            string radar = "";
            foreach (artificialObj art in OggettiInradar)
            {
                Point shipd = (art as Ship).position;

                double distance = Math.Sqrt((Math.Pow(this.position.X - shipd.X, 2) + Math.Pow(this.position.Y - shipd.Y, 2)));

                if (distance <= this.radarRange)
                {
                    radar = radar + "VESSEL " + (art as Ship).Name + " POSITION " + shipd + " DISTANCE " + distance;
                }

            }

            return radar;
        }

        public void RadarPulse()
        {

            OggettiInradar = new List<artificialObj>();

            foreach (artificialObj art in GameSessionHandler.artificialList.Where(s => s.Name != this.Name))
            {
                Point shipd = (art as Ship).position;

                double distance = Math.Sqrt((Math.Pow(this.position.X - shipd.X, 2) + Math.Pow(this.position.Y - shipd.Y, 2)));

                if (distance <= this.radarRange)
                {
                    OggettiInradar.Add(art);
                }

            }
        }

        public string PulseRadarAndGetFormattedInfo()
        {
            RadarPulse();
            return getRadarFormattedInfo();
        }

        private void SetComponenetList()
        {
            int n = 0;
            int m = 0;
            ShipsComponentsList = new List<ComponentsEnumerator>();

            foreach (List<ShipSector> secList in Structure)
            {
                
                m = 0;
                foreach (ShipSector s in secList)
                {
                    s.x = n;
                    s.y = m;

                    foreach (ShipComponents c in s.SectorComponents)
                    {
                        if (ShipsComponentsList.Where(p => p.comp.Equals(c)).Count() == 0)
                        {
                            int active = 0;
                            if (c.active)
                            {
                                active = 1;
                            }


                            ShipsComponentsList.Add(new ComponentsEnumerator(c, active, 1));
                        }
                        else
                        {
                           
                            if (c.active)
                            {
                                ShipsComponentsList.Where(x => x.comp.Equals(c) && x.comp.active).ToList().ForEach(x => x.attivi++);
                            }
                            ShipsComponentsList.Where(x => x.comp.Equals(c)).ToList().ForEach(x => x.totali++);

                            
                        }
                    }
                    
                    m++;
                }
                n++;
            }

            if (this.CurrentShield == -1)
            {
                this.CurrentShield = getShipMAXShield();
            }
        }

        public void spawn(double X, double Y)
        {
            this.position = new Point(X,Y);

            Canvas.SetLeft(this.Shape, this.position.X);
            Canvas.SetTop(this.Shape, this.position.Y);
        }

        public void redrawPan(double xoffset, double yoffset)
        {
            Canvas.SetLeft(this.Shape, this.position.X + xoffset);
            Canvas.SetTop(this.Shape, this.position.Y + yoffset);
           
        }

        public int getShipMass()
        {
            int mass = 0;
            foreach (List<ShipSector> secList in Structure)
            {
                foreach (ShipSector s in secList)
                {
                    mass = mass + s.SectorComponents.Sum(x => x.mass);
                }
            }

            return mass;
        }

        public int getShipCargoSpace()
        {
            int mass = 0;
            foreach (List<ShipSector> secList in Structure)
            {
                foreach (ShipSector s in secList)
                {
                    mass = mass + s.SectorComponents.Sum(x => x.cargoSpace);
                }
            }

            return mass;
        }


        public void moveToDestination()
        {




            Point v1 = new Point(destination.X - position.X, destination.Y - position.Y);
            Point v2 = new Point(position.X , position.Y);

            // Calculate the vector lengths.
            double len1 = Math.Sqrt(v1.X * v1.X + v1.Y * v1.Y);
            double len2 = Math.Sqrt(v2.X * v2.X + v2.Y * v2.Y);

            // Use the dot product to get the cosine.
            double dot_product = v1.X * v2.X + v1.Y * v2.Y;
            double cos = dot_product / len1 / len2;

            // Use the cross product to get the sine.
            double cross_product = v1.X * v2.Y - v1.Y * v2.X;
            double sin = cross_product / len1 / len2;

            // Find the angle. angolo tra la posizione attuale della nave e la verticale
            double angolo = Math.Acos(cos);
            if (sin < 0) angolo = -angolo;




            if (destination != null)
            {
                double xIncrement = 0;
                if (destination.X > position.X)
                {
                    if (Math.Abs(destination.X - position.X) < speed * Math.Sin(angolo))
                    {
                        xIncrement = Math.Abs(destination.X - position.X);
                    }
                    else
                    {
                        xIncrement = speed * Math.Sin(angolo);
                    }
                }
                else if (destination.X < position.X)
                {
                    if (Math.Abs(destination.X - position.X) < speed * Math.Sin(angolo))
                    {
                        xIncrement = -Math.Abs(destination.X - position.X);
                    }
                    else
                    {

                        xIncrement = speed * Math.Sin(angolo);
                    }
                }

                double yIncrement = 0;
                if (destination.Y > position.Y)
                {
                    if (Math.Abs(destination.Y - position.Y) < speed * Math.Cos(angolo))
                    {
                        yIncrement = Math.Abs(destination.Y - position.Y);
                    }
                    else
                    {

                        yIncrement = speed * Math.Cos(angolo);
                    }
                }
                else if (destination.Y < position.Y)
                {
                    if (Math.Abs(destination.Y - position.Y) < speed * Math.Cos(angolo))
                    {
                        yIncrement = -Math.Abs(destination.Y - position.Y);
                    }
                    else
                    {

                        yIncrement = speed * Math.Cos(angolo);
                    }
                }
                this.position = new Point(this.position.X + xIncrement, this.position.Y + yIncrement);
                Canvas.SetLeft(this.Shape, position.X);
                Canvas.SetTop(this.Shape, position.Y);
            }
        }

        public void Setdamage(int Damage, int x, int y)
        {
            if (Structure[x][y].HP > 0)
            {
                Structure[x][y].allocateDamage(Damage);

                SetComponenetList();
            }
        }

        public void Setdamage(int Damage, Point p)
        {
            
            while(CurrentShield > 0 && Damage > 0)
            {
                CurrentShield--;
                Damage--;
            }

            if (Damage > 0)
            {
                Setdamage(Damage, (int)(p.X), (int)(p.Y));
            }

            this.ShipInfoP.LoadInfo(this);
        }

        public  List<Point> GetvalidDamageLocations()
        {
            List<Point> to_return = new List<Point>();

            foreach (List<ShipSector> secList in Structure)
            {
                foreach (ShipSector s in secList)
                {
                    if (s.HP > 0)
                    {
                        to_return.Add(new Point(s.x, s.y));
                    }
                }

                   
            }
            return to_return;
        }

        public Point GetRandomValidDamageLocation()
        {
            List<Point> validList = GetvalidDamageLocations();

            if(validList.Count == 0)
            {
                throw new Exception("Ship already destroyed");
            }

            var random = new Random();

            int a = random.Next(validList.Count);

            return validList[a];
        }

        public class ComponentsEnumerator
        {
            public ShipComponents comp;
            public int attivi = 0;
            public int totali = 0;
            public ComponentsEnumerator(ShipComponents s, int _attivi, int _totali)
            {
                comp = s;
                attivi = _attivi;
                totali = _totali;
            }
        }


    }
}
