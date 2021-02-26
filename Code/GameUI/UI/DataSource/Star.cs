using GameUI.UI.DataSource.UIItems_DS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameCore = MainGame.Applicazione.DataModel;
namespace GameUI.UI.DataSource
{
    class Star : IBodyTreeViewItem
    {

        private GameCore.Star relatedStar;
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
    }
}
