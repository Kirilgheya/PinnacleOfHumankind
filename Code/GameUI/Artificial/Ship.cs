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

        public String name = "Aurora";

        public Point Position = new Point();

        public Point destination = new Point(470,470);

        public double speed = 1;
        public bool selected;

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
            if(destination != null)
            {
                double xIncrement = 0;
                if(destination.X > Position.X)
                {
                    xIncrement = speed;
                }
                else if (destination.X < Position.X)
                {
                    xIncrement = -speed;
                }

                double yIncrement = 0;
                if (destination.Y > Position.Y)
                {
                    yIncrement = speed;
                }
                else if (destination.Y < Position.Y)
                {
                    yIncrement = -speed;
                }
                this.Position = new Point(this.Position.X + xIncrement, this.Position.Y + yIncrement);
                Canvas.SetLeft(this.Shape, Position.X);
                Canvas.SetTop(this.Shape, Position.Y);
            }
        }

       
    }
}
