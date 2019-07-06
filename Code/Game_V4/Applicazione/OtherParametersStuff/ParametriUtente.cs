using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MainGame.Applicazione
{
    public static class ParametriUtente
    {
        public static string exeRootFodler = Application.StartupPath;


        public static class Science
		{
            
		    public static double g_t = 9.80665; //accelerazione di gravità sulla Terra in m/s^2
			public static double G = 6.67 * (Math.Pow(10.0, -11.0)); //costante di gravitazione universale
			public static double m_t = 5.97219 * (Math.Pow(10.0, 27.0)); //massa terrestre in g
			public static double r_t = 6.3725 * (Math.Pow(10.0,8.0)); //raggio terra in Cm
			public static double d_t = 5.51; //densità terra g/cm^3
			public static double v_t = 1083206916846.0; //volume della terra in km3
			public static List<DataModel.ChemicalElement> knownElements = new List<DataModel.ChemicalElement>(103); //default is 103

            public static double g_sun = g_t * 28.02;
            public static double m_sun = 1.9885 * Math.Pow(10.00, 30.00); //kg
			public static double r_sun = 695700.00; //Km
			public static double surfacetemp_sun = 5.778 * Math.Pow(10.00,3); //Kelvin
			public static double coretemp_sun = 1.57 * Math.Pow(10.00, 7.00);
			public static double lum_sun = 3.828 * Math.Pow(10.0,26.0);
            public static double avg_d_sun = 1.408; //1.408 g/cm3
            public static double core_d_sun = 162.2; //162.2 g/cm3
            public static double v_sun = 1.41 * Math.Pow(10, 18); //km^3
            public static double p_coreSun = 2.49 * Math.Pow(10, 16);
            //public static Function hydrostaticEquilibrium = new Function("HiEq(d,g,h)=(-1*d)*g*h");
            public static Function hydrostaticEquilibrium = new Function("HiEq(G,M,p,r)=(G*M*p)/r");
        }

		public static Dictionary<char, int> keybind = new Dictionary<char, int>()
		{
			{ 'q',1 },
			
		};

		public static Dictionary<int, Command> relatedFunction = new Dictionary<int, Command>()
		{
			{1,new Commands.ExitCommand() },
			{2,new Commands.NewGame() },
			{3,new Commands.Options() },
			{4,new Commands.LoadGame() },
			{5,new Commands.Help() }
		};
		/**
		public static Dictionary<int, int> commandCode = new Dictionary<int, int>()
		{
			{1,1}, //exit game
			{21,2}, //new game
			{22,3}, //options
			{23,5}, //loadGame
			{2,5} //help
		};
		**/
		public static void resetToGlobal()
		{
			 keybind = new Dictionary<char, int>()
			{
				{ 'q',1 },
				{ 'n',2 },
				{ 'h',5 }
			};

			relatedFunction = new Dictionary<int, Command>()
			{
				{1,new Commands.ExitCommand() },
				{2,new Commands.NewGame() },
				{3,new Commands.Options() },
				{4,new Commands.LoadGame() },
				{5,new Commands.Help() }
			};
			/**
			commandCode = new Dictionary<int, int>()
			{
				{11,1}, //quitGame
				{21,2}, //new game
				{22,3}, //options
				{23,4} //loadGame
			};
			**/
	}
		/*
		 * keybind			q, 1	  | c, 2
		 * relatedFunction	1, exit() | 2, menu()
		 * commandCode		103, 1	  | 155, 2
		 *
		 */
	}
}
