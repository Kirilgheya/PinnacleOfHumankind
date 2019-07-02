using System;
using System.Collections.Generic;
using System.Text;
using MainGame;

namespace MainGame.Applicazione.DataModel
{
    public class NucleusClass
    {
		public string Nucleus_Class { get { return this.nucleusClass.ToString(); }
			set
			{

				NucleusClassification classification;
				Enum.TryParse<NucleusClassification>(value, out classification);
				this.nucleusClass = classification;
			}
		}
		protected NucleusClassification nucleusClass { get; set; }
		

		public NucleusClass(List<ChemicalElement> elements)
		{

			this.nucleusClass = NucleusClassification.Liquid;
		}
    }
}
