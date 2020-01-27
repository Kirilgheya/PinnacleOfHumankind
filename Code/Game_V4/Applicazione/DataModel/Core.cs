using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainGame.Applicazione.DataModel
{
    class Core
    {
        private NucleusClass innerNucleusClass;
        private NucleusClass outerNucleusClass;
        public Core()
        {
            this.outerNucleusClass = new NucleusClass(null);
            this.innerNucleusClass = new NucleusClass(null);
            this.innerNucleusClass.Nucleus_Class = "Solid";
            this.outerNucleusClass.Nucleus_Class = "Liquid";
        }
    }
}
