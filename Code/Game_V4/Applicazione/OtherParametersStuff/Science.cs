using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainGame.Applicazione.DataModel; 

namespace MainGame.Applicazione
{
	public class Science : INotifyPropertyChanged
	{

		public static double g_t = 9.80665; //accelerazione di gravità sulla Terra in m/s^2
		public static double G = 6.67 * (Math.Pow(10.0, -11.0)); //costante di gravitazione universale
		public static double m_t = 5.97219 * (Math.Pow(10.0, 27.0)); //massa terrestre in g
		public static double r_t = 6.3725 * (Math.Pow(10.0, 8.0)); //raggio terra in Cm
		public static double d_t = 5.51; //densità terra g/cm^3
		public static double v_t = 1083206916846.0; //volume della terra in km3
		public  List<ChemicalElement> knownElements = new List<ChemicalElement>(103); //default is 103

		public static double m_sun = 1.9885 * Math.Pow(10.00, 30.00);
		public static double r_sun = 695700.00; //Km
		public static double surfacetemp_sun = 5.778 * Math.Pow(10.00, 3); //Kelvin
		public static double coretemp_sun = 1.57 * Math.Pow(10.00, 7.00);
        public static double lum_sun = 3.828 * Math.Pow(10.0, 26.0);

		public event PropertyChangedEventHandler PropertyChanged;



		private void OnPropertyChanged(string propertyName)
		{
			var handler = PropertyChanged;

        if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
