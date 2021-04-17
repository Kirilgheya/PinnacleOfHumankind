using GameUI.UI.DataSource.UIItems_DS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GameUI.Artificial
{
    public class artificialObj : IBodyTreeViewItem
    {

        SolidColorBrush StrokeColor = new SolidColorBrush() { Color = Colors.Red };
        private int StrokeThickeness = 4;

        SolidColorBrush Transparent = new SolidColorBrush() { Color = Colors.Transparent };

        public override void advanceTime(double timestep = -1, double increment = -1)
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

        protected override void initShapeParameters()
        {
            throw new NotImplementedException();
        }

        protected override void linkShapeToBody()
        {
            throw new NotImplementedException();
        }

        protected override void setChildren()
        {
            throw new NotImplementedException();
        }

        protected override void setName()
        {
            throw new NotImplementedException();
        }

        protected override void setSpriteForBody()
        {
            throw new NotImplementedException();
        }

        protected override void UpdateHiglight()
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
