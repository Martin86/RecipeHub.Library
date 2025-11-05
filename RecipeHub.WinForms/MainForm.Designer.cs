namespace RecipeHub.WinForms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel3 = new Panel();
            tabControl = new TabControl();
            tabPageAllRecipe = new TabPage();
            dataGridAllRecipes = new DataGridView();
            tabPageOwnRecipes = new TabPage();
            dgvMyRecipes = new DataGridView();
            tabPageFavorites = new TabPage();
            dgvMyFavorites = new DataGridView();
            tabPageAddRecipe = new TabPage();
            panel1 = new Panel();
            label18 = new Label();
            buttonAddCategory = new Button();
            flowLayoutPanelKategory = new FlowLayoutPanel();
            flowSelectedIngredients = new FlowLayoutPanel();
            buttonAddIngredient = new Button();
            comboBoxCategory = new ComboBox();
            buttonCreateIngredient = new Button();
            textBoxCreateIngredient = new TextBox();
            label9 = new Label();
            comboBoxIngredients = new ComboBox();
            label8 = new Label();
            buttonCreateCategory = new Button();
            textBoxCreateCategory = new TextBox();
            label7 = new Label();
            buttonCreateRecipe = new Button();
            textBoxDescription = new TextBox();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            textBoxRecipeName = new TextBox();
            label3 = new Label();
            tabPageEdit = new TabPage();
            panel2 = new Panel();
            buttonEditCancel = new Button();
            label17 = new Label();
            cmbEditCategory = new ComboBox();
            buttonEditSave = new Button();
            buttonEditAddCategory = new Button();
            buttonEditAddIngredient = new Button();
            flowLayoutPanelEditIngredients = new FlowLayoutPanel();
            buttonCreateIngredientInEdit = new Button();
            buttonCreateCategoryInEdit = new Button();
            flowLayoutPanelEditCategories = new FlowLayoutPanel();
            cmbEditIngredient = new ComboBox();
            textBoxEditName = new TextBox();
            textBoxCreateIngredientInEdit = new TextBox();
            textBoxCreateCategoryInEdit = new TextBox();
            textBoxEditDescription = new TextBox();
            label16 = new Label();
            label15 = new Label();
            label14 = new Label();
            label13 = new Label();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            groupBox2 = new GroupBox();
            buttonLogin = new Button();
            label2 = new Label();
            label1 = new Label();
            textBoxPassword = new TextBox();
            textBoxLoginName = new TextBox();
            buttonLogout = new Button();
            groupBox1 = new GroupBox();
            comboBoxFilterKategorie = new ComboBox();
            checkBoxFilterKategorie = new CheckBox();
            groupBoxEditCategorie = new GroupBox();
            label20 = new Label();
            label19 = new Label();
            buttonSaveEditCategory = new Button();
            textBoxEditCategorie = new TextBox();
            comboBoxEditCategory = new ComboBox();
            groupBox3 = new GroupBox();
            comboBoxFilterIngredient = new ComboBox();
            checkBoxFilterIngredient = new CheckBox();
            panel3.SuspendLayout();
            tabControl.SuspendLayout();
            tabPageAllRecipe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridAllRecipes).BeginInit();
            tabPageOwnRecipes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMyRecipes).BeginInit();
            tabPageFavorites.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMyFavorites).BeginInit();
            tabPageAddRecipe.SuspendLayout();
            panel1.SuspendLayout();
            tabPageEdit.SuspendLayout();
            panel2.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBoxEditCategorie.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // panel3
            // 
            panel3.Controls.Add(tabControl);
            panel3.Location = new Point(271, 12);
            panel3.Name = "panel3";
            panel3.Size = new Size(856, 593);
            panel3.TabIndex = 2;
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabPageAllRecipe);
            tabControl.Controls.Add(tabPageOwnRecipes);
            tabControl.Controls.Add(tabPageFavorites);
            tabControl.Controls.Add(tabPageAddRecipe);
            tabControl.Controls.Add(tabPageEdit);
            tabControl.ImeMode = ImeMode.On;
            tabControl.Location = new Point(3, 3);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(850, 587);
            tabControl.TabIndex = 0;
            tabControl.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            tabControl.Selecting += tabControl_Selecting;
            // 
            // tabPageAllRecipe
            // 
            tabPageAllRecipe.Controls.Add(dataGridAllRecipes);
            tabPageAllRecipe.Location = new Point(4, 24);
            tabPageAllRecipe.Name = "tabPageAllRecipe";
            tabPageAllRecipe.Padding = new Padding(3);
            tabPageAllRecipe.Size = new Size(842, 559);
            tabPageAllRecipe.TabIndex = 0;
            tabPageAllRecipe.Text = "Alle Rezepte";
            tabPageAllRecipe.UseVisualStyleBackColor = true;
            // 
            // dataGridAllRecipes
            // 
            dataGridAllRecipes.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridAllRecipes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridAllRecipes.Location = new Point(6, 6);
            dataGridAllRecipes.Name = "dataGridAllRecipes";
            dataGridAllRecipes.Size = new Size(830, 547);
            dataGridAllRecipes.TabIndex = 0;
            dataGridAllRecipes.CellValueChanged += dataGridAllRecipes_CellValueChanged;
            // 
            // tabPageOwnRecipes
            // 
            tabPageOwnRecipes.Controls.Add(dgvMyRecipes);
            tabPageOwnRecipes.Location = new Point(4, 24);
            tabPageOwnRecipes.Name = "tabPageOwnRecipes";
            tabPageOwnRecipes.Padding = new Padding(3);
            tabPageOwnRecipes.Size = new Size(842, 559);
            tabPageOwnRecipes.TabIndex = 1;
            tabPageOwnRecipes.Text = "Meine Rezepte";
            tabPageOwnRecipes.UseVisualStyleBackColor = true;
            // 
            // dgvMyRecipes
            // 
            dgvMyRecipes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMyRecipes.Location = new Point(6, 6);
            dgvMyRecipes.Name = "dgvMyRecipes";
            dgvMyRecipes.Size = new Size(830, 547);
            dgvMyRecipes.TabIndex = 0;
            dgvMyRecipes.CellContentClick += dgvMyRecipes_CellContentClick;
            dgvMyRecipes.CellDoubleClick += dgvMyRecipes_CellDoubleClick;
            // 
            // tabPageFavorites
            // 
            tabPageFavorites.Controls.Add(dgvMyFavorites);
            tabPageFavorites.Location = new Point(4, 24);
            tabPageFavorites.Name = "tabPageFavorites";
            tabPageFavorites.Size = new Size(842, 559);
            tabPageFavorites.TabIndex = 3;
            tabPageFavorites.Text = "Meine Favoriten";
            tabPageFavorites.UseVisualStyleBackColor = true;
            // 
            // dgvMyFavorites
            // 
            dgvMyFavorites.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMyFavorites.Location = new Point(3, 3);
            dgvMyFavorites.Name = "dgvMyFavorites";
            dgvMyFavorites.Size = new Size(836, 553);
            dgvMyFavorites.TabIndex = 0;
            // 
            // tabPageAddRecipe
            // 
            tabPageAddRecipe.Controls.Add(panel1);
            tabPageAddRecipe.Location = new Point(4, 24);
            tabPageAddRecipe.Name = "tabPageAddRecipe";
            tabPageAddRecipe.Size = new Size(842, 559);
            tabPageAddRecipe.TabIndex = 2;
            tabPageAddRecipe.Text = "Rezept anlegen";
            tabPageAddRecipe.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(label18);
            panel1.Controls.Add(buttonAddCategory);
            panel1.Controls.Add(flowLayoutPanelKategory);
            panel1.Controls.Add(flowSelectedIngredients);
            panel1.Controls.Add(buttonAddIngredient);
            panel1.Controls.Add(comboBoxCategory);
            panel1.Controls.Add(buttonCreateIngredient);
            panel1.Controls.Add(textBoxCreateIngredient);
            panel1.Controls.Add(label9);
            panel1.Controls.Add(comboBoxIngredients);
            panel1.Controls.Add(label8);
            panel1.Controls.Add(buttonCreateCategory);
            panel1.Controls.Add(textBoxCreateCategory);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(buttonCreateRecipe);
            panel1.Controls.Add(textBoxDescription);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(textBoxRecipeName);
            panel1.Controls.Add(label3);
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(836, 560);
            panel1.TabIndex = 0;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(286, 285);
            label18.Name = "label18";
            label18.Size = new Size(119, 15);
            label18.TabIndex = 23;
            label18.Text = "Kategorie auswählen:";
            // 
            // buttonAddCategory
            // 
            buttonAddCategory.Location = new Point(147, 281);
            buttonAddCategory.Name = "buttonAddCategory";
            buttonAddCategory.Size = new Size(129, 23);
            buttonAddCategory.TabIndex = 22;
            buttonAddCategory.Text = "Kategorie hinzufügen";
            buttonAddCategory.UseVisualStyleBackColor = true;
            buttonAddCategory.Click += buttonAddCategory_Click;
            // 
            // flowLayoutPanelKategory
            // 
            flowLayoutPanelKategory.BorderStyle = BorderStyle.FixedSingle;
            flowLayoutPanelKategory.Location = new Point(147, 226);
            flowLayoutPanelKategory.Name = "flowLayoutPanelKategory";
            flowLayoutPanelKategory.Size = new Size(662, 49);
            flowLayoutPanelKategory.TabIndex = 21;
            // 
            // flowSelectedIngredients
            // 
            flowSelectedIngredients.AutoScroll = true;
            flowSelectedIngredients.BorderStyle = BorderStyle.FixedSingle;
            flowSelectedIngredients.Location = new Point(147, 374);
            flowSelectedIngredients.Name = "flowSelectedIngredients";
            flowSelectedIngredients.Size = new Size(662, 58);
            flowSelectedIngredients.TabIndex = 20;
            // 
            // buttonAddIngredient
            // 
            buttonAddIngredient.Location = new Point(147, 442);
            buttonAddIngredient.Name = "buttonAddIngredient";
            buttonAddIngredient.Size = new Size(129, 23);
            buttonAddIngredient.TabIndex = 18;
            buttonAddIngredient.Text = "Zutat hinzufügen";
            buttonAddIngredient.UseVisualStyleBackColor = true;
            buttonAddIngredient.Click += buttonAddIngredient_Click;
            // 
            // comboBoxCategory
            // 
            comboBoxCategory.FormattingEnabled = true;
            comboBoxCategory.Location = new Point(413, 282);
            comboBoxCategory.Name = "comboBoxCategory";
            comboBoxCategory.Size = new Size(121, 23);
            comboBoxCategory.TabIndex = 16;
            comboBoxCategory.SelectedIndexChanged += comboBoxCategory_SelectedIndexChanged;
            // 
            // buttonCreateIngredient
            // 
            buttonCreateIngredient.Location = new Point(549, 487);
            buttonCreateIngredient.Name = "buttonCreateIngredient";
            buttonCreateIngredient.Size = new Size(129, 23);
            buttonCreateIngredient.TabIndex = 15;
            buttonCreateIngredient.Text = "Zutat erstellen";
            buttonCreateIngredient.UseVisualStyleBackColor = true;
            buttonCreateIngredient.Click += buttonCreateIngredient_Click;
            // 
            // textBoxCreateIngredient
            // 
            textBoxCreateIngredient.BorderStyle = BorderStyle.FixedSingle;
            textBoxCreateIngredient.Location = new Point(413, 487);
            textBoxCreateIngredient.Name = "textBoxCreateIngredient";
            textBoxCreateIngredient.Size = new Size(121, 23);
            textBoxCreateIngredient.TabIndex = 14;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(286, 489);
            label9.Name = "label9";
            label9.Size = new Size(98, 15);
            label9.TabIndex = 13;
            label9.Text = "Zutaten erstellen:";
            // 
            // comboBoxIngredients
            // 
            comboBoxIngredients.FormattingEnabled = true;
            comboBoxIngredients.Location = new Point(413, 443);
            comboBoxIngredients.Name = "comboBoxIngredients";
            comboBoxIngredients.Size = new Size(121, 23);
            comboBoxIngredients.TabIndex = 12;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(286, 446);
            label8.Name = "label8";
            label8.Size = new Size(110, 15);
            label8.TabIndex = 11;
            label8.Text = "Zutaten auswählen:";
            // 
            // buttonCreateCategory
            // 
            buttonCreateCategory.Location = new Point(549, 322);
            buttonCreateCategory.Name = "buttonCreateCategory";
            buttonCreateCategory.Size = new Size(129, 23);
            buttonCreateCategory.TabIndex = 9;
            buttonCreateCategory.Text = "Kategorie erstellen";
            buttonCreateCategory.UseVisualStyleBackColor = true;
            buttonCreateCategory.Click += buttonCreateCategory_Click;
            // 
            // textBoxCreateCategory
            // 
            textBoxCreateCategory.BorderStyle = BorderStyle.FixedSingle;
            textBoxCreateCategory.Location = new Point(413, 322);
            textBoxCreateCategory.Name = "textBoxCreateCategory";
            textBoxCreateCategory.Size = new Size(121, 23);
            textBoxCreateCategory.TabIndex = 8;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(286, 323);
            label7.Name = "label7";
            label7.Size = new Size(107, 15);
            label7.TabIndex = 7;
            label7.Text = "Kategorie erstellen:";
            // 
            // buttonCreateRecipe
            // 
            buttonCreateRecipe.Location = new Point(20, 521);
            buttonCreateRecipe.Name = "buttonCreateRecipe";
            buttonCreateRecipe.Size = new Size(109, 23);
            buttonCreateRecipe.TabIndex = 6;
            buttonCreateRecipe.Text = "Rezept Erstellen";
            buttonCreateRecipe.UseVisualStyleBackColor = true;
            buttonCreateRecipe.Click += buttonCreateRecipe_Click;
            // 
            // textBoxDescription
            // 
            textBoxDescription.BorderStyle = BorderStyle.FixedSingle;
            textBoxDescription.Location = new Point(147, 59);
            textBoxDescription.Multiline = true;
            textBoxDescription.Name = "textBoxDescription";
            textBoxDescription.Size = new Size(662, 149);
            textBoxDescription.TabIndex = 5;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(20, 380);
            label6.Name = "label6";
            label6.Size = new Size(51, 15);
            label6.TabIndex = 4;
            label6.Text = "Zutaten:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(20, 226);
            label5.Name = "label5";
            label5.Size = new Size(60, 15);
            label5.TabIndex = 3;
            label5.Text = "Kategorie:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(20, 62);
            label4.Name = "label4";
            label4.Size = new Size(82, 15);
            label4.TabIndex = 2;
            label4.Text = "Beschreibung:";
            // 
            // textBoxRecipeName
            // 
            textBoxRecipeName.BorderStyle = BorderStyle.FixedSingle;
            textBoxRecipeName.Location = new Point(147, 15);
            textBoxRecipeName.Name = "textBoxRecipeName";
            textBoxRecipeName.Size = new Size(387, 23);
            textBoxRecipeName.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 18);
            label3.Name = "label3";
            label3.Size = new Size(45, 15);
            label3.TabIndex = 0;
            label3.Text = "Rezept:";
            // 
            // tabPageEdit
            // 
            tabPageEdit.Controls.Add(panel2);
            tabPageEdit.Location = new Point(4, 24);
            tabPageEdit.Name = "tabPageEdit";
            tabPageEdit.Size = new Size(842, 559);
            tabPageEdit.TabIndex = 4;
            tabPageEdit.Text = "Rezept bearbeiten";
            tabPageEdit.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.Controls.Add(buttonEditCancel);
            panel2.Controls.Add(label17);
            panel2.Controls.Add(cmbEditCategory);
            panel2.Controls.Add(buttonEditSave);
            panel2.Controls.Add(buttonEditAddCategory);
            panel2.Controls.Add(buttonEditAddIngredient);
            panel2.Controls.Add(flowLayoutPanelEditIngredients);
            panel2.Controls.Add(buttonCreateIngredientInEdit);
            panel2.Controls.Add(buttonCreateCategoryInEdit);
            panel2.Controls.Add(flowLayoutPanelEditCategories);
            panel2.Controls.Add(cmbEditIngredient);
            panel2.Controls.Add(textBoxEditName);
            panel2.Controls.Add(textBoxCreateIngredientInEdit);
            panel2.Controls.Add(textBoxCreateCategoryInEdit);
            panel2.Controls.Add(textBoxEditDescription);
            panel2.Controls.Add(label16);
            panel2.Controls.Add(label15);
            panel2.Controls.Add(label14);
            panel2.Controls.Add(label13);
            panel2.Controls.Add(label12);
            panel2.Controls.Add(label11);
            panel2.Controls.Add(label10);
            panel2.Location = new Point(3, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(839, 553);
            panel2.TabIndex = 0;
            // 
            // buttonEditCancel
            // 
            buttonEditCancel.Location = new Point(167, 517);
            buttonEditCancel.Name = "buttonEditCancel";
            buttonEditCancel.Size = new Size(142, 23);
            buttonEditCancel.TabIndex = 21;
            buttonEditCancel.Text = "abbrechen";
            buttonEditCancel.UseVisualStyleBackColor = true;
            buttonEditCancel.Click += buttonEditCancel_Click;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(319, 459);
            label17.Name = "label17";
            label17.Size = new Size(85, 15);
            label17.TabIndex = 20;
            label17.Text = "Zutat erstellen:";
            // 
            // cmbEditCategory
            // 
            cmbEditCategory.FormattingEnabled = true;
            cmbEditCategory.Location = new Point(410, 251);
            cmbEditCategory.Name = "cmbEditCategory";
            cmbEditCategory.Size = new Size(121, 23);
            cmbEditCategory.TabIndex = 19;
            // 
            // buttonEditSave
            // 
            buttonEditSave.Location = new Point(19, 517);
            buttonEditSave.Name = "buttonEditSave";
            buttonEditSave.Size = new Size(142, 23);
            buttonEditSave.TabIndex = 18;
            buttonEditSave.Text = "speichern";
            buttonEditSave.UseVisualStyleBackColor = true;
            buttonEditSave.Click += buttonEditSave_Click;
            // 
            // buttonEditAddCategory
            // 
            buttonEditAddCategory.Location = new Point(118, 251);
            buttonEditAddCategory.Name = "buttonEditAddCategory";
            buttonEditAddCategory.Size = new Size(142, 23);
            buttonEditAddCategory.TabIndex = 17;
            buttonEditAddCategory.Text = "Kategorie hinzufügen";
            buttonEditAddCategory.UseVisualStyleBackColor = true;
            buttonEditAddCategory.Click += buttonEditAddCategory_Click;
            // 
            // buttonEditAddIngredient
            // 
            buttonEditAddIngredient.Location = new Point(118, 428);
            buttonEditAddIngredient.Name = "buttonEditAddIngredient";
            buttonEditAddIngredient.Size = new Size(142, 23);
            buttonEditAddIngredient.TabIndex = 16;
            buttonEditAddIngredient.Text = "Zutat hinzufügen";
            buttonEditAddIngredient.UseVisualStyleBackColor = true;
            buttonEditAddIngredient.Click += buttonEditAddIngredient_Click;
            // 
            // flowLayoutPanelEditIngredients
            // 
            flowLayoutPanelEditIngredients.Location = new Point(119, 362);
            flowLayoutPanelEditIngredients.Name = "flowLayoutPanelEditIngredients";
            flowLayoutPanelEditIngredients.Size = new Size(689, 60);
            flowLayoutPanelEditIngredients.TabIndex = 14;
            // 
            // buttonCreateIngredientInEdit
            // 
            buttonCreateIngredientInEdit.Location = new Point(575, 459);
            buttonCreateIngredientInEdit.Name = "buttonCreateIngredientInEdit";
            buttonCreateIngredientInEdit.Size = new Size(142, 23);
            buttonCreateIngredientInEdit.TabIndex = 15;
            buttonCreateIngredientInEdit.Text = "Zutat erstellen";
            buttonCreateIngredientInEdit.UseVisualStyleBackColor = true;
            buttonCreateIngredientInEdit.Click += buttonCreateIngredientInEdit_Click;
            // 
            // buttonCreateCategoryInEdit
            // 
            buttonCreateCategoryInEdit.Location = new Point(575, 299);
            buttonCreateCategoryInEdit.Name = "buttonCreateCategoryInEdit";
            buttonCreateCategoryInEdit.Size = new Size(142, 23);
            buttonCreateCategoryInEdit.TabIndex = 14;
            buttonCreateCategoryInEdit.Text = "Kategorie erstellen";
            buttonCreateCategoryInEdit.UseVisualStyleBackColor = true;
            buttonCreateCategoryInEdit.Click += buttonCreateCategoryInEdit_Click;
            // 
            // flowLayoutPanelEditCategories
            // 
            flowLayoutPanelEditCategories.Location = new Point(119, 185);
            flowLayoutPanelEditCategories.Name = "flowLayoutPanelEditCategories";
            flowLayoutPanelEditCategories.Size = new Size(689, 60);
            flowLayoutPanelEditCategories.TabIndex = 13;
            // 
            // cmbEditIngredient
            // 
            cmbEditIngredient.FormattingEnabled = true;
            cmbEditIngredient.Location = new Point(410, 428);
            cmbEditIngredient.Name = "cmbEditIngredient";
            cmbEditIngredient.Size = new Size(121, 23);
            cmbEditIngredient.TabIndex = 11;
            // 
            // textBoxEditName
            // 
            textBoxEditName.BorderStyle = BorderStyle.FixedSingle;
            textBoxEditName.Location = new Point(119, 15);
            textBoxEditName.Name = "textBoxEditName";
            textBoxEditName.Size = new Size(412, 23);
            textBoxEditName.TabIndex = 10;
            // 
            // textBoxCreateIngredientInEdit
            // 
            textBoxCreateIngredientInEdit.BorderStyle = BorderStyle.FixedSingle;
            textBoxCreateIngredientInEdit.Location = new Point(410, 456);
            textBoxCreateIngredientInEdit.Name = "textBoxCreateIngredientInEdit";
            textBoxCreateIngredientInEdit.Size = new Size(121, 23);
            textBoxCreateIngredientInEdit.TabIndex = 9;
            // 
            // textBoxCreateCategoryInEdit
            // 
            textBoxCreateCategoryInEdit.BorderStyle = BorderStyle.FixedSingle;
            textBoxCreateCategoryInEdit.Location = new Point(410, 299);
            textBoxCreateCategoryInEdit.Name = "textBoxCreateCategoryInEdit";
            textBoxCreateCategoryInEdit.Size = new Size(121, 23);
            textBoxCreateCategoryInEdit.TabIndex = 8;
            // 
            // textBoxEditDescription
            // 
            textBoxEditDescription.Location = new Point(119, 56);
            textBoxEditDescription.Multiline = true;
            textBoxEditDescription.Name = "textBoxEditDescription";
            textBoxEditDescription.Size = new Size(689, 123);
            textBoxEditDescription.TabIndex = 7;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(307, 432);
            label16.Name = "label16";
            label16.Size = new Size(97, 15);
            label16.TabIndex = 6;
            label16.Text = "Zutat auswählen:";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(19, 362);
            label15.Name = "label15";
            label15.Size = new Size(51, 15);
            label15.TabIndex = 5;
            label15.Text = "Zutaten:";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(288, 254);
            label14.Name = "label14";
            label14.Size = new Size(116, 15);
            label14.TabIndex = 4;
            label14.Text = "Kategorie auswählen";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(297, 303);
            label13.Name = "label13";
            label13.Size = new Size(107, 15);
            label13.TabIndex = 3;
            label13.Text = "Kategorie erstellen:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(20, 185);
            label12.Name = "label12";
            label12.Size = new Size(57, 15);
            label12.TabIndex = 2;
            label12.Text = "Kategorie";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(20, 59);
            label11.Name = "label11";
            label11.Size = new Size(82, 15);
            label11.TabIndex = 1;
            label11.Text = "Beschreibung:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(20, 20);
            label10.Name = "label10";
            label10.Size = new Size(45, 15);
            label10.TabIndex = 0;
            label10.Text = "Rezept:";
            // 
            // groupBox2
            // 
            groupBox2.BackColor = Color.WhiteSmoke;
            groupBox2.Controls.Add(buttonLogin);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(textBoxPassword);
            groupBox2.Controls.Add(textBoxLoginName);
            groupBox2.Controls.Add(buttonLogout);
            groupBox2.Location = new Point(12, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(247, 128);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "Anmeldung";
            // 
            // buttonLogin
            // 
            buttonLogin.Location = new Point(11, 89);
            buttonLogin.Name = "buttonLogin";
            buttonLogin.Size = new Size(108, 23);
            buttonLogin.TabIndex = 21;
            buttonLogin.Text = "Einloggen";
            buttonLogin.UseVisualStyleBackColor = true;
            buttonLogin.Click += buttonLogin_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 53);
            label2.Name = "label2";
            label2.Size = new Size(54, 15);
            label2.TabIndex = 20;
            label2.Text = "Passwort";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 24);
            label1.Name = "label1";
            label1.Size = new Size(83, 15);
            label1.TabIndex = 19;
            label1.Text = "Benutzername";
            // 
            // textBoxPassword
            // 
            textBoxPassword.BorderStyle = BorderStyle.FixedSingle;
            textBoxPassword.Location = new Point(103, 50);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.Size = new Size(138, 23);
            textBoxPassword.TabIndex = 18;
            // 
            // textBoxLoginName
            // 
            textBoxLoginName.BorderStyle = BorderStyle.FixedSingle;
            textBoxLoginName.Location = new Point(103, 21);
            textBoxLoginName.Name = "textBoxLoginName";
            textBoxLoginName.Size = new Size(138, 23);
            textBoxLoginName.TabIndex = 17;
            // 
            // buttonLogout
            // 
            buttonLogout.Location = new Point(128, 89);
            buttonLogout.Name = "buttonLogout";
            buttonLogout.Size = new Size(108, 23);
            buttonLogout.TabIndex = 16;
            buttonLogout.Text = "Ausloggen";
            buttonLogout.UseVisualStyleBackColor = true;
            buttonLogout.Click += buttonLogout_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(comboBoxFilterKategorie);
            groupBox1.Controls.Add(checkBoxFilterKategorie);
            groupBox1.Location = new Point(12, 146);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(247, 96);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Kategorie - Filter";
            // 
            // comboBoxFilterKategorie
            // 
            comboBoxFilterKategorie.FormattingEnabled = true;
            comboBoxFilterKategorie.Location = new Point(13, 51);
            comboBoxFilterKategorie.Name = "comboBoxFilterKategorie";
            comboBoxFilterKategorie.Size = new Size(223, 23);
            comboBoxFilterKategorie.TabIndex = 1;
            comboBoxFilterKategorie.SelectedIndexChanged += comboBoxFilterKategorie_SelectedIndexChanged;
            // 
            // checkBoxFilterKategorie
            // 
            checkBoxFilterKategorie.AutoSize = true;
            checkBoxFilterKategorie.Location = new Point(13, 26);
            checkBoxFilterKategorie.Name = "checkBoxFilterKategorie";
            checkBoxFilterKategorie.Size = new Size(76, 19);
            checkBoxFilterKategorie.TabIndex = 0;
            checkBoxFilterKategorie.Text = "Kategorie";
            checkBoxFilterKategorie.UseVisualStyleBackColor = true;
            checkBoxFilterKategorie.CheckedChanged += checkBoxFilterKategorie_CheckedChanged;
            // 
            // groupBoxEditCategorie
            // 
            groupBoxEditCategorie.Controls.Add(label20);
            groupBoxEditCategorie.Controls.Add(label19);
            groupBoxEditCategorie.Controls.Add(buttonSaveEditCategory);
            groupBoxEditCategorie.Controls.Add(textBoxEditCategorie);
            groupBoxEditCategorie.Controls.Add(comboBoxEditCategory);
            groupBoxEditCategorie.Location = new Point(12, 356);
            groupBoxEditCategorie.Name = "groupBoxEditCategorie";
            groupBoxEditCategorie.Size = new Size(247, 191);
            groupBoxEditCategorie.TabIndex = 4;
            groupBoxEditCategorie.TabStop = false;
            groupBoxEditCategorie.Text = "Kategorie bearbeiten";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(14, 97);
            label20.Name = "label20";
            label20.Size = new Size(162, 15);
            label20.TabIndex = 4;
            label20.Text = "Neue Kategorie Bezeichnung:";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(14, 33);
            label19.Name = "label19";
            label19.Size = new Size(119, 15);
            label19.TabIndex = 3;
            label19.Text = "Kategorie auswählen:";
            // 
            // buttonSaveEditCategory
            // 
            buttonSaveEditCategory.Location = new Point(11, 157);
            buttonSaveEditCategory.Name = "buttonSaveEditCategory";
            buttonSaveEditCategory.Size = new Size(75, 23);
            buttonSaveEditCategory.TabIndex = 2;
            buttonSaveEditCategory.Text = "speichern";
            buttonSaveEditCategory.UseVisualStyleBackColor = true;
            buttonSaveEditCategory.Click += buttonSaveEditCategory_Click;
            // 
            // textBoxEditCategorie
            // 
            textBoxEditCategorie.BorderStyle = BorderStyle.FixedSingle;
            textBoxEditCategorie.Location = new Point(11, 115);
            textBoxEditCategorie.Name = "textBoxEditCategorie";
            textBoxEditCategorie.Size = new Size(223, 23);
            textBoxEditCategorie.TabIndex = 1;
            // 
            // comboBoxEditCategory
            // 
            comboBoxEditCategory.FormattingEnabled = true;
            comboBoxEditCategory.Location = new Point(11, 51);
            comboBoxEditCategory.Name = "comboBoxEditCategory";
            comboBoxEditCategory.Size = new Size(223, 23);
            comboBoxEditCategory.TabIndex = 0;
            comboBoxEditCategory.SelectedIndexChanged += comboBoxEditCategory_SelectedIndexChanged;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(comboBoxFilterIngredient);
            groupBox3.Controls.Add(checkBoxFilterIngredient);
            groupBox3.Location = new Point(12, 248);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(247, 100);
            groupBox3.TabIndex = 5;
            groupBox3.TabStop = false;
            groupBox3.Text = "Zutaten Filter";
            // 
            // comboBoxFilterIngredient
            // 
            comboBoxFilterIngredient.FormattingEnabled = true;
            comboBoxFilterIngredient.Location = new Point(14, 59);
            comboBoxFilterIngredient.Name = "comboBoxFilterIngredient";
            comboBoxFilterIngredient.Size = new Size(220, 23);
            comboBoxFilterIngredient.TabIndex = 1;
            comboBoxFilterIngredient.SelectedIndexChanged += comboBoxFilterIngredient_SelectedIndexChanged;
            // 
            // checkBoxFilterIngredient
            // 
            checkBoxFilterIngredient.AutoSize = true;
            checkBoxFilterIngredient.Location = new Point(11, 22);
            checkBoxFilterIngredient.Name = "checkBoxFilterIngredient";
            checkBoxFilterIngredient.Size = new Size(54, 19);
            checkBoxFilterIngredient.TabIndex = 0;
            checkBoxFilterIngredient.Text = "Zutat";
            checkBoxFilterIngredient.UseVisualStyleBackColor = true;
            checkBoxFilterIngredient.CheckedChanged += checkBoxFilterIngredient_CheckedChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1139, 617);
            Controls.Add(groupBox3);
            Controls.Add(groupBoxEditCategorie);
            Controls.Add(groupBox1);
            Controls.Add(groupBox2);
            Controls.Add(panel3);
            Name = "MainForm";
            Text = "Form1";
            panel3.ResumeLayout(false);
            tabControl.ResumeLayout(false);
            tabPageAllRecipe.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridAllRecipes).EndInit();
            tabPageOwnRecipes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMyRecipes).EndInit();
            tabPageFavorites.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvMyFavorites).EndInit();
            tabPageAddRecipe.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tabPageEdit.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBoxEditCategorie.ResumeLayout(false);
            groupBoxEditCategorie.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel3;
        private GroupBox groupBox2;
        private Button buttonLogin;
        private Label label2;
        private Label label1;
        private TextBox textBoxPassword;
        private TextBox textBoxLoginName;
        private Button buttonLogout;
        private TabControl tabControl;
        private TabPage tabPageAllRecipe;
        private TabPage tabPageOwnRecipes;
        private TabPage tabPageAddRecipe;
        private DataGridView dataGridAllRecipes;
        private DataGridView dgvMyRecipes;
        private TabPage tabPageFavorites;
        private DataGridView dgvMyFavorites;
        private Panel panel1;
        private TextBox textBoxRecipeName;
        private Label label3;
        private Button buttonCreateRecipe;
        private TextBox textBoxDescription;
        private Label label6;
        private Label label5;
        private Label label4;
        private Button buttonCreateCategory;
        private TextBox textBoxCreateCategory;
        private Label label7;
        private Button buttonCreateIngredient;
        private TextBox textBoxCreateIngredient;
        private Label label9;
        private ComboBox comboBoxIngredients;
        private Label label8;
        private ComboBox comboBoxCategory;
        private Button buttonAddIngredient;
        private FlowLayoutPanel flowSelectedIngredients;
        private TabPage tabPageEdit;
        private Panel panel2;
        private Label label16;
        private Label label15;
        private Label label14;
        private Label label13;
        private Label label12;
        private Label label11;
        private Label label10;
        private FlowLayoutPanel flowLayoutPanelEditIngredients;
        private Button buttonCreateIngredientInEdit;
        private Button buttonCreateCategoryInEdit;
        private FlowLayoutPanel flowLayoutPanelEditCategories;
        private ComboBox cmbEditIngredient;
        private TextBox textBoxEditName;
        private TextBox textBoxCreateIngredientInEdit;
        private TextBox textBoxCreateCategoryInEdit;
        private TextBox textBoxEditDescription;
        private Button buttonEditSave;
        private Button buttonEditAddCategory;
        private Button buttonEditAddIngredient;
        private ComboBox cmbEditCategory;
        private Label label17;
        private Button buttonEditCancel;
        private Button buttonAddCategory;
        private FlowLayoutPanel flowLayoutPanelKategory;
        private Label label18;
        private GroupBox groupBox1;
        private CheckBox checkBoxFilterKategorie;
        private ComboBox comboBoxFilterKategorie;
        private GroupBox groupBoxEditCategorie;
        private Label label20;
        private Label label19;
        private Button buttonSaveEditCategory;
        private TextBox textBoxEditCategorie;
        private ComboBox comboBoxEditCategory;
        private GroupBox groupBox3;
        private ComboBox comboBoxFilterIngredient;
        private CheckBox checkBoxFilterIngredient;
    }
}
