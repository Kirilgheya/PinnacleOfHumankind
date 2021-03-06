﻿using GameUI.UI.GameEngine;
using GameUI.UI.Interfaccia;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace GameUI.UI.DataSource.UIItems_DS
{
    public abstract class IBodyTreeViewItem
    {
        public PlanetInfoPage PlanetInfoP = new PlanetInfoPage();

        public ShipInfoPage ShipInfoP = new ShipInfoPage();

        public double orbitRadius;

        public Ellipse Shape;
        public Ellipse bodyShape { get { return Shape; } }
        public string Name { get; set; }
        public ObservableCollection<IBodyTreeViewItem> Children { get; set; }

        public double minShapeRadius =1;

        public double angleOnOrbit = -1;
        public  Point position;
        protected abstract void setName();

        protected abstract void setChildren();

        private bool _selected = false;
        public bool selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
                if (value)
                {
                    if (!GameSessionHandler.selected.Contains(this))
                    {
                        GameSessionHandler.selected.Add(this);

                        
                    }                  
                }
                else
                {
                    if (GameSessionHandler.selected.Contains(this))
                    {
                        GameSessionHandler.selected.Remove(this);
                    }
                }

                this.UpdateHiglight();
            }
        }

        protected abstract void UpdateHiglight();

        public Ellipse drawBody( double scale = 1)
        {

            this.childrenDrawBody(scale);
            this.setSpriteForBody();
            this.linkShapeToBody();
         
            return Shape;
        }

        public Ellipse drawBody(double x, double y)
        {

            this.childrenDrawBody(x,y);
            this.setSpriteForBody();
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
        protected abstract void setSpriteForBody();

        protected abstract void initShapeParameters();

        public abstract void advanceTime(double timestep = -1, double increment = -1);
    }
}
