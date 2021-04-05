using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameCore = MainGame.Applicazione.DataModel;
using System.Collections.ObjectModel;
using GameUI.UI.DataSource.UIItems_DS;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace GameUI.UI.DataSource
{
    public class StarSystem : IBodyTreeViewItem
    {
        public GameCore.StarSystem relatedStarSystem;
        public double relativeDistances;

        public StarSystem(GameCore.StarSystem _starSystem)
        {
            this.relatedStarSystem = _starSystem;
            this.setName();
            this.setChildren();
            
        }

        public StarSystem()
        {
        }

        protected override void setChildren()
        {
            GameCore.Star[] stars = relatedStarSystem.getStars();
            ObservableCollection<IBodyTreeViewItem> local = new ObservableCollection<IBodyTreeViewItem>();
      
            for (int i = 0; i < stars.Length; i++)
            {

                Star star = new Star(stars[i]);
                local.Add(star);
            }



            if(relatedStarSystem.Planets.Count>0)
            {
                
                local.Add(new TreeElementPlanets(relatedStarSystem.Planets.Cast<Body>().ToList()));
            }

            if(relatedStarSystem.asteroids.Count >0)
            {
                local.Add(new TreeElementPlanets(relatedStarSystem.asteroids.Cast<Body>().ToList()));
            }

            Children = local;
        }

        protected override void setSpriteForBody()
        {
            throw new NotImplementedException();
        }

        protected override void childrenDrawBody(double scale = 1)
        {
            throw new NotImplementedException();
        }

        protected override void childrenDrawBody(double x, double y)
        {
            throw new NotImplementedException();
        }

        protected override void setName()
        {
            string majorStarName;

            List<GameCore.Star> stars = this.relatedStarSystem.getStars().ToList<GameCore.Star>();

            majorStarName = stars.Aggregate((i, j) => i.Mass > j.Mass ? i : j).FullName;

            Name = "Stellar System: " + majorStarName;

        }

        protected override void linkShapeToBody()
        {
            throw new NotImplementedException();
        }

        protected override void initShapeParameters()
        {
            throw new NotImplementedException();
        }

        public override void advanceTime(double timestep = -1, double increment = -1)
        {
            throw new NotImplementedException();
        }
    }
}
