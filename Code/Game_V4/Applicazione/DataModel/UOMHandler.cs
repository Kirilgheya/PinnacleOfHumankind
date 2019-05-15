using System;
using System.Collections.Generic;
using System.Text;

namespace MainGame.Applicazione.DataModel
{
    public static class UOMHandler
    {
		public static Planet planet;
		private static double mass;
		private static double volume;
		private static double radius;
		private static double g_atSeaLevel;
		private static double average_density;

		public static Planet populatePlanet(Planet _planet)
		{
			planet = _planet;
			planet.initPlanetStats();
			return planet;
		}

		public static double getPlanetMass(double _relativeMass)
		{
			mass = _relativeMass * ParametriUtente.Science.m_t;
			return mass;
		}

		public static double getPlanetDensity(double _relativeDensity)
		{
			average_density = _relativeDensity * ParametriUtente.Science.d_t;
			return average_density;
		}

		public static double getPlanetRadius(double _relativeRadius)
		{
			radius = _relativeRadius * ParametriUtente.Science.r_t;
			return radius;
		}

		public static double getPlanetG(double _relativeG)
		{
			g_atSeaLevel = _relativeG * ParametriUtente.Science.g_t;
			return g_atSeaLevel;
		}

		public static double getPlanetVolume(double _relativeVolume)
		{
			volume = _relativeVolume * ParametriUtente.Science.v_t;
			return volume;
		}
	}
}
