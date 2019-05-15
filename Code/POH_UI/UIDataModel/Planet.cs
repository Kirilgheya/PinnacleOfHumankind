using System;
using System.Collections.Generic;
using System.Text;
using MainGame.Applicazione.DataModel;

namespace MainGame.UI.DataModel
{
    public class Planet : Applicazione.DataModel.Planet
	{
		private PlanetClass planetClass;
		private NucleusClass nucleusClass;
		private List<ChemicalElement> composition;
		public String Name { get; set; }
		
		public string Mass { get; set; }
		public string Volume { get; set; }
		public string Radius { get; set; }
		public string Gravity { get; set; }
		public string Density { get; set; }


		public new void  initPlanetStats()
		{
			this.name = this.Name;
			this.Mass = UOMHandler.getPlanetMass(this.relativeMass).ToString();
			this.Volume = UOMHandler.getPlanetVolume(this.relativeVolume).ToString();
			this.Radius = UOMHandler.getPlanetRadius(this.relativeRadius).ToString();
			this.Gravity = UOMHandler.getPlanetG(this.relativeg).ToString();
			this.Density = UOMHandler.getPlanetDensity(this.relativeAvgDensity).ToString();
		}
	}
}
