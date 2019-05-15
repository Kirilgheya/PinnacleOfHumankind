namespace POH_UI
{
	partial class Form1
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
			this.planetSeedCompositionBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
			this.PlanetSeedGrid = new System.Windows.Forms.DataGridView();
			this.PlanetGrid = new System.Windows.Forms.DataGridView();
			this.StartGame = new System.Windows.Forms.Button();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridView2 = new System.Windows.Forms.DataGridView();
			this.button1 = new System.Windows.Forms.Button();
			this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ElementCompositionGrid = new System.Windows.Forms.DataGridView();
			this.Element = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.Element_Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.SeedPlanet = new System.Windows.Forms.BindingSource(this.components);
			this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.massDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.volumeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.radiusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.gravityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.densityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Planets = new System.Windows.Forms.BindingSource(this.components);
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Stars = new System.Windows.Forms.BindingSource(this.components);
			this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.planetClassDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.nucleusClassDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.planetCompositionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.PlanetClassTxt = new System.Windows.Forms.ComboBox();
			this.NucleusClassTxt = new System.Windows.Forms.ComboBox();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.planetSeedCompositionBindingSource1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PlanetSeedGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PlanetGrid)).BeginInit();
			this.tabPage2.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ElementCompositionGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SeedPlanet)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Planets)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Stars)).BeginInit();
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
			// planetSeedCompositionBindingSource1
			// 
			this.planetSeedCompositionBindingSource1.DataMember = "planetSeedComposition";
			this.planetSeedCompositionBindingSource1.DataSource = this.SeedPlanet;
			// 
			// PlanetSeedGrid
			// 
			this.PlanetSeedGrid.AllowUserToOrderColumns = true;
			this.PlanetSeedGrid.AutoGenerateColumns = false;
			this.PlanetSeedGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.PlanetSeedGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.planetClassDataGridViewTextBoxColumn,
            this.nucleusClassDataGridViewTextBoxColumn,
            this.planetCompositionDataGridViewTextBoxColumn});
			this.PlanetSeedGrid.DataSource = this.SeedPlanet;
			this.PlanetSeedGrid.Location = new System.Drawing.Point(339, 227);
			this.PlanetSeedGrid.Name = "PlanetSeedGrid";
			this.PlanetSeedGrid.RowTemplate.Height = 24;
			this.PlanetSeedGrid.Size = new System.Drawing.Size(625, 150);
			this.PlanetSeedGrid.TabIndex = 3;
			// 
			// PlanetGrid
			// 
			this.PlanetGrid.AllowUserToOrderColumns = true;
			this.PlanetGrid.AutoGenerateColumns = false;
			this.PlanetGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
			this.PlanetGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
			this.PlanetGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.PlanetGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.massDataGridViewTextBoxColumn,
            this.volumeDataGridViewTextBoxColumn,
            this.radiusDataGridViewTextBoxColumn,
            this.gravityDataGridViewTextBoxColumn,
            this.densityDataGridViewTextBoxColumn});
			this.PlanetGrid.DataSource = this.Planets;
			this.PlanetGrid.Location = new System.Drawing.Point(339, 65);
			this.PlanetGrid.Name = "PlanetGrid";
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
			this.groupBox2.Controls.Add(this.dataGridView1);
			this.groupBox2.Controls.Add(this.dataGridView2);
			this.groupBox2.Controls.Add(this.button1);
			this.groupBox2.Location = new System.Drawing.Point(3, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(725, 398);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Menu";
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToOrderColumns = true;
			this.dataGridView1.AutoGenerateColumns = false;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5});
			this.dataGridView1.DataSource = this.Stars;
			this.dataGridView1.Location = new System.Drawing.Point(190, 217);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowTemplate.Height = 24;
			this.dataGridView1.Size = new System.Drawing.Size(529, 160);
			this.dataGridView1.TabIndex = 3;
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn2.DataPropertyName = "Luminosity";
			this.dataGridViewTextBoxColumn2.HeaderText = "Luminosity";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			// 
			// dataGridViewTextBoxColumn3
			// 
			this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn3.DataPropertyName = "Metallicity";
			this.dataGridViewTextBoxColumn3.HeaderText = "Metallicity";
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			// 
			// dataGridViewTextBoxColumn4
			// 
			this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn4.DataPropertyName = "Age";
			this.dataGridViewTextBoxColumn4.HeaderText = "Age";
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			// 
			// dataGridViewTextBoxColumn5
			// 
			this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn5.DataPropertyName = "Radius";
			this.dataGridViewTextBoxColumn5.HeaderText = "Radius";
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			// 
			// dataGridView2
			// 
			this.dataGridView2.AllowUserToOrderColumns = true;
			this.dataGridView2.AutoGenerateColumns = false;
			this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
			this.dataGridView2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
			this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11});
			this.dataGridView2.DataSource = this.Planets;
			this.dataGridView2.Location = new System.Drawing.Point(190, 55);
			this.dataGridView2.Name = "dataGridView2";
			this.dataGridView2.RowTemplate.Height = 24;
			this.dataGridView2.Size = new System.Drawing.Size(529, 156);
			this.dataGridView2.TabIndex = 2;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(48, 39);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(110, 40);
			this.button1.TabIndex = 0;
			this.button1.Text = "Setup";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// dataGridViewTextBoxColumn12
			// 
			this.dataGridViewTextBoxColumn12.DataPropertyName = "planetClass";
			this.dataGridViewTextBoxColumn12.HeaderText = "planetClass";
			this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
			// 
			// dataGridViewTextBoxColumn13
			// 
			this.dataGridViewTextBoxColumn13.DataPropertyName = "nucleusClass";
			this.dataGridViewTextBoxColumn13.HeaderText = "nucleusClass";
			this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
			// 
			// ElementCompositionGrid
			// 
			this.ElementCompositionGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.ElementCompositionGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Element,
            this.Element_Quantity});
			this.ElementCompositionGrid.Location = new System.Drawing.Point(16, 227);
			this.ElementCompositionGrid.Name = "ElementCompositionGrid";
			this.ElementCompositionGrid.RowTemplate.Height = 24;
			this.ElementCompositionGrid.Size = new System.Drawing.Size(301, 150);
			this.ElementCompositionGrid.TabIndex = 6;
			this.ElementCompositionGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView3_CellContentClick);
			// 
			// Element
			// 
			this.Element.HeaderText = "Element";
			this.Element.Name = "Element";
			// 
			// Element_Quantity
			// 
			this.Element_Quantity.HeaderText = "Quantity (%)";
			this.Element_Quantity.MaxInputLength = 3;
			this.Element_Quantity.Name = "Element_Quantity";
			// 
			// SeedPlanet
			// 
			this.SeedPlanet.AllowNew = true;
			this.SeedPlanet.DataSource = typeof(MainGame.UI.DataModel.PlanetSeed);
			// 
			// nameDataGridViewTextBoxColumn
			// 
			this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
			this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
			this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
			this.nameDataGridViewTextBoxColumn.Width = 74;
			// 
			// massDataGridViewTextBoxColumn
			// 
			this.massDataGridViewTextBoxColumn.DataPropertyName = "Mass";
			this.massDataGridViewTextBoxColumn.HeaderText = "Mass";
			this.massDataGridViewTextBoxColumn.Name = "massDataGridViewTextBoxColumn";
			this.massDataGridViewTextBoxColumn.Width = 70;
			// 
			// volumeDataGridViewTextBoxColumn
			// 
			this.volumeDataGridViewTextBoxColumn.DataPropertyName = "Volume";
			this.volumeDataGridViewTextBoxColumn.HeaderText = "Volume";
			this.volumeDataGridViewTextBoxColumn.Name = "volumeDataGridViewTextBoxColumn";
			this.volumeDataGridViewTextBoxColumn.Width = 84;
			// 
			// radiusDataGridViewTextBoxColumn
			// 
			this.radiusDataGridViewTextBoxColumn.DataPropertyName = "Radius";
			this.radiusDataGridViewTextBoxColumn.HeaderText = "Radius";
			this.radiusDataGridViewTextBoxColumn.Name = "radiusDataGridViewTextBoxColumn";
			this.radiusDataGridViewTextBoxColumn.Width = 81;
			// 
			// gravityDataGridViewTextBoxColumn
			// 
			this.gravityDataGridViewTextBoxColumn.DataPropertyName = "Gravity";
			this.gravityDataGridViewTextBoxColumn.HeaderText = "Gravity";
			this.gravityDataGridViewTextBoxColumn.Name = "gravityDataGridViewTextBoxColumn";
			this.gravityDataGridViewTextBoxColumn.Width = 82;
			// 
			// densityDataGridViewTextBoxColumn
			// 
			this.densityDataGridViewTextBoxColumn.DataPropertyName = "Density";
			this.densityDataGridViewTextBoxColumn.HeaderText = "Density";
			this.densityDataGridViewTextBoxColumn.Name = "densityDataGridViewTextBoxColumn";
			this.densityDataGridViewTextBoxColumn.Width = 84;
			// 
			// Planets
			// 
			this.Planets.DataSource = typeof(MainGame.UI.DataModel.Planet);
			this.Planets.CurrentChanged += new System.EventHandler(this.GameVariables_CurrentChanged);
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dataGridViewTextBoxColumn1.DataPropertyName = "Radius";
			this.dataGridViewTextBoxColumn1.HeaderText = "Radius";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			// 
			// Stars
			// 
			this.Stars.AllowNew = true;
			this.Stars.DataSource = typeof(MainGame.UI.DataModel.Star);
			// 
			// dataGridViewTextBoxColumn6
			// 
			this.dataGridViewTextBoxColumn6.DataPropertyName = "Name";
			this.dataGridViewTextBoxColumn6.HeaderText = "Name";
			this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			this.dataGridViewTextBoxColumn6.Width = 74;
			// 
			// dataGridViewTextBoxColumn7
			// 
			this.dataGridViewTextBoxColumn7.DataPropertyName = "Mass";
			this.dataGridViewTextBoxColumn7.HeaderText = "Mass";
			this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
			this.dataGridViewTextBoxColumn7.Width = 70;
			// 
			// dataGridViewTextBoxColumn8
			// 
			this.dataGridViewTextBoxColumn8.DataPropertyName = "Volume";
			this.dataGridViewTextBoxColumn8.HeaderText = "Volume";
			this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
			this.dataGridViewTextBoxColumn8.Width = 84;
			// 
			// dataGridViewTextBoxColumn9
			// 
			this.dataGridViewTextBoxColumn9.DataPropertyName = "Radius";
			this.dataGridViewTextBoxColumn9.HeaderText = "Radius";
			this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
			this.dataGridViewTextBoxColumn9.Width = 81;
			// 
			// dataGridViewTextBoxColumn10
			// 
			this.dataGridViewTextBoxColumn10.DataPropertyName = "Gravity";
			this.dataGridViewTextBoxColumn10.HeaderText = "Gravity";
			this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
			this.dataGridViewTextBoxColumn10.Width = 82;
			// 
			// dataGridViewTextBoxColumn11
			// 
			this.dataGridViewTextBoxColumn11.DataPropertyName = "Density";
			this.dataGridViewTextBoxColumn11.HeaderText = "Density";
			this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
			this.dataGridViewTextBoxColumn11.Width = 84;
			// 
			// planetClassDataGridViewTextBoxColumn
			// 
			this.planetClassDataGridViewTextBoxColumn.DataPropertyName = "PlanetClass";
			this.planetClassDataGridViewTextBoxColumn.HeaderText = "PlanetClass";
			this.planetClassDataGridViewTextBoxColumn.Name = "planetClassDataGridViewTextBoxColumn";
			// 
			// nucleusClassDataGridViewTextBoxColumn
			// 
			this.nucleusClassDataGridViewTextBoxColumn.DataPropertyName = "NucleusClass";
			this.nucleusClassDataGridViewTextBoxColumn.HeaderText = "NucleusClass";
			this.nucleusClassDataGridViewTextBoxColumn.Name = "nucleusClassDataGridViewTextBoxColumn";
			// 
			// planetCompositionDataGridViewTextBoxColumn
			// 
			this.planetCompositionDataGridViewTextBoxColumn.DataPropertyName = "PlanetComposition";
			this.planetCompositionDataGridViewTextBoxColumn.HeaderText = "PlanetComposition";
			this.planetCompositionDataGridViewTextBoxColumn.Name = "planetCompositionDataGridViewTextBoxColumn";
			this.planetCompositionDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// PlanetClassTxt
			// 
			this.PlanetClassTxt.FormattingEnabled = true;
			this.PlanetClassTxt.Location = new System.Drawing.Point(48, 103);
			this.PlanetClassTxt.Name = "PlanetClassTxt";
			this.PlanetClassTxt.Size = new System.Drawing.Size(121, 24);
			this.PlanetClassTxt.TabIndex = 7;
			// 
			// NucleusClassTxt
			// 
			this.NucleusClassTxt.FormattingEnabled = true;
			this.NucleusClassTxt.Location = new System.Drawing.Point(48, 151);
			this.NucleusClassTxt.Name = "NucleusClassTxt";
			this.NucleusClassTxt.Size = new System.Drawing.Size(121, 24);
			this.NucleusClassTxt.TabIndex = 8;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ClientSize = new System.Drawing.Size(999, 469);
			this.Controls.Add(this.tabControl1);
			this.Name = "Form1";
			this.Text = "Pinnacle of Humankind";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.planetSeedCompositionBindingSource1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PlanetSeedGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PlanetGrid)).EndInit();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ElementCompositionGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SeedPlanet)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Planets)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Stars)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.BindingSource Planets;
		private System.Windows.Forms.BindingSource Stars;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.DataGridView PlanetGrid;
		private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn massDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn volumeDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn radiusDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn gravityDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn densityDataGridViewTextBoxColumn;
		private System.Windows.Forms.Button StartGame;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
		private System.Windows.Forms.DataGridView dataGridView2;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.BindingSource SeedPlanet;
		private System.Windows.Forms.DataGridView PlanetSeedGrid;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
		private System.Windows.Forms.BindingSource planetSeedCompositionBindingSource1;
		private System.Windows.Forms.DataGridView ElementCompositionGrid;
		private System.Windows.Forms.DataGridViewComboBoxColumn Element;
		private System.Windows.Forms.DataGridViewTextBoxColumn Element_Quantity;
		private System.Windows.Forms.DataGridViewTextBoxColumn planetClassDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn nucleusClassDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn planetCompositionDataGridViewTextBoxColumn;
		private System.Windows.Forms.ComboBox NucleusClassTxt;
		private System.Windows.Forms.ComboBox PlanetClassTxt;
	}
}

