using MainGame;
using DataChemicalElement = MainGame.Applicazione.DataModel;
using MainGame.UI.DataModel;
using POH_UI.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MainGame.Applicazione.Engine;
namespace POH_UI
{
	public partial class CreationKitForm : Form
	{
		private Partita sessione;
		private List<DataChemicalElement.ChemicalElement> periodicTable;
		public CreationKitForm()
		{
			InitializeComponent();
		}

		private void StartGame_Click(object sender, EventArgs e)
		{
			
			List<Elements> elements = new List<Elements>();
			

			PlanetSeed seed = new PlanetSeed(elements);
			seed.PlanetClass = this.PlanetClassTxt.Text;
			seed.NucleusClass = this.NucleusClassTxt.Text;

            this.initElementsDropDown(elements);

			List<DataChemicalElement.ChemicalElement> listOfSeedElements = new List<DataChemicalElement.ChemicalElement>();
			foreach(Elements element in elements)
			{
				DataChemicalElement.ChemicalElement foundElement;
				int counterPeriodic = 0;

				for(counterPeriodic =0; counterPeriodic<periodicTable.Count; counterPeriodic++)
				{

					if(periodicTable.ElementAt(counterPeriodic).symbol == Enum.GetName(typeof(Elements),element))
					{

						foundElement = periodicTable.ElementAt(counterPeriodic);
						listOfSeedElements.Add(foundElement);
						break;
					}
				}

			}

			seed.planetSeedComposition = elements;
			this.SeedPlanet.Add(seed);

			//Logic starts here



			//Logic ends here

			this.resetInputValues();
		}

        private void initElementsDropDown(List<Elements> elements)
        {
            foreach (DataGridViewRow row in this.ElementCompositionGrid.Rows)
            {
                if (!row.IsNewRow)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.ValueType == typeof(Elements))
                        {
                            Elements element = (Elements)cell.Value;
                            elements.Add(element);

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

		private void Form1_Load(object sender, EventArgs e)
		{
			Partita newGame = new Partita();
			newGame = Partita.createPartita_form();
			this.sessione = newGame;
			this.Element.ValueType=typeof(DataChemicalElement.ChemicalElement);

			DataEngine engine = new DataEngine();

			periodicTable = new List<DataChemicalElement.ChemicalElement>();

			periodicTable = engine.getPeriodiTable(0);


            //foreach(Object obj in Enum.GetValues(typeof(Elements)))
            foreach (DataChemicalElement.ChemicalElement obj in periodicTable)
            {

                this.Element.Items.Add(obj);
            }

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
	}
}
