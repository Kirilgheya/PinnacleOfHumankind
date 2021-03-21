﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace GameUI.UI.DataSource.UIItems_DS
{
    public abstract class IBodyTreeViewItem
    {

        protected Ellipse Shape;
        public Ellipse bodyShape { get { return Shape; } }
         public string Name { get; set; }
        public ObservableCollection<IBodyTreeViewItem> Children { get; set; }

        protected double minShapeRadius =1;

        public double angleOnOrbit = -1;

        protected abstract void setName();

        protected abstract void setChildren();



        public Ellipse drawBody( double scale = 1)
        {

            this.childrenDrawBody(scale);
            this.setColor();
            this.linkShapeToBody();
            return Shape;
        }

        public Ellipse drawBody(double x, double y)
        {

            this.childrenDrawBody(x,y);
            this.setColor();
            this.linkShapeToBody();
            return Shape;
        }

        public Boolean hasMoved()
        {

            if(this.angleOnOrbit==-1)
            {

                return false;
            }
            else
            {

                return true;
            }
        }

        protected abstract void linkShapeToBody();
        
        protected abstract void childrenDrawBody(double scale = 1);

        protected abstract void childrenDrawBody(double x, double y);
        protected abstract void setColor();

        protected abstract void initShapeParameters();

        public abstract void advanceTime(double timestep = -1, double increment = -1);
    }
}
