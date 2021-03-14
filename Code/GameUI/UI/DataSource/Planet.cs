using GameUI.UI.DataSource.UIItems_DS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GameCore = MainGame.Applicazione.DataModel;
namespace GameUI.UI.DataSource
{
    public class Planet : IBodyTreeViewItem
    {

        public GameCore.Planet relatedPlanet
        {
            get { return _relatedPlanet; }
            set { _relatedPlanet = value; }
        }
        private GameCore.Planet _relatedPlanet;
        internal Point position;

        

        public double Temperature
        {
            get
            {
                return this.relatedPlanet.Surface_temperature;
            }
        }



        public Planet(GameCore.Planet _generatedPlanet)
        {
            Children = new ObservableCollection<IBodyTreeViewItem>();
            relatedPlanet = _generatedPlanet;
            this.setName();
            this.setChildren();
        }

        protected override void setChildren()
        {

        }

        protected override void setName()
        {

           this.Name =  this.relatedPlanet.name;
        }

        protected override void childrenDrawBody()
        {

            ImageBrush PlanetBrush = new ImageBrush(new BitmapImage(
            new Uri(AppDomain.CurrentDomain.BaseDirectory + "Res\\Planets\\planet12.png")));

            this.Shape  = new Ellipse { Width = 7, Height = 7, Fill = PlanetBrush };
           
        }

        protected override void setColor()
        {
            
        }
        protected override void linkShapeToBody()
        {
            this.Shape.Tag = this.relatedPlanet;
        }

        protected override void initShapeParameters()
        {
            this.minShapeRadius = 5;
        }
    }
}
