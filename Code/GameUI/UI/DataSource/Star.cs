using GameUI.UI.DataSource.UIItems_DS;
using MaterialDesignColors.ColorManipulation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using GameCore = MainGame.Applicazione.DataModel;
namespace GameUI.UI.DataSource
{
    class Star : IBodyTreeViewItem
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
                return ColorToBrush("#000000");
            }
            else if (this.relatedStar.getColor() == MainGame.StarClassification_byColor.Blue)
            {
                return ColorToBrush("#4897e0");
            }
            else if (this.relatedStar.getColor() == MainGame.StarClassification_byColor.Blue_White)
            {
                return ColorToBrush("#6cc1e6");
            }
            else if (this.relatedStar.getColor() == MainGame.StarClassification_byColor.Light_Orange)
            {
                return ColorToBrush("#f2cf5a");
            }
            else if (this.relatedStar.getColor() == MainGame.StarClassification_byColor.Orange)
            {
                return ColorToBrush("#fc8f00");
            }
            else if (this.relatedStar.getColor() == MainGame.StarClassification_byColor.Orange_Red)
            {
                return ColorToBrush("#fc5c00");
            }
            else if (this.relatedStar.getColor() == MainGame.StarClassification_byColor.White)
            {
                return ColorToBrush("#ffffff");
            }
            else if (this.relatedStar.getColor() == MainGame.StarClassification_byColor.Yellow)
            {
                return ColorToBrush("#fff700");
            }
            else //this.relatedStar.getColor() == MainGame.StarClassification_byColor.Yellow_White
            {
                return ColorToBrush("#faf8b4");
            }


        }


        public static Brush ColorToBrush(string color) // color = "#E7E44D"
        {
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString(color);
            return brush;
        }

       

        protected override void childrenDrawBody()
        {

            this.Shape = new Ellipse { Width = 10, Height = 10, Fill = Brushes.White };
            
        }
    }


}
