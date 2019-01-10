using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Applicazione.DataModel
{
    class Planet
    {
		private PlanetClass planetClass;
		private NucleusClass nucleusClass;
		private List<ChemicalElement> composition;

		public double relativeMass;
		public double relativeVolume;
		public double relativeRadius;
		public double relativeg;
		public double relativeAvgDensity;

		private double mass;
		private double volume;
		private double radius;
		private double g_atSeaLevel;
		private double average_density;

		public void initPlanetStats()
		{
			this.mass = UOMHandler.getPlanetMass(this.relativeMass);
			this.volume = UOMHandler.getPlanetVolume(this.relativeVolume);
			this.radius = UOMHandler.getPlanetRadius(this.relativeRadius);
			this.g_atSeaLevel = UOMHandler.getPlanetG(this.relativeg);
			this.average_density = UOMHandler.getPlanetDensity(this.relativeAvgDensity);
		}
	}
}
