﻿using GameUI.UI.DataSource.UIItems_DS;
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

        private string SpritePath = "Res\\Planets\\DryPlanet.png";
        private string animatedSpritePath = "Res\\Planets\\DryPlanet.gif";
        public GameCore.Planet relatedPlanet
        {
            get { return _relatedPlanet; }
            set { _relatedPlanet = value; }
        }
        private GameCore.Planet _relatedPlanet;
       

        private int _currentAngle = -1;

        SolidColorBrush StrokeColor = new SolidColorBrush() { Color = Colors.Red };
        private int StrokeThickeness = 4;

        SolidColorBrush Transparent = new SolidColorBrush() { Color = Colors.Transparent };

        public double Temperature
        {
            get
            {
                return this.relatedPlanet.Surface_temperature;
            }
        }

        public Point getShapeCenter()
        {
            Point center = new Point();

            center.X = position.X - this.Shape.Width;
            center.Y = position.Y - this.Shape.Height;
            return center;
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

        public void setPosition(Point _newPosition)
        {

            this.position = _newPosition;

            Canvas.SetLeft(this.bodyShape, this.position.X);
            Canvas.SetTop(this.bodyShape, this.position.Y);
        }

        protected override void childrenDrawBody(double scale = 1)
        {

            //ImageBrush PlanetBrush = new ImageBrush(new BitmapImage(
            //new Uri(AppDomain.CurrentDomain.BaseDirectory + "Res\\Planets\\planet12.png")));


            BitmapImage image_file = new BitmapImage();

            Image Control_image = new Image();

            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + SpritePath);
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Control_image, image);
            //ImageBehavior.SetAnimationSpeedRatio(Control_image, 4);


            VisualBrush PlanetBrush = new VisualBrush();
            PlanetBrush.Visual = Control_image;

            double drawsize = 1;

            scale = scale / 10;

            drawsize = relatedPlanet.relativeRadius + 1 / scale;

            if(drawsize < 5)
            {
                drawsize = 5;
            }



            this.Shape  = new Ellipse { Width = drawsize, Height = drawsize , Fill = PlanetBrush };
            Canvas.SetZIndex(this.Shape, 1);
        }


        protected override void childrenDrawBody(double x, double y)
        {

            BitmapImage image_file = new BitmapImage();

            Image Control_image = new Image();

            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + animatedSpritePath);
            image.EndInit();
            ImageBehavior.SetAnimatedSource(Control_image, image);
            //ImageBehavior.SetAnimationSpeedRatio(Control_image, 4);


            VisualBrush PlanetBrush = new VisualBrush();
            PlanetBrush.Visual = Control_image;




            this.Shape = new Ellipse { Width = x, Height = y, Fill = PlanetBrush };
            Canvas.SetZIndex(this.Shape, 1);
        }

        protected override void setSpriteForBody()
        {
            
        }
        protected override void linkShapeToBody()
        {
            this.Shape.Tag = this;
        }

        protected override void initShapeParameters()
        {
            this.minShapeRadius = 5;
        }

        public override void advanceTime(double totalTimePassed = -1, double increment = 0)
        {

            if (totalTimePassed >= 0 && increment > 0)
            {

                throw new Exception("Bisogna specificare solo uno dei due argomenti");
            }
            else if (totalTimePassed >= 0)
            {
                double val = totalTimePassed;
                if(totalTimePassed > 365)
                {

                    val = totalTimePassed % 365;
                }
                this.angleOnOrbit = val;
            }
            else
            {
                if(!this.hasMoved())
                {

                    this.angleOnOrbit = 0;
                }

                this.angleOnOrbit += increment;
            }
        }

        protected override void UpdateHiglight()
        {

            if(this.bodyShape!=null)
            { 
                if (this.selected)
                {
                    this.Shape.Stroke = StrokeColor;
                    this.Shape.StrokeThickness = StrokeThickeness;

                }
                else
                {
                    this.Shape.Stroke = Transparent;
                    this.Shape.StrokeThickness = 0;
                }
            }
        }
    }
}
