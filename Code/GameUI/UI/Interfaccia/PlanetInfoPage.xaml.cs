using GameUI.UI.DataSource;
using MainGame.Applicazione.Engine.CreatureEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static MainGame.Applicazione.Engine.CreatureEngine.CreatureEngine;

namespace GameUI.UI.Interfaccia
{
    /// <summary>
    /// Interaction logic for PlanetInfoPage.xaml
    /// </summary>
    public partial class PlanetInfoPage : Window
    {
        public PlanetInfoPage()
        {
            InitializeComponent();

        }

        public void LoadInfo(Star s)
        {
            cv_back.Children.Add(s.drawBody(200,200));

            txtInfo.Text = s.relatedStar.ToString_Info();
        }

        public void LoadInfo(Planet s)
        {
            cv_back.Children.Add(s.drawBody(200, 200));

            txtInfo.Text = s.relatedPlanet.ToString() + "\n\n\n" +s.relatedPlanet.flavour_text();

            List<Creature> EcoSystem = new List<Creature>();

            EcoSystem = CreatureEngine.getEcoSystem(s.relatedPlanet);

            foreach( Creature c in EcoSystem)
            {
                txtInfo.Text = txtInfo.Text + " \n\n" + c.FlavourText;
            }

            if(EcoSystem.Count == 0)
            {
                txtInfo.Text = txtInfo.Text + " \n\n" + "This planet has no life on it";
            }

           
        }

    }
}
