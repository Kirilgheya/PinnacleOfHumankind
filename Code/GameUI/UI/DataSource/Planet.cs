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
using WpfAnimatedGif;
using System.Windows.Controls;

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

        public Planet()
        {
        }

        protected override void setChildren()
        {

        }

        protected override void setName()
        {

           this.Name =  this.relatedPlanet.name;
        }

        protected override void childrenDrawBody(double scale = 1)
        {

            //ImageBrush PlanetBrush = new ImageBrush(new BitmapImage(
            //new Uri(AppDomain.CurrentDomain.BaseDirectory + "Res\\Planets\\planet12.png")));


            BitmapImage image_file = new BitmapImage();

            Image Control_image = new Image();

            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "Res\\Planets\\planet_gif.gif");
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Control_image, image);


            VisualBrush PlanetBrush = new VisualBrush();
            PlanetBrush.Visual = Control_image;
             


            this.Shape  = new Ellipse { Width = 100 * 1 / scale, Height = 100 * 1 / scale , Fill = PlanetBrush };
           
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
