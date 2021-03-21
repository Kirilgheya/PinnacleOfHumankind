using GameUI.UI.DataSource.UIItems_DS;
using GameUI.UI.Utilities;
using MaterialDesignColors.ColorManipulation;
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
using GameCore = MainGame.Applicazione.DataModel;
namespace GameUI.UI.DataSource
{
    public class Star : IBodyTreeViewItem
    {

        public GameCore.Star relatedStar;
        internal Point position;

        public double Temperature {
                get {
                    return this.relatedStar.Surface_temperature;
                }
        }


       
        public Star(GameCore.Star _generatedStar)
        {

            relatedStar = _generatedStar;
            this.setChildren();
            this.setName();
        }

        public Star()
        {
        }

        protected override void setChildren()
        {
         
        }
        protected override void setName()
        {


            this.Name = relatedStar.FullName;

        }

        public Brush getBrushFromStarColor()
        {



            if (this.relatedStar.getColor() == MainGame.StarClassification_byColor.BlackHole)
            {
                return UIStaticClass.ColorToBrush("#000000");
            }
            else if (this.relatedStar.getColor() == MainGame.StarClassification_byColor.Blue)
            {
                return UIStaticClass.ColorToBrush("#4897e0");
            }
            else if (this.relatedStar.getColor() == MainGame.StarClassification_byColor.Blue_White)
            {
                return UIStaticClass.ColorToBrush("#6cc1e6");
            }
            else if (this.relatedStar.getColor() == MainGame.StarClassification_byColor.Light_Orange)
            {
                return UIStaticClass.ColorToBrush("#f2cf5a");
            }
            else if (this.relatedStar.getColor() == MainGame.StarClassification_byColor.Orange)
            {
                return UIStaticClass.ColorToBrush("#fc8f00");
            }
            else if (this.relatedStar.getColor() == MainGame.StarClassification_byColor.Orange_Red)
            {
                return UIStaticClass.ColorToBrush("#fc5c00");
            }
            else if (this.relatedStar.getColor() == MainGame.StarClassification_byColor.White)
            {
                return UIStaticClass.ColorToBrush("#ffffff");
            }
            else if (this.relatedStar.getColor() == MainGame.StarClassification_byColor.Yellow)
            {
                return UIStaticClass.ColorToBrush("#fff700");
            }
            else //this.relatedStar.getColor() == MainGame.StarClassification_byColor.Yellow_White
            {
                return UIStaticClass.ColorToBrush("#faf8b4");
            }


        }


       

        protected override void childrenDrawBody(double scale = 1)
        {
            scale = scale / 150;

            
            double drawsize = relatedStar.relativeRadius + 1 / scale;

            if(drawsize > 50)
            {
                drawsize = 50;
            }


            this.Shape = new Ellipse { Width = drawsize, Height = drawsize , Fill = Brushes.White };
            Canvas.SetZIndex(this.Shape, 1);
        }

        protected override void childrenDrawBody(double x, double y)
        {
          



            this.Shape = new Ellipse { Width = x, Height = y, Fill = Brushes.White };
            Canvas.SetZIndex(this.Shape, 1);
        }

        protected override void setColor()
        {
            Brush shapeColor = this.getBrushFromStarColor();

            this.Shape.Fill = shapeColor;
        }

        protected override void linkShapeToBody()
        {
            this.Shape.Tag = this;
        }

        protected override void initShapeParameters()
        {
            this.minShapeRadius = 10;
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
    }


}
