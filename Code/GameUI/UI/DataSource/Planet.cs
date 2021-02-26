﻿using GameUI.UI.DataSource.UIItems_DS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameCore = MainGame.Applicazione.DataModel;
namespace GameUI.UI.DataSource
{
    class Planet : IBodyTreeViewItem
    {

        private GameCore.Planet relatedPlanet;
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

           this.Name =  this.relatedPlanet.generate_planet_name();
        }
    }
}