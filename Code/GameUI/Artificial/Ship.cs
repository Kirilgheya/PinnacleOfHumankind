﻿using System;
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

        public String name = "Aurora";

        public Point Position = new Point();

        public Point destination = new Point(470,470);

        public double speed = 1;
        public bool selected;

        public List<ComponentsEnumerator> ShipsComponentsList = new List<ComponentsEnumerator>();

         public int totalHP
        {
            get
            {
                return calculateSctorHP();
            }
        }

        public int calculateSctorHP()
        {
            int HP = 0;
           foreach( List<ShipSector> secList in Structure)
            {
                foreach( ShipSector s in secList)
                {
                    HP = HP + s.HP;
                }
            }

            return HP;
        }

        public List<List<ShipSector>> Structure = new List<List<ShipSector>>();


        public Ship()
        {
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


        private void SetComponenetList()
        {

            ShipsComponentsList = new List<ComponentsEnumerator>();

            foreach (List<ShipSector> secList in Structure)
            {
                foreach (ShipSector s in secList)

                    foreach (ShipComponents c in s.SectorComponents)
                    {
                        if (ShipsComponentsList.Where(p => p.comp.Equals(c)).Count() == 0 )
                        {
                            ShipsComponentsList.Add( new ComponentsEnumerator(c, 1, 1));
                        }
                        else
                        {
                            ShipsComponentsList.Where(x => x.comp.Equals(c)).ToList().ForEach(x => x.totali++) ;
                            ShipsComponentsList.Where(x => x.comp.Equals(c) && x.comp.active).ToList().ForEach(x => x.attivi++);
                        }
                    }

            }
        }

        public void spawn(double X, double Y)
        {
            this.Position = new Point(X,Y);

            Canvas.SetLeft(this.Shape, this.Position.X);
            Canvas.SetTop(this.Shape, this.Position.Y);
        }

        public void redrawPan(double xoffset, double yoffset)
        {
            Canvas.SetLeft(this.Shape, this.Position.X + xoffset);
            Canvas.SetTop(this.Shape, this.Position.Y + yoffset);
           
        }

     

        public void moveToDestination()
        {
            if (destination != null)
            {
                double xIncrement = 0;
                if (destination.X > Position.X)
                {
                    if (Math.Abs(destination.X - Position.X) < speed)
                    {
                        xIncrement = Math.Abs(destination.X - Position.X);
                    }
                    else
                    {
                        xIncrement = speed;
                    }
                }
                else if (destination.X < Position.X)
                {
                    if (Math.Abs(destination.X - Position.X) < speed)
                    {
                        xIncrement = -Math.Abs(destination.X - Position.X);
                    }
                    else
                    {

                        xIncrement = -speed;
                    }
                }

                double yIncrement = 0;
                if (destination.Y > Position.Y)
                {
                    if (Math.Abs(destination.Y - Position.Y) < speed)
                    {
                        yIncrement = Math.Abs(destination.Y - Position.Y);
                    }
                    else
                    {

                        yIncrement = speed;
                    }
                }
                else if (destination.Y < Position.Y)
                {
                    if (Math.Abs(destination.Y - Position.Y) < speed)
                    {
                        yIncrement = -Math.Abs(destination.Y - Position.Y);
                    }
                    else
                    {

                        yIncrement = -speed;
                    }
                }
                this.Position = new Point(this.Position.X + xIncrement, this.Position.Y + yIncrement);
                Canvas.SetLeft(this.Shape, Position.X);
                Canvas.SetTop(this.Shape, Position.Y);
            }
        }

        public void Setdamage(int Damage, int x, int y)
        {
            Structure[x][y].allocateDamage(Damage);

            SetComponenetList();
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
