using GameUI.UI.DataSource.UIItems_DS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Gamecore = MainGame.Applicazione.DataModel;
namespace GameUI.UI.DataSource
{
    class Asteroid : IBodyTreeViewItem
    {

        public Gamecore.Asteroid relatedAsteroid;

        public Asteroid(Gamecore.Asteroid _asteroid)
        {
            relatedAsteroid = _asteroid;
            this.setName();
            this.setChildren();
        }

        public Asteroid()
        {
        }

        protected override void setName()
        {
            this.Name = relatedAsteroid.generate_planet_name();
        }

        protected override void setChildren()
        {
           
        }

        protected override void childrenDrawBody(double scale = 1 )
        {

            this.Shape = new Ellipse { Width = 5 * 1 /scale, Height = 5 * 1/ scale, Fill = Brushes.Brown };
            
        }

        protected override void childrenDrawBody(double x, double y)
        {

            this.Shape = new Ellipse { Width = x, Height = y, Fill = Brushes.Brown };

        }

        protected override void setSpriteForBody()
        {
            this.Shape.Fill = Brushes.LightGray;
        }

        public void setPosition(Point _newPosition)
        {

            this.position = _newPosition;

            Canvas.SetLeft(this.bodyShape, this.position.X);
            Canvas.SetTop(this.bodyShape, this.position.Y);
        }

        protected override void linkShapeToBody()
        {
            this.Shape.Tag = this;
        }

        protected override void initShapeParameters()
        {
            this.minShapeRadius = 2;
        }

        public override void advanceTime(double timestep = -1, double increment = -1)
        {

            if (timestep >= 0 && increment > 0)
            {

                throw new Exception("Bisogna specificare solo uno dei due argomenti");
            }
            else if (timestep >= 0)
            {

                this.angleOnOrbit = timestep;
            }
            else
            {
                if (!this.hasMoved())
                {

                    this.angleOnOrbit = 0;
                }

                this.angleOnOrbit += increment;
            }
        }

        protected override void UpdateHiglight()
        {
            throw new NotImplementedException();
        }
    }
}
