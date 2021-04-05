using GameUI.UI.DataSource.UIItems_DS;
using GameUI.UI.Utilities;
using MaterialDesignColors.ColorManipulation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfAnimatedGif;
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
           
            
            double drawsize = relatedStar.relativeRadius  * (1/scale);

            
            
            if(drawsize > 50)
            {
                drawsize = 50;
            }
            else if (this.relatedStar.relativeRadius < 1)
            {
                drawsize = 5 * (1/scale);
            }

            if(drawsize < 1)
            {

                drawsize = 5;
            }

            this.Shape = new Ellipse { Width = drawsize, Height = drawsize , Fill = Brushes.Orange };
            this.Shape.ClipToBounds = true;
            Canvas.SetZIndex(this.Shape, 1);
        }

        protected override void childrenDrawBody(double x, double y)
        {
          



            this.Shape = new Ellipse { Width = x, Height = y, Fill = Brushes.White };
            Canvas.SetZIndex(this.Shape, 1);
        }

        protected override void setSpriteForBody()
        {
         
            //Brush shapeColor = this.getBrushFromStarColor();
            string spriteFolder = AppDomain.CurrentDomain.BaseDirectory + "Res\\Stars\\";
       
            switch(this.relatedStar.luminosityClass)
            {

                case MainGame.StarClassification_byLum.O:
                    spriteFolder = String.Concat(spriteFolder, "Active_OType_Star");
                    break;

                case MainGame.StarClassification_byLum.B:
                    spriteFolder = String.Concat(spriteFolder, "Active_BType_Star");
                    break;

                case MainGame.StarClassification_byLum.A:
                    spriteFolder = String.Concat(spriteFolder, "Active_AType_Star");
                    break;

                case MainGame.StarClassification_byLum.F:
                    spriteFolder = String.Concat(spriteFolder, "Active_FType_Star");
                    break;

                case MainGame.StarClassification_byLum.G:
                    spriteFolder = String.Concat(spriteFolder, "Active_GType_Star");
                    break;

                case MainGame.StarClassification_byLum.K:
                    spriteFolder = String.Concat(spriteFolder, "Active_KType_Star");
                    break;

                case MainGame.StarClassification_byLum.M:
                    spriteFolder = String.Concat(spriteFolder, "Active_MType_Star");
                    break;

                case MainGame.StarClassification_byLum.L:
                    spriteFolder = String.Concat(spriteFolder, "Active_MType_Star");
                    break;

                case MainGame.StarClassification_byLum.T:
                    spriteFolder = String.Concat(spriteFolder, "Active_MType_Star");
                    break;
                case MainGame.StarClassification_byLum.BlackHole:
                    spriteFolder = String.Concat(spriteFolder, "Active_AType_Star");
                    break;
                       
            }

            spriteFolder = String.Concat(spriteFolder, ".gif");

            BitmapImage image_file = new BitmapImage();

            Image Control_image = new Image();

            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(spriteFolder);
            image.EndInit();
            
            ImageBehavior.SetAnimatedSource(Control_image, image);
            Control_image.Width = this.bodyShape.Width;
            Control_image.Height = this.bodyShape.Height;
            VisualBrush starBrush = new VisualBrush();
            starBrush.Visual = Control_image;
            starBrush.Stretch = Stretch.UniformToFill;
            
            this.Shape.Fill = starBrush;
        
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
