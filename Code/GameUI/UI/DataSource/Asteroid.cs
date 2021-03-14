using GameUI.UI.DataSource.UIItems_DS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using Gamecore = MainGame.Applicazione.DataModel;
namespace GameUI.UI.DataSource
{
    class Asteroid : IBodyTreeViewItem
    {

        Gamecore.Asteroid relatedAsteroid;

        public Asteroid()
        {

        }
        public Asteroid(Gamecore.Asteroid _asteroid)
        {
            relatedAsteroid = _asteroid;
            this.setName();
            this.setChildren();
        }

        protected override void setName()
        {
            this.Name = relatedAsteroid.generate_planet_name();
        }

        protected override void setChildren()
        {
           
        }

        protected override void childrenDrawBody()
        {

            this.Shape = new Ellipse { Width = 5, Height = 5, Fill = Brushes.Brown };
            
        }

        protected override void setColor()
        {
            this.Shape.Fill = Brushes.LightGray;
        }

        protected override void linkShapeToBody()
        {
            this.Shape.Tag = this.relatedAsteroid;
        }

        protected override void initShapeParameters()
        {
            this.minShapeRadius = 2;
        }
    }
}
