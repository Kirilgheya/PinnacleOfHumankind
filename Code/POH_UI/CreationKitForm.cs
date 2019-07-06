using MainGame;
using UI = MainGame.UI.DataModel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Applicazione.DataModel;
using MainGame.Applicazione;
using Core = MainGame.Applicazione.DataModel;

namespace POH_UI
{
    public partial class CreationKitForm : Form
	{
		private Partita sessione;
        private UI.ChemicalElements_ComboBox_Ds elementsComboBox_ds = new UI.ChemicalElements_ComboBox_Ds();
        private List<Core.Star> generatedStars = new List<Core.Star>();
        public CreationKitForm()
		{
			InitializeComponent();
		}

		private void StartGame_Click(object sender, EventArgs e)
		{
			
			 
			//this.SeedPlanet.Add(seed);

			//Logic starts here



			//Logic ends here

			this.resetInputValues();
		}

        private void getSeedsFromGrid(List<Core.ChemicalElement> elements,List<double> quantities,DataGridView _grid)
        {
            foreach (DataGridViewRow row in _grid.Rows)
            {
                if (!row.IsNewRow)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.ValueType == typeof(Core.ChemicalElement))
                        {
                            UI.ChemicalElement element = new UI.ChemicalElement();
                            element.initElementDataFromFather((Core.ChemicalElement)cell.Value);
                            elements.Add(element);

                        }
                        if (cell.ValueType == typeof(double) || cell.ValueType == typeof(int))
                        {
                            
                            quantities.Add((double)cell.Value);

                        }
                    }
                }

            }
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
		{

		}

		private void GameVariables_CurrentChanged(object sender, EventArgs e)
		{

		}

		private void PlanetGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		private void groupBox1_Enter(object sender, EventArgs e)
		{

		}

		private void CreationKit_Load(object sender, EventArgs e)
		{
			Partita newGame = new Partita();
			newGame = Partita.createPartita_form();
			this.sessione = newGame;


            PeriodicTable.init();
        

         
            this.StarSeedElements.DataSource = elementsComboBox_ds.periodicTable_UI;

            this.Element.DataSource = elementsComboBox_ds.periodicTable_UI;
            this.Element.DisplayMember = "completeName";
            this.Element.ValueMember = "Self";

            foreach (Object obj in Enum.GetValues(typeof(PlanetClassification)))
			{
				this.PlanetClassTxt.Items.Add(obj);
			}

			foreach (Object obj in Enum.GetValues(typeof(NucleusClassification)))
			{
				this.NucleusClassTxt.Items.Add(obj);
			}
		}

		private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			
		}

		private void resetInputValues()
		{
			//TODO: Implement reset for grid inputs
		}

        private void GenerateStar_Click(object sender, EventArgs e)
        {
            List<Core.ChemicalElement> elements = new List<Core.ChemicalElement>();
            List<double> quantities = new List<double>();
            double radii;
            double masses;

            getSeedsFromGrid(elements, quantities, StarSeeds_Grid);

            UI.StarSeed starSeed = new UI.StarSeed(elements, quantities);
            Double.TryParse(this.SolarMasses.Text,out radii);
            Double.TryParse(this.SolarMasses.Text, out masses);
            if (masses <= 0)
            {
                masses = 1;
            }
            if (radii <= 0)
            {
                radii = 1;
            }

            UI.Star Star = new UI.Star(masses,radii, elements, quantities);
           
            generatedStars.Add(Star);
            this.Stars.Add(Star);
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void GroupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void GeneratedStar_Grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
