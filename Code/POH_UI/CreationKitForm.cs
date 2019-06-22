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
			
			List<DataChemicalElement.ChemicalElement> elements = new List<DataChemicalElement.ChemicalElement>();
			

			PlanetSeed seed = new PlanetSeed(elements);
			seed.PlanetClass = this.PlanetClassTxt.Text;
			seed.NucleusClass = this.NucleusClassTxt.Text;

            this.initElementsDropDown(elements);

			List<DataChemicalElement.ChemicalElement> listOfSeedElements = new List<DataChemicalElement.ChemicalElement>();
			foreach(DataChemicalElement.ChemicalElement element in elements)
			{
				DataChemicalElement.ChemicalElement foundElement;
				foundElement = element;
				listOfSeedElements.Add(foundElement);
			    
				
			}

			seed.planetSeedComposition = elements;
			this.SeedPlanet.Add(seed);

			//Logic starts here



			//Logic ends here

			this.resetInputValues();
		}

        private void initElementsDropDown(List<DataChemicalElement.ChemicalElement> elements)
        {
            foreach (DataGridViewRow row in this.ElementCompositionGrid.Rows)
            {
                if (!row.IsNewRow)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.ValueType == typeof(DataChemicalElement.ChemicalElement))
                        {
                            ChemicalElement element = new ChemicalElement();
                            element.initElementDataFromFather((DataChemicalElement.ChemicalElement)cell.Value);
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

		private void CreationKit_Load(object sender, EventArgs e)
		{
			Partita newGame = new Partita();
			newGame = Partita.createPartita_form();
			this.sessione = newGame;
			
          

            DataEngine engine = new DataEngine();

			periodicTable = new List<DataChemicalElement.ChemicalElement>();

			periodicTable = engine.getPeriodicTable(0);

            BindingSource comboBs = new BindingSource();

            //this.Element.ValueType = typeof(DataChemicalElement.ChemicalElement);
            //foreach(Object obj in Enum.GetValues(typeof(Elements)))
            foreach (DataChemicalElement.ChemicalElement obj in periodicTable)
            {
                comboBs.Add(obj);
                //this.Element.Items.Add(obj);
               
            }

          //  this.Element.DataPropertyName = "name";
            this.Element.DataSource = comboBs;
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
	}
}
