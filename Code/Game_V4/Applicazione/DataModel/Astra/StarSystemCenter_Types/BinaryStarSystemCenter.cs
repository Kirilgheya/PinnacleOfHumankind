using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainGame.Applicazione.DataModel.Astra
{
    class BinaryStarSystemCenter : StarSystemCenter
    {

        public BinaryStarSystemCenter()
        {

            this.stars = new Star[2];
        }

        protected override void childAddStar(Star _star, int _index)
        {

            this.stars[_index] = _star;
        }

        public override int canHaveMore()
        {
            int index= 0;
            while (index<this.stars.Length && this.stars[index]!=null)
            {

                index++;
            }

            if(index >= this.stars.Length)
            {
                index = -1;
            }

            return index;
        }


        public override string getFullName()
        {
            Star star;
            string formattedInfo = "";

            for (int i = 0; i < this.stars.Length; i++)
            {
                star = this.stars[i];
              

                formattedInfo += "\nStar Name: " + star.FullName;
                formattedInfo += "\n\tRadius: " + star.relativeRadius + " " +Converter.getUOMFromName("Raggio solare");
                formattedInfo += "\n\tMass: " + star.RelativeMass + " " + Converter.getUOMFromName("Massa solare");
                formattedInfo += "\n\tDensity: " + star.RelativeAvgDensity + " Relative to the Sun";
                formattedInfo += "\n\tCore Temperature: " + star.Core_temperature;
                formattedInfo += "\n\tEffective Temperature: " + star.Surface_temperature;
                formattedInfo += "\n\tStar Class: " + star.StarClass.ToString();
                formattedInfo += "\n\tVega-relative chromaticity: " + star.StarColor.ToString();
                formattedInfo += "\n\t" + star.StarComposition.ToString();


               
            }

            return formattedInfo;
        }

        public override string getStarSystemInformation()
        {
            Star star;
            string formattedInfo = "";

            for (int i = 0; i < this.stars.Length; i++)
            {
                star = this.stars[i];


                formattedInfo += "\nStar Name: " + star.FullName;
                formattedInfo += "\n\tRadius: " + star.relativeRadius + " " + Converter.getUOMFromName("Raggio solare");
                formattedInfo += "\n\tMass: " + star.RelativeMass + " " + Converter.getUOMFromName("Massa solare");
                formattedInfo += "\n\tDensity: " + star.RelativeAvgDensity + " Relative to the Sun";
                formattedInfo += "\n\tCore Temperature: " + star.Core_temperature;
                formattedInfo += "\n\tEffective Temperature: " + star.Surface_temperature;
                formattedInfo += "\n\tStar Class: " + star.StarClass.ToString();
                formattedInfo += "\n\tVega-relative chromaticity: " + star.StarColor.ToString();
                formattedInfo += "\n\t" + star.StarComposition.ToString();



            }

            return formattedInfo;
        }
    }
}
