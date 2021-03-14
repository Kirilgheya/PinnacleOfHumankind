using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using Gamecore = MainGame.Applicazione.DataModel;
namespace GameUI.UI.DataSource.UIItems_DS
{
    class TreeElementPlanets : IBodyTreeViewItem
    {
     
        public List<Body> bodies;


        public TreeElementPlanets()
        {

        }
        public TreeElementPlanets(List<Body> _bodies)
        {

            bodies = _bodies;
            this.setName();
            this.setChildren();
        }

        protected override void setName()
        {
            this.Name = "System Bodies:";
        }

        protected override void setChildren()
        {
        
          switch(bodies[0])
            {
                case Gamecore.Planet p:
                    Children = new ObservableCollection<IBodyTreeViewItem>();
                    foreach (Body planet in bodies)
                    {

                        Children.Add(new Planet(planet as Gamecore.Planet));
                    }
                    break;
                case Gamecore.Asteroid a:
                    Children = new ObservableCollection<IBodyTreeViewItem>();
                    foreach(Body asteroid in bodies)
                    {

                        Children.Add(new Asteroid(asteroid as Gamecore.Asteroid));
                    }
                    break;
            }
        }

        protected override void childrenDrawBody()
        {
            throw new NotImplementedException();
        }

        protected override void setColor()
        {
            throw new NotImplementedException();
        }

        protected override void linkShapeToBody()
        {
            throw new NotImplementedException();
        }

        protected override void initShapeParameters()
        {
            throw new NotImplementedException();
        }
    }
}
