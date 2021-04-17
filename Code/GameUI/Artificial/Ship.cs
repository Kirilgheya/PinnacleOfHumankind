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

        public Ellipse shape;

        public Point destination = new Point(250,250);

        public double speed = 1;

        public Ship()
        {
            shape = new Ellipse() { Width = 5, Height = 5, Fill = Brushes.LightGray };
        }

        public void spawn(double X, double Y)
        {
            this.Position = new Point(X,Y);

            Canvas.SetLeft(this.shape, this.Position.X);
            Canvas.SetTop(this.shape, this.Position.Y);
        }

        public void redrawPan(double xoffset, double yoffset)
        {
            Canvas.SetLeft(this.shape, this.Position.X + xoffset);
            Canvas.SetTop(this.shape, this.Position.Y + yoffset);
           
        }

        public void redrawZoom(double xoffset, double yoffset)
        {
            Canvas.SetLeft(this.shape, this.Position.X );
            Canvas.SetTop(this.shape, this.Position.Y );


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
                Canvas.SetLeft(this.shape, Position.X);
                Canvas.SetTop(this.shape, Position.Y);
            }
        }
    }
}
