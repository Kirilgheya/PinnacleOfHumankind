namespace POH_UI
{
	partial class CreationKitForm
	{
		/// <summary>
		/// Variabile di progettazione necessaria.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Pulire le risorse in uso.
		/// </summary>
		/// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Codice generato da Progettazione Windows Form

		/// <summary>
		/// Metodo necessario per il supporto della finestra di progettazione. Non modificare
		/// il contenuto del metodo con l'editor di codice.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.NucleusClassTxt = new System.Windows.Forms.ComboBox();
            this.PlanetClassTxt = new System.Windows.Forms.ComboBox();
            this.ElementCompositionGrid = new System.Windows.Forms.DataGridView();
            this.Element = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Element_Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PlanetSeedGrid = new System.Windows.Forms.DataGridView();
            this.planetSeedCompositionBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.PlanetGrid = new System.Windows.Forms.DataGridView();
            this.StartGame = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SolarRadii = new System.Windows.Forms.TextBox();
            this.SolarMasses = new System.Windows.Forms.TextBox();
            this.StarSeeds_Grid = new System.Windows.Forms.DataGridView();
            this.GeneratedStar_Grid = new System.Windows.Forms.DataGridView();
            this.GenerateStar = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quantityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn4 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn5 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Stars = new System.Windows.Forms.BindingSource(this.components);
            this.StarSeedElements = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.periodicTableUIBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.quantityDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.starSeedBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.FullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.radiusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.massDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Luminosity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Age = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ElementCompositionGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlanetSeedGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.planetSeedCompositionBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlanetGrid)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StarSeeds_Grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GeneratedStar_Grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Stars)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodicTableUIBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.starSeedBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(1, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(998, 466);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(990, 437);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Planet generation";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.NucleusClassTxt);
            this.groupBox1.Controls.Add(this.PlanetClassTxt);
            this.groupBox1.Controls.Add(this.ElementCompositionGrid);
            this.groupBox1.Controls.Add(this.PlanetSeedGrid);
            this.groupBox1.Controls.Add(this.PlanetGrid);
            this.groupBox1.Controls.Add(this.StartGame);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(970, 398);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Menu";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // NucleusClassTxt
            // 
            this.NucleusClassTxt.FormattingEnabled = true;
            this.NucleusClassTxt.Location = new System.Drawing.Point(48, 151);
            this.NucleusClassTxt.Name = "NucleusClassTxt";
            this.NucleusClassTxt.Size = new System.Drawing.Size(121, 24);
            this.NucleusClassTxt.TabIndex = 8;
            // 
            // PlanetClassTxt
            // 
            this.PlanetClassTxt.FormattingEnabled = true;
            this.PlanetClassTxt.Location = new System.Drawing.Point(48, 103);
            this.PlanetClassTxt.Name = "PlanetClassTxt";
            this.PlanetClassTxt.Size = new System.Drawing.Size(121, 24);
            this.PlanetClassTxt.TabIndex = 7;
            // 
            // ElementCompositionGrid
            // 
            this.ElementCompositionGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ElementCompositionGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Element,
            this.Element_Quantity});
            this.ElementCompositionGrid.Location = new System.Drawing.Point(16, 227);
            this.ElementCompositionGrid.MultiSelect = false;
            this.ElementCompositionGrid.Name = "ElementCompositionGrid";
            this.ElementCompositionGrid.RowHeadersWidth = 51;
            this.ElementCompositionGrid.RowTemplate.Height = 24;
            this.ElementCompositionGrid.Size = new System.Drawing.Size(301, 150);
            this.ElementCompositionGrid.TabIndex = 6;
            this.ElementCompositionGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView3_CellContentClick);
            // 
            // Element
            // 
            this.Element.HeaderText = "Element";
            this.Element.MinimumWidth = 6;
            this.Element.Name = "Element";
            this.Element.Width = 125;
            // 
            // Element_Quantity
            // 
            this.Element_Quantity.HeaderText = "Quantity (%)";
            this.Element_Quantity.MaxInputLength = 3;
            this.Element_Quantity.MinimumWidth = 6;
            this.Element_Quantity.Name = "Element_Quantity";
            this.Element_Quantity.Width = 125;
            // 
            // PlanetSeedGrid
            // 
            this.PlanetSeedGrid.AllowUserToOrderColumns = true;
            this.PlanetSeedGrid.AutoGenerateColumns = false;
            this.PlanetSeedGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PlanetSeedGrid.DataSource = this.planetSeedCompositionBindingSource1;
            this.PlanetSeedGrid.Location = new System.Drawing.Point(339, 227);
            this.PlanetSeedGrid.Name = "PlanetSeedGrid";
            this.PlanetSeedGrid.RowHeadersWidth = 51;
            this.PlanetSeedGrid.RowTemplate.Height = 24;
            this.PlanetSeedGrid.Size = new System.Drawing.Size(625, 150);
            this.PlanetSeedGrid.TabIndex = 3;
            // 
            // planetSeedCompositionBindingSource1
            // 
            this.planetSeedCompositionBindingSource1.DataMember = "planetSeedComposition";
            // 
            // PlanetGrid
            // 
            this.PlanetGrid.AllowUserToOrderColumns = true;
            this.PlanetGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.PlanetGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.PlanetGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PlanetGrid.Location = new System.Drawing.Point(339, 65);
            this.PlanetGrid.Name = "PlanetGrid";
            this.PlanetGrid.RowHeadersWidth = 51;
            this.PlanetGrid.RowTemplate.Height = 24;
            this.PlanetGrid.Size = new System.Drawing.Size(625, 156);
            this.PlanetGrid.TabIndex = 2;
            // 
            // StartGame
            // 
            this.StartGame.Location = new System.Drawing.Point(48, 39);
            this.StartGame.Name = "StartGame";
            this.StartGame.Size = new System.Drawing.Size(110, 40);
            this.StartGame.TabIndex = 0;
            this.StartGame.Text = "Create";
            this.StartGame.UseVisualStyleBackColor = true;
            this.StartGame.Click += new System.EventHandler(this.StartGame_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(990, 437);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Star generation";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.SolarRadii);
            this.groupBox2.Controls.Add(this.SolarMasses);
            this.groupBox2.Controls.Add(this.StarSeeds_Grid);
            this.groupBox2.Controls.Add(this.GeneratedStar_Grid);
            this.groupBox2.Controls.Add(this.GenerateStar);
            this.groupBox2.Location = new System.Drawing.Point(7, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(882, 367);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Menu";
            this.groupBox2.Enter += new System.EventHandler(this.GroupBox2_Enter);
            // 
            // SolarRadii
            // 
            this.SolarRadii.Location = new System.Drawing.Point(776, 268);
            this.SolarRadii.Name = "SolarRadii";
            this.SolarRadii.Size = new System.Drawing.Size(100, 22);
            this.SolarRadii.TabIndex = 9;
            // 
            // SolarMasses
            // 
            this.SolarMasses.Location = new System.Drawing.Point(776, 224);
            this.SolarMasses.Name = "SolarMasses";
            this.SolarMasses.Size = new System.Drawing.Size(100, 22);
            this.SolarMasses.TabIndex = 8;
            // 
            // StarSeeds_Grid
            // 
            this.StarSeeds_Grid.AutoGenerateColumns = false;
            this.StarSeeds_Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.StarSeeds_Grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StarSeedElements,
            this.quantityDataGridViewTextBoxColumn1});
            this.StarSeeds_Grid.DataMember = "seed";
            this.StarSeeds_Grid.DataSource = this.starSeedBindingSource;
            this.StarSeeds_Grid.Location = new System.Drawing.Point(296, 196);
            this.StarSeeds_Grid.MultiSelect = false;
            this.StarSeeds_Grid.Name = "StarSeeds_Grid";
            this.StarSeeds_Grid.RowHeadersWidth = 51;
            this.StarSeeds_Grid.RowTemplate.Height = 24;
            this.StarSeeds_Grid.Size = new System.Drawing.Size(459, 150);
            this.StarSeeds_Grid.TabIndex = 7;
            this.StarSeeds_Grid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick);
            // 
            // GeneratedStar_Grid
            // 
            this.GeneratedStar_Grid.AllowUserToOrderColumns = true;
            this.GeneratedStar_Grid.AutoGenerateColumns = false;
            this.GeneratedStar_Grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.GeneratedStar_Grid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.GeneratedStar_Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GeneratedStar_Grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FullName,
            this.Type,
            this.radiusDataGridViewTextBoxColumn,
            this.massDataGridViewTextBoxColumn,
            this.Luminosity,
            this.Age});
            this.GeneratedStar_Grid.DataSource = this.Stars;
            this.GeneratedStar_Grid.Location = new System.Drawing.Point(296, 21);
            this.GeneratedStar_Grid.Name = "GeneratedStar_Grid";
            this.GeneratedStar_Grid.RowHeadersWidth = 51;
            this.GeneratedStar_Grid.RowTemplate.Height = 24;
            this.GeneratedStar_Grid.Size = new System.Drawing.Size(558, 156);
            this.GeneratedStar_Grid.TabIndex = 2;
            this.GeneratedStar_Grid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GeneratedStar_Grid_CellContentClick);
            // 
            // GenerateStar
            // 
            this.GenerateStar.Location = new System.Drawing.Point(48, 39);
            this.GenerateStar.Name = "GenerateStar";
            this.GenerateStar.Size = new System.Drawing.Size(110, 40);
            this.GenerateStar.TabIndex = 0;
            this.GenerateStar.Text = "Setup";
            this.GenerateStar.UseVisualStyleBackColor = true;
            this.GenerateStar.Click += new System.EventHandler(this.GenerateStar_Click);
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "planetClass";
            this.dataGridViewTextBoxColumn12.HeaderText = "planetClass";
            this.dataGridViewTextBoxColumn12.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.Width = 125;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.DataPropertyName = "nucleusClass";
            this.dataGridViewTextBoxColumn13.HeaderText = "nucleusClass";
            this.dataGridViewTextBoxColumn13.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.Width = 125;
            // 
            // quantityDataGridViewTextBoxColumn
            // 
            this.quantityDataGridViewTextBoxColumn.DataPropertyName = "quantity";
            this.quantityDataGridViewTextBoxColumn.HeaderText = "quantity";
            this.quantityDataGridViewTextBoxColumn.MaxInputLength = 3;
            this.quantityDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.quantityDataGridViewTextBoxColumn.Name = "quantityDataGridViewTextBoxColumn";
            this.quantityDataGridViewTextBoxColumn.Width = 125;
            // 
            // dataGridViewComboBoxColumn1
            // 
            this.dataGridViewComboBoxColumn1.DataPropertyName = "element";
            this.dataGridViewComboBoxColumn1.HeaderText = "Elements";
            this.dataGridViewComboBoxColumn1.MaxDropDownItems = 70;
            this.dataGridViewComboBoxColumn1.MinimumWidth = 6;
            this.dataGridViewComboBoxColumn1.Name = "dataGridViewComboBoxColumn1";
            this.dataGridViewComboBoxColumn1.Width = 125;
            // 
            // dataGridViewComboBoxColumn2
            // 
            this.dataGridViewComboBoxColumn2.DataPropertyName = "element";
            this.dataGridViewComboBoxColumn2.HeaderText = "Elements";
            this.dataGridViewComboBoxColumn2.MaxDropDownItems = 70;
            this.dataGridViewComboBoxColumn2.MinimumWidth = 6;
            this.dataGridViewComboBoxColumn2.Name = "dataGridViewComboBoxColumn2";
            this.dataGridViewComboBoxColumn2.Width = 125;
            // 
            // dataGridViewComboBoxColumn3
            // 
            this.dataGridViewComboBoxColumn3.DataPropertyName = "element";
            this.dataGridViewComboBoxColumn3.HeaderText = "Elements";
            this.dataGridViewComboBoxColumn3.MaxDropDownItems = 70;
            this.dataGridViewComboBoxColumn3.MinimumWidth = 6;
            this.dataGridViewComboBoxColumn3.Name = "dataGridViewComboBoxColumn3";
            this.dataGridViewComboBoxColumn3.Width = 125;
            // 
            // dataGridViewComboBoxColumn4
            // 
            this.dataGridViewComboBoxColumn4.DataPropertyName = "element";
            this.dataGridViewComboBoxColumn4.HeaderText = "Elements";
            this.dataGridViewComboBoxColumn4.MaxDropDownItems = 70;
            this.dataGridViewComboBoxColumn4.MinimumWidth = 6;
            this.dataGridViewComboBoxColumn4.Name = "dataGridViewComboBoxColumn4";
            this.dataGridViewComboBoxColumn4.Width = 125;
            // 
            // dataGridViewComboBoxColumn5
            // 
            this.dataGridViewComboBoxColumn5.DataPropertyName = "element";
            this.dataGridViewComboBoxColumn5.HeaderText = "Elements";
            this.dataGridViewComboBoxColumn5.MinimumWidth = 6;
            this.dataGridViewComboBoxColumn5.Name = "dataGridViewComboBoxColumn5";
            this.dataGridViewComboBoxColumn5.Width = 125;
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Stars, "Luminosity", true));
            this.textBox1.Location = new System.Drawing.Point(69, 122);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 10;
            // 
            // Stars
            // 
            this.Stars.AllowNew = true;
            this.Stars.DataSource = typeof(MainGame.UI.DataModel.Star);
            // 
            // StarSeedElements
            // 
            this.StarSeedElements.DataPropertyName = "element";
            this.StarSeedElements.DataSource = this.periodicTableUIBindingSource;
            this.StarSeedElements.DisplayMember = "name";
            this.StarSeedElements.HeaderText = "Elements";
            this.StarSeedElements.MinimumWidth = 6;
            this.StarSeedElements.Name = "StarSeedElements";
            this.StarSeedElements.ValueMember = "Self";
            this.StarSeedElements.Width = 125;
            // 
            // periodicTableUIBindingSource
            // 
            this.periodicTableUIBindingSource.DataMember = "periodicTable_UI";
            this.periodicTableUIBindingSource.DataSource = typeof(MainGame.UI.DataModel.ChemicalElements_ComboBox_Ds);
            // 
            // quantityDataGridViewTextBoxColumn1
            // 
            this.quantityDataGridViewTextBoxColumn1.DataPropertyName = "quantity";
            this.quantityDataGridViewTextBoxColumn1.HeaderText = "quantity";
            this.quantityDataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.quantityDataGridViewTextBoxColumn1.Name = "quantityDataGridViewTextBoxColumn1";
            this.quantityDataGridViewTextBoxColumn1.Width = 125;
            // 
            // starSeedBindingSource
            // 
            this.starSeedBindingSource.DataSource = typeof(MainGame.UI.DataModel.StarSeed);
            // 
            // FullName
            // 
            this.FullName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.FullName.DataPropertyName = "FullName";
            this.FullName.HeaderText = "FullName";
            this.FullName.MinimumWidth = 6;
            this.FullName.Name = "FullName";
            this.FullName.ReadOnly = true;
            this.FullName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FullName.Width = 73;
            // 
            // Type
            // 
            this.Type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Type.DataPropertyName = "Type";
            this.Type.HeaderText = "Type";
            this.Type.MinimumWidth = 6;
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.Width = 69;
            // 
            // radiusDataGridViewTextBoxColumn
            // 
            this.radiusDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.radiusDataGridViewTextBoxColumn.DataPropertyName = "Radius";
            this.radiusDataGridViewTextBoxColumn.HeaderText = "Radius";
            this.radiusDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.radiusDataGridViewTextBoxColumn.Name = "radiusDataGridViewTextBoxColumn";
            this.radiusDataGridViewTextBoxColumn.ReadOnly = true;
            this.radiusDataGridViewTextBoxColumn.Width = 81;
            // 
            // massDataGridViewTextBoxColumn
            // 
            this.massDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.massDataGridViewTextBoxColumn.DataPropertyName = "Mass";
            this.massDataGridViewTextBoxColumn.HeaderText = "Mass";
            this.massDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.massDataGridViewTextBoxColumn.Name = "massDataGridViewTextBoxColumn";
            this.massDataGridViewTextBoxColumn.ReadOnly = true;
            this.massDataGridViewTextBoxColumn.Width = 70;
            // 
            // Luminosity
            // 
            this.Luminosity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Luminosity.DataPropertyName = "Luminosity";
            this.Luminosity.HeaderText = "Luminosity";
            this.Luminosity.MinimumWidth = 6;
            this.Luminosity.Name = "Luminosity";
            this.Luminosity.ReadOnly = true;
            this.Luminosity.Width = 104;
            // 
            // Age
            // 
            this.Age.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Age.DataPropertyName = "Age";
            this.Age.HeaderText = "Age";
            this.Age.MinimumWidth = 6;
            this.Age.Name = "Age";
            this.Age.ReadOnly = true;
            this.Age.Width = 62;
            // 
            // CreationKitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(999, 469);
            this.Controls.Add(this.tabControl1);
            this.Name = "CreationKitForm";
            this.Text = "Pinnacle of Humankind";
            this.Load += new System.EventHandler(this.CreationKit_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ElementCompositionGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlanetSeedGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.planetSeedCompositionBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlanetGrid)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StarSeeds_Grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GeneratedStar_Grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Stars)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.periodicTableUIBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.starSeedBindingSource)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.BindingSource Stars;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.DataGridView PlanetGrid;
		private System.Windows.Forms.Button StartGame;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.DataGridView GeneratedStar_Grid;
		private System.Windows.Forms.Button GenerateStar;
		private System.Windows.Forms.DataGridView PlanetSeedGrid;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
		private System.Windows.Forms.BindingSource planetSeedCompositionBindingSource1;
		private System.Windows.Forms.DataGridView ElementCompositionGrid;
		private System.Windows.Forms.DataGridViewComboBoxColumn Element;
		private System.Windows.Forms.DataGridViewTextBoxColumn Element_Quantity;
		private System.Windows.Forms.ComboBox NucleusClassTxt;
		private System.Windows.Forms.ComboBox PlanetClassTxt;
        private System.Windows.Forms.DataGridView StarSeeds_Grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.BindingSource starSeedBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn1;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn2;
        private System.Windows.Forms.BindingSource periodicTableUIBindingSource;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn3;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn4;
        private System.Windows.Forms.TextBox SolarMasses;
        private System.Windows.Forms.DataGridViewComboBoxColumn StarSeedElements;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantityDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn5;
        private System.Windows.Forms.TextBox SolarRadii;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn FullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn radiusDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn massDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Luminosity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Age;
    }
}

