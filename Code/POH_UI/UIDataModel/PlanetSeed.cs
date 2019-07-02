using System;
using System.Collections.Generic;
using System.Text;
using MainGame.Applicazione.DataModel;

namespace MainGame.UI.DataModel
{
    public class PlanetSeed : Applicazione.DataModel.Planet
	{
		protected PlanetClass planetClass { get; set; }
		protected NucleusClass nucleusClass { get; set; }
		public List<Applicazione.DataModel.ChemicalElement> planetSeedComposition { get; set; }

		public string PlanetClass { get { return this.planetClass.Planet_Class; }
			set {
				this.planetClass = new PlanetClass(this.planetSeedComposition);
				this.planetClass.Planet_Class = value;
			}
		}
		public string NucleusClass { get { return this.nucleusClass.Nucleus_Class; }
				set
			{
				this.nucleusClass = new NucleusClass(this.planetSeedComposition);
				this.nucleusClass.Nucleus_Class = value;
			}
		}
		public string PlanetComposition {
			get
			{
				string returned="";
				foreach (Applicazione.DataModel.ChemicalElement elem in this.planetSeedComposition)
				{
					if(returned.Equals(String.Empty))
					{

						returned = String.Concat(String.Empty, elem.completeName);
					}
					else
					{
						returned = String.Concat(returned, " ; ", elem.completeName);
					}
					
				}

				return returned;
			}
			
		 }



		public PlanetSeed(List<Applicazione.DataModel.ChemicalElement> _composition)
		{
			planetSeedComposition = _composition;
			this.planetClass = new PlanetClass(this.planetSeedComposition);
			this.nucleusClass = new NucleusClass(this.planetSeedComposition);

		}

	}
}
