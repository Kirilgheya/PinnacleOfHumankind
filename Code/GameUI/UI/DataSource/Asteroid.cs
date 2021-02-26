using GameUI.UI.DataSource.UIItems_DS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gamecore = MainGame.Applicazione.DataModel;
namespace GameUI.UI.DataSource
{
    class Asteroid : IBodyTreeViewItem
    {

        Gamecore.Asteroid relatedAsteroid;

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
    }
}
