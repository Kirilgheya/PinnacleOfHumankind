using System;
using System.Collections.Generic;
using System.Text;

namespace MainGame.Applicazione.OtherParametersStuff
{
	static class SeedParameters
	{

		public static class MetallicityParams
		{

			private static double hel_to_hidrogRatio = 0.3246251;

			public static double helFraction;
			public static double hidrogFraction;
			public static double otherStuffFraction;
			public static double metallicityFraction;

			public static void generateFromParams()
			{

				if(helFraction > 0.0 && hidrogFraction > 0.0)
				{

					metallicityFraction = 1.00 - (helFraction + hidrogFraction);
				}
			}
		}

		public static class LuminosityParams
		{

			public static double temperature; 
			public static double radius; 
			public static double luminosity;
			public static double stefan_BoltzmannConstant = 5.67 * Math.Pow(10, -8.0);  // Watt di potenza per m^(-2) * k^(-4)

			public static void calcLuminosity()
			{

				luminosity = 4 * Math.PI * (Math.Pow(radius, 2.0)) * stefan_BoltzmannConstant * Math.Pow(temperature,4);
			}

			public static void calcLuminosityFromRel(double _relRadius, double _relTemperature)
			{

				// L = (radiusRel in km) ^ 2 * (surfTemp in K) ^ 4;
				 luminosity = Math.Pow(_relRadius * ParametriUtente.Science.r_sun, 2.00)
									* Math.Pow(_relTemperature * ParametriUtente.Science.surfacetemp_sun, 4.00);			
			}
		}

	}
}
