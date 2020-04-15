using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainGame.Applicazione.DataModel.Utils
{
    class ModifierList
    {

        List<String> modifierName;

        List<double> modifierValue;

        public ModifierList()
        {
            this.modifierName = new List<string>();
            this.modifierValue = new List<double>();
        }

        public void resetModifiers()
        {

            this.modifierName = new List<string>();
            this.modifierValue = new List<double>();
        }

        public double getSumOfValues()
        {

            return this.modifierValue.Sum();
        }

        public void addModifier(string _modifierName, double _modifierValue)
        {
            double presentValue = 0;
            if (this.modifierName.Contains(_modifierName))
            {
                int presentIndex = this.modifierName.IndexOf(_modifierName);
                presentValue = this.modifierValue.ElementAt(presentIndex);
                this.modifierName.Remove(_modifierName);
                this.modifierValue.RemoveAt(presentIndex);
            }

            this.modifierName.Add(_modifierName);
            this.modifierValue.Add(_modifierValue + presentValue);
        }

        public void removeModifier(string _modifierName)
        {

            if (this.modifierName.Contains(_modifierName))
            {
                int presentIndex = this.modifierName.IndexOf(_modifierName);
                
                this.modifierName.Remove(_modifierName);
                this.modifierValue.RemoveAt(presentIndex);
            }
        }

        public double getModifierValue(string _modifierName)
        {

            double presentValue = 0;

            if (this.modifierName.Contains(_modifierName))
            {
                int presentIndex = this.modifierName.IndexOf(_modifierName);

                presentValue = this.modifierValue.ElementAt(presentIndex);
            }

            return presentValue;
        }
    }
}
