using Microsoft.EntityFrameworkCore;
using RecipeHub.Library.Data;
using RecipeHub.Library.Models;
using RecipeHub.Library.Repositories;
using RecipeHub.Library.Services;
using System.Windows.Forms;

namespace RecipeHub.WinForms
{
    public partial class MainForm : Form
    {
        #region Felder und Services

        //  DB,Services,aktueller User 
        private readonly RecipeHubDbContext _db = new();
        private readonly UserService _userService;
        private readonly IngredientService _ingredientService;
        private readonly CategoryService _categoryService;
        private readonly RecipeService _recipeService;
        private readonly FavoriteService _favoriteService;

        // Auswahlstatus für Rezept anlegen
        private readonly List<Ingredient> _selectedIngredients = new();
        private readonly List<Category> _selectedCategories = new();

        //  Ausgewählte Kategorie für Rezept anlegen
        private Category? _selectedCategory;

        // Aktueller eingeloggter User
        private User? _currentUser;

        // Favoriten-Event-Suppressor
        private bool _suppressFavEvents = false;

        // Bearbeitungsmodus für eigene Rezepte
        private bool _editMode = false;
        private int _editRecipeId = 0;

        // Für veränderte Zutaten/Kategorien im Edit-Modus
        private readonly HashSet<int> _editIngredientIds = new();
        private readonly HashSet<int> _editCategoryIds = new();

        #endregion

        #region Konstruktor & Initialisierung
        public MainForm()
        {
            InitializeComponent();

            // Services instanzieren
            _userService = new UserService(new Repository<User>(_db));
            _ingredientService = new IngredientService(new Repository<Ingredient>(_db));
            _categoryService = new CategoryService(new Repository<Category>(_db), _db);
            _recipeService = new RecipeService(new Repository<Recipe>(_db), _userService, _db);
            _favoriteService = new FavoriteService(_db);

            // Load-Event abonnieren (DB migrieren, UI-Status setzen)
            this.Load += Form1_Load;

            // Logout-Button initial deaktivieren
            buttonLogout.Enabled = false;
            buttonLogout.BackColor = Color.LightGray;
            textBoxPassword.UseSystemPasswordChar = true;

            // Fenster-Titel
            this.Text = "RecipeHub";

            // Buttons initial deaktivieren/aktivieren
            UpdateUiState();

            //Konfigurieren der DataGrids
            ConfigureAllRecipesGrid();
            ConfigureMyRecipesGrid();
            ConfigureFavoritesGrid();

            // Enable Edit-on-Enter für alle Grids
            dataGridAllRecipes.EditMode = DataGridViewEditMode.EditOnEnter;

            //TabPageEdit initial ausblenden
            tabPageEdit.Parent = null;

            // TabControl: Rezept Edit-Tab nur über doppelten Klick auf eigenes Rezept betretbar 
            tabControl.Selecting += TabControl_Selecting_BlockEdit;

            // Passwort-Eingabe verbergen
            textBoxPassword.UseSystemPasswordChar = true;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // Ressourcen freigeben
            _db?.Dispose();
            base.OnFormClosed(e);
        }

        private async void Form1_Load(object? sender, EventArgs e)
        {
            try
            {
                await _db.Database.MigrateAsync();

                // Laden der Zutatenliste aus der Textdatei in die Datenbank
                await _ingredientService.LoadIngredientsToDatabaseAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DB-Initialisierung fehlgeschlagen:\n{ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                // Grid vorbereiten und alle Rezepte laden 
                await LoadAllRecipesAsync();

                // Comboboxen befüllen
                await LoadLookupsAsync();
                await LoadFilterCategoryLookupsAsync();
                await LoadFilterIngredientLookupAsync();
                await LoadEditCategoryLookupAsync();

                UpdateUiState();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Initiales Laden fehlgeschlagen:\n{ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateUiState()
        {
            bool isLoggedIn = _currentUser != null;

            // Login-Controls
            textBoxLoginName.ReadOnly = isLoggedIn;
            textBoxPassword.ReadOnly = isLoggedIn;
            textBoxLoginName.Enabled = !isLoggedIn;
            textBoxPassword.Enabled = !isLoggedIn;
            buttonLogin.Enabled = !isLoggedIn;

            // Kategorie groupbox
            groupBoxEditCategorie.Visible = isLoggedIn;

            // Tabs für private Bereiche sperren/freigeben
            tabPageFavorites.Enabled = isLoggedIn;
            tabPageOwnRecipes.Enabled = isLoggedIn;
            tabPageAddRecipe.Enabled = isLoggedIn;

            // zurück auf „Alle Rezepte“ springen, wenn ausgeloggt
            if (!isLoggedIn)
            {
                tabControl.SelectedTab = tabPageAllRecipe;
            }

            // Logout-Button (aktiv nur wenn eingeloggt)
            buttonLogout.Enabled = isLoggedIn;
            buttonLogout.BackColor = isLoggedIn ? SystemColors.Control : Color.LightGray;
        }
        private void ConfigureAllRecipesGrid()
        {
            dataGridAllRecipes.AutoGenerateColumns = false;
            dataGridAllRecipes.AllowUserToAddRows = false;
            dataGridAllRecipes.AllowUserToDeleteRows = false;
            dataGridAllRecipes.MultiSelect = false;
            dataGridAllRecipes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridAllRecipes.RowHeadersVisible = false;
            dataGridAllRecipes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridAllRecipes.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridAllRecipes.ReadOnly = false;
            dataGridAllRecipes.Columns.Clear();

            // Versteckte Spalten
            dataGridAllRecipes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colId",
                DataPropertyName = "Id",
                Visible = false
            });

            dataGridAllRecipes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colOwnerId",
                DataPropertyName = "UserId",
                Visible = false
            });

            // Titel
            var colTitle = new DataGridViewTextBoxColumn
            {
                Name = "colTitle",
                DataPropertyName = "Name",
                HeaderText = "Titel",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells,
                MinimumWidth = 120,
                DefaultCellStyle = { Font = new Font(Font, FontStyle.Bold) }
            };

            // Zutaten
            var colIngs = new DataGridViewTextBoxColumn
            {
                Name = "colIngredients",
                DataPropertyName = "Ingredients",
                HeaderText = "Zutaten",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 35,
                DefaultCellStyle = { WrapMode = DataGridViewTriState.True }
            };

            // Beschreibung
            var colDesc = new DataGridViewTextBoxColumn
            {
                Name = "colDesc",
                DataPropertyName = "Description",
                HeaderText = "Beschreibung",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 65,
                DefaultCellStyle = { WrapMode = DataGridViewTriState.True }
            };

            // Favoriten-Checkbox
            var colFav = new DataGridViewCheckBoxColumn
            {
                Name = "colFavCheck",
                HeaderText = "★",
                DataPropertyName = "IsFavorite",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = 36,
                ThreeState = false
            };

            var colCategories = new DataGridViewTextBoxColumn
            {
                Name = "colCategories",
                DataPropertyName = "Categories",
                HeaderText = "Kategorien",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 25,
                DefaultCellStyle = { WrapMode = DataGridViewTriState.True }
            };

            dataGridAllRecipes.Columns.AddRange(colTitle, colDesc, colCategories, colIngs, colFav);

            // Wenn der Nutzer eine Checkbox anklickt, ist die Zelle zunächst "dirty" (noch nicht übernommen).
            // Mit CommitEdit, EndEdit wird der neue Wert sofort commited,
            // damit CellValueChanged direkt feuert und sofort auf den Klick reagiert wird
            dataGridAllRecipes.CurrentCellDirtyStateChanged += (s, e) =>
            {
                // Nur bei Checkboxen
                if (dataGridAllRecipes.IsCurrentCellDirty &&
                    dataGridAllRecipes.CurrentCell is DataGridViewCheckBoxCell)
                {
                    dataGridAllRecipes.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    dataGridAllRecipes.EndEdit();
                }
            };
        }
        private void ConfigureMyRecipesGrid()
        {
            dgvMyRecipes.AutoGenerateColumns = false;
            dgvMyRecipes.ReadOnly = false;
            dgvMyRecipes.AllowUserToAddRows = false;
            dgvMyRecipes.AllowUserToDeleteRows = false;
            dgvMyRecipes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMyRecipes.RowHeadersVisible = false;
            dgvMyRecipes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMyRecipes.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvMyRecipes.Columns.Clear();

            dgvMyRecipes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colId",
                DataPropertyName = "Id",
                Visible = false
            });

            dgvMyRecipes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colName",
                DataPropertyName = "Name",
                HeaderText = "Titel",
                MinimumWidth = 150,
                DefaultCellStyle = { Font = new Font(Font, FontStyle.Bold) }
            });

            dgvMyRecipes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colIngredients",
                DataPropertyName = "Ingredients",
                HeaderText = "Zutaten",
                ReadOnly = true,
                DefaultCellStyle = { WrapMode = DataGridViewTriState.True }
            });

            dgvMyRecipes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDesc",
                DataPropertyName = "Description",
                HeaderText = "Beschreibung",
                DefaultCellStyle = { WrapMode = DataGridViewTriState.True }
            });

            dgvMyRecipes.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "colDelete",
                HeaderText = "",
                Text = "🗑",
                UseColumnTextForButtonValue = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = 44
            });
        }
        private void ConfigureFavoritesGrid()
        {
            dgvMyFavorites.AutoGenerateColumns = false;
            dgvMyFavorites.ReadOnly = true;
            dgvMyFavorites.AllowUserToAddRows = false;
            dgvMyFavorites.AllowUserToDeleteRows = false;
            dgvMyFavorites.MultiSelect = false;
            dgvMyFavorites.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMyFavorites.RowHeadersVisible = false;
            dgvMyFavorites.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMyFavorites.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvMyFavorites.Columns.Clear();

            dgvMyFavorites.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "favId",
                DataPropertyName = "Id",
                Visible = false
            });

            dgvMyFavorites.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "favName",
                DataPropertyName = "Name",
                HeaderText = "Titel",
                MinimumWidth = 150,
                DefaultCellStyle = { Font = new Font(Font, FontStyle.Bold) }
            });

            dgvMyFavorites.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "favIngredients",
                DataPropertyName = "Ingredients",
                HeaderText = "Zutaten",
                DefaultCellStyle = { WrapMode = DataGridViewTriState.True }
            });

            dgvMyFavorites.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "favOwner",
                DataPropertyName = "Owner",
                HeaderText = "Besitzer"
            });
        }
        // Laden der Lookup-Daten für Comboboxen für Rezeptanlegen
        private async Task LoadLookupsAsync()
        {
            // Zutaten
            var ings = await _ingredientService.GetAllAsync();
            comboBoxIngredients.DataSource = ings;
            comboBoxIngredients.DisplayMember = "Name";
            comboBoxIngredients.ValueMember = "Id";
            comboBoxIngredients.SelectedIndex = -1;

            // Kategorien
            var cats = await _categoryService.GetAllAsync();
            comboBoxCategory.DataSource = cats;
            comboBoxCategory.DisplayMember = "Name";
            comboBoxCategory.ValueMember = "Id";
            comboBoxCategory.SelectedIndex = -1;
        }
        // Laden der Lookup-Daten für Comboboxen im Edit-Tab
        private async Task LoadEditLookupsAsync()
        {
            var ings = await _ingredientService.GetAllAsync();
            cmbEditIngredient.DataSource = ings;
            cmbEditIngredient.DisplayMember = "Name";
            cmbEditIngredient.ValueMember = "Id";
            cmbEditIngredient.SelectedIndex = -1;

            var cats = await _categoryService.GetAllAsync();
            cmbEditCategory.DataSource = cats;
            cmbEditCategory.DisplayMember = "Name";
            cmbEditCategory.ValueMember = "Id";
            cmbEditCategory.SelectedIndex = -1;
        }
        // Laden der Lookup-Daten für Filter-Comboboxen
        private async Task LoadFilterCategoryLookupsAsync()
        {
            var cats = await _categoryService.GetAllAsync();
            comboBoxFilterKategorie.DataSource = null;
            comboBoxFilterKategorie.DataSource = cats;
            comboBoxFilterKategorie.DisplayMember = "Name";
            comboBoxFilterKategorie.ValueMember = "Id";
            comboBoxFilterKategorie.SelectedIndex = -1; // nichts vorauswählen
        }
        // Laden der Lookup-Daten für Filter-Comboboxen
        private async Task LoadFilterIngredientLookupAsync()
        {
            var ings = await _ingredientService.GetAllAsync();
            comboBoxFilterIngredient.DataSource = null;
            comboBoxFilterIngredient.DataSource = ings;
            comboBoxFilterIngredient.DisplayMember = "Name";
            comboBoxFilterIngredient.ValueMember = "Id";
            comboBoxFilterIngredient.SelectedIndex = -1;
        }
        // Laden der Lookup-Daten für Edit-Comboboxen
        private async Task LoadEditCategoryLookupAsync(int? selectId = null)
        {
            var cats = await _categoryService.GetAllAsync();
            comboBoxEditCategory.DataSource = null;
            comboBoxEditCategory.DataSource = cats;
            comboBoxEditCategory.DisplayMember = "Name";
            comboBoxEditCategory.ValueMember = "Id";
            comboBoxEditCategory.SelectedIndex = -1;
            if (selectId.HasValue) comboBoxEditCategory.SelectedValue = selectId.Value;
        }
        // Laden der Lookup-Daten für Edit-Comboboxen
        private async Task LoadEditIngredientsLookupAsync(int? selectId = null)
        {
            var ings = await _ingredientService.GetAllAsync();
            cmbEditIngredient.DataSource = null;
            cmbEditIngredient.DataSource = ings;
            cmbEditIngredient.DisplayMember = "Name";
            cmbEditIngredient.ValueMember = "Id";
            if (selectId.HasValue)
            {
                cmbEditIngredient.SelectedValue = selectId.Value;
            }
            else
            {
                cmbEditIngredient.SelectedIndex = -1;
            }
        }
        // Laden der Lookup-Daten für Edit-Comboboxen
        private async Task LoadEditCategoriesLookupAsync(int? selectId = null)
        {
            var cats = await _categoryService.GetAllAsync();
            cmbEditCategory.DataSource = null;
            cmbEditCategory.DataSource = cats;
            cmbEditCategory.DisplayMember = "Name";
            cmbEditCategory.ValueMember = "Id";

            if (selectId.HasValue)
            {
                cmbEditCategory.SelectedValue = selectId.Value;
            }
            else
            {
                cmbEditCategory.SelectedIndex = -1;
            }
        }
        // Laden der Lookup-Daten für Comboboxen für Rezeptanlegen

        /*private async Task LoadCategoriesAsync(int? selectId = null)
        {
            var cats = await _categoryService.GetAllAsync();
            comboBoxCategory.DataSource = null;
            comboBoxCategory.DataSource = cats;
            comboBoxCategory.DisplayMember = "Name";
            comboBoxCategory.ValueMember = "Id";
            comboBoxCategory.SelectedIndex = -1;

            if (selectId.HasValue)
                comboBoxCategory.SelectedValue = selectId.Value;
        }*/
        #endregion

        #region Login / Logout
        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            var username = (textBoxLoginName.Text ?? "").Trim();
            var password = (textBoxPassword.Text ?? "").Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Bitte Benutzername und Passwort eingeben.", "Hinweis",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var result = await _userService.AuthenticateAsync(username, password);

                if (result == LoginResult.Success)
                {
                    var user = (await _userService.GetAllAsync())
                        .First(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
                    _currentUser = user;
                    UpdateUiState();

                    // Laden der DataGrids des Users
                    await LoadAllRecipesAsync();
                    await LoadMyFavoritesAsync();
                    await LoadMyRecipesAsync();

                    MessageBox.Show($"Willkommen, {user.Username}!", "Login",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (result == LoginResult.WrongPassword)
                {
                    MessageBox.Show("Passwort ist falsch.", "Login fehlgeschlagen",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // UserNotFound → fragen, ob registriert werden soll
                var ask = MessageBox.Show(
                    $"Benutzer '{username}' existiert nicht.\nJetzt registrieren?",
                    "Registrieren?",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (ask == DialogResult.Yes)
                {
                    var newId = await _userService.RegisterAsync(username, password);
                    var newUser = (await _userService.GetAllAsync()).First(u => u.Id == newId);
                    _currentUser = newUser;
                    UpdateUiState();
                    MessageBox.Show($"Benutzer '{username}' wurde registriert und angemeldet.", "Erfolg",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login/Registrierung fehlgeschlagen:\n{ex.Message}", "Fehler",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            _currentUser = null;
            textBoxPassword.Clear();
            UpdateUiState();
            MessageBox.Show("Abgemeldet.", "Logout",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region Tab-Steuerung & Navigation
        private async void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tabControl.SelectedTab == tabPageAllRecipe)
                {
                    await LoadAllRecipesAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Laden fehlgeschlagen:\n" + ex.Message);
            }
        }

        private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (_currentUser != null) return;

            if (e.TabPage == tabPageAddRecipe ||
                e.TabPage == tabPageFavorites ||
                e.TabPage == tabPageOwnRecipes)
            {
                e.Cancel = true;

                MessageBox.Show("Bitte zuerst einloggen.");
            }
        }

        private void TabControl_Selecting_BlockEdit(object? sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == tabPageEdit && !_editMode) e.Cancel = true;
        }
        #endregion

        #region Rezeptverwaltung
        private async void buttonCreateRecipe_Click(object sender, EventArgs e)
        {
            if (_currentUser == null) { MessageBox.Show("Bitte zuerst einloggen."); return; }

            var name = (textBoxRecipeName.Text ?? "").Trim();
            var desc = (textBoxDescription.Text ?? "").Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Bitte Rezeptname eingeben.");
                return;
            }
            if (string.IsNullOrWhiteSpace(desc))
            {
                MessageBox.Show("Bitte Beschreibung eingeben.");
                return;
            }
            if (_selectedCategories.Count == 0)
            {
                MessageBox.Show("Bitte Kategorie wählen.");
                return;
            }
            if (_selectedIngredients.Count == 0)
            {
                MessageBox.Show("Mindestens eine Zutat auswählen.");
                return;
            }

            try
            {
                var recipe = new Recipe
                {
                    Name = name,
                    Description = desc,
                    UserId = _currentUser.Id,
                    Categories = new List<Category> { _selectedCategory },
                    Ingredients = new List<Ingredient>(_selectedIngredients)
                };

                await _recipeService.AddRecipeAsync(recipe);
                MessageBox.Show($"Rezept '{name}' wurde angelegt.");

                // Reset
                textBoxRecipeName.Clear();
                textBoxDescription.Clear();
                comboBoxIngredients.SelectedIndex = -1;
                comboBoxCategory.SelectedIndex = -1;
                ClearChips();

                await LoadAllRecipesAsync();
                await LoadMyRecipesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Rezept konnte nicht erstellt werden:\n{ex.Message}");
            }
        }

        private async Task LoadMyRecipesAsync()
        {
            if (_currentUser == null)
            {
                dgvMyRecipes.DataSource = null;
                return;
            }

            var view = await _db.Recipes
                .AsNoTracking()
                .Where(r => r.UserId == _currentUser.Id)
                .Include(r => r.Ingredients)
                .Select(r => new
                {
                    r.Id,
                    r.Name,
                    Ingredients = string.Join(", ", r.Ingredients.Select(i => i.Name)),
                    r.Description
                })
                .ToListAsync();

            dgvMyRecipes.DataSource = null;
            dgvMyRecipes.DataSource = view;
        }

        private async Task LoadMyFavoritesAsync()
        {
            if (_currentUser == null) { dgvMyFavorites.DataSource = null; return; }

            var view = await _db.Users
                .AsNoTracking()
                .Where(u => u.Id == _currentUser.Id)
                .SelectMany(u => u.FavoriteRecipes)
                .Include(r => r.User)         // Besitzer in einem JOIN laden
                .Include(r => r.Ingredients)  // Zutaten gleich mitladen
                .Select(r => new
                {
                    r.Id,
                    r.Name,
                    Owner = r.User.Username,
                    Ingredients = string.Join(", ", r.Ingredients.Select(i => i.Name))
                })
                .OrderBy(x => x.Name)
                .ToListAsync();

            dgvMyFavorites.DataSource = null;
            dgvMyFavorites.DataSource = view;
        }

        private async Task LoadAllRecipesAsync()
        {
            // Favoriten-IDs (leer wenn nicht eingeloggt)
            var favIds = _currentUser == null
            ? new HashSet<int>()
            : (await _db.Users
                    .Where(u => u.Id == _currentUser.Id)
                    .SelectMany(u => u.FavoriteRecipes.Select(r => r.Id))
                    .ToListAsync()).ToHashSet();

            // Basisabfrage
            var query = _db.Recipes
                .AsNoTracking()
                .Include(r => r.Ingredients)
                .Include(r => r.Categories)
                .AsQueryable();

            // Kategorie-Filter anwenden, wenn Checkbox aktiv und eine Kategorie gewählt
            if (checkBoxFilterKategorie.Checked && comboBoxFilterKategorie.SelectedItem is Category selCat)
            {
                int catId = selCat.Id;
                query = query.Where(r => r.Categories.Any(c => c.Id == catId));
            }

            // Zutaten-Filter anwenden, wenn Checkbox aktiv und eine Zutat gewählt
            if (checkBoxFilterIngredient.Checked && comboBoxFilterIngredient.SelectedItem is Ingredient selIng)
            {
                int ingId = selIng.Id;
                query = query.Where(r => r.Ingredients.Any(i => i.Id == ingId));
            }

            // 🔹 Hier die Kategorie-Spalte hinzufügen
            var view = await query
                .Select(r => new
                {
                    r.Id,
                    r.UserId,
                    r.Name,
                    Categories = string.Join(", ", r.Categories.Select(c => c.Name)),
                    Ingredients = string.Join(", ", r.Ingredients.Select(i => i.Name)),
                    r.Description,
                    IsFavorite = favIds.Contains(r.Id)
                })
                .ToListAsync();

            // Grid befüllen
            dataGridAllRecipes.DataSource = null;
            dataGridAllRecipes.DataSource = view;

            // Checkbox-Zellen (de)aktivieren: nur fremde Rezepte und wenn eingeloggt
            foreach (DataGridViewRow row in dataGridAllRecipes.Rows)
            {
                var ownerCell = row.Cells["colOwnerId"]?.Value;
                if (ownerCell is null) continue;

                int ownerId = Convert.ToInt32(ownerCell);
                bool canEdit = _currentUser != null && ownerId != _currentUser!.Id;
                row.Cells["colFavCheck"].ReadOnly = !canEdit;
            }
        }

        private void buttonRemoveIngredient_Click(object sender, EventArgs e)
        {
            var chip = flowSelectedIngredients.Controls.Cast<Control>().LastOrDefault();
            if (chip is null) return;

            if (chip.Tag is int id)
                RemoveChip(id, chip);
        }

        #endregion

        #region Favoriten
        private async void dataGridAllRecipes_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Favoriten-Checkbox geändert
            if (_suppressFavEvents)
            {
                return;
            }

            // Gültigkeitsprüfungen
            if (e.RowIndex < 0)
            {
                return;
            }

            // Nur klicken auf Favoriten-Spalte behandeln
            if (dataGridAllRecipes.Columns[e.ColumnIndex].Name != "colFavCheck")
            {
                return;
            }

            var row = dataGridAllRecipes.Rows[e.RowIndex];

            // Hidden-Spalten verwenden
            int recipeId = Convert.ToInt32(row.Cells["colId"].Value);
            int ownerId = Convert.ToInt32(row.Cells["colOwnerId"].Value);

            // Neuer Zustand der Checkbox Spalte:
            var chkCell = (DataGridViewCheckBoxCell)row.Cells["colFavCheck"];

            // Aktuellen Wert ermitteln (EditedFormattedValue hat Vorrang vor Value)
            bool isChecked = Convert.ToBoolean(chkCell.EditedFormattedValue ?? chkCell.Value ?? false);

            // Regeln: Login + nicht eigenes Rezept
            if (_currentUser == null || ownerId == _currentUser.Id)
            {
                _suppressFavEvents = true;
                chkCell.Value = !isChecked; // zurückdrehen
                _suppressFavEvents = false;

                MessageBox.Show(_currentUser == null
                    ? "Bitte zuerst einloggen."
                    : "Eigene Rezepte können nicht favorisiert werden.");
                return;
            }

            try
            {
                if (isChecked)
                {
                    await _favoriteService.AddFavoriteAsync(_currentUser.Id, recipeId);
                }
                else
                {
                    await _favoriteService.RemoveFavoriteAsync(_currentUser.Id, recipeId);
                }
            }
            catch (Exception ex)
            {
                // Rollback bei Fehler
                _suppressFavEvents = true;
                chkCell.Value = !isChecked;
                _suppressFavEvents = false;
                MessageBox.Show("Favorit-Änderung fehlgeschlagen:\n" + ex.Message);
                return;
            }

            // UI aktualisieren (beide Grids), ohne Event-Schleife
            _suppressFavEvents = true;
            await LoadAllRecipesAsync();
            await LoadMyFavoritesAsync();
            _suppressFavEvents = false;
        }

        #endregion

        #region Zutatenverwaltung
        private async void buttonCreateIngredient_Click(object sender, EventArgs e)
        {
            var name = (textBoxCreateIngredient.Text ?? "").Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Bitte Zutatenname eingeben."); return;
            }

            try
            {
                await _ingredientService.GetOrCreateAsync(name);
                textBoxCreateIngredient.Clear();
                await LoadLookupsAsync(); // neu laden
            }
            catch (Exception ex)
            {
                MessageBox.Show("Zutat anlegen fehlgeschlagen:\n" + ex.Message);
            }
        }
        private void buttonAddIngredient_Click(object sender, EventArgs e)
        {
            // Ausgewählte Zutat ermitteln
            if (comboBoxIngredients.SelectedItem is not Ingredient ing)
            {
                return;
            }
            AddChip(ing);
        }

        private async void checkBoxFilterIngredient_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                await LoadAllRecipesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Filterfehler (Zutat):\n" + ex.Message);
            }
        }

        private async void comboBoxFilterIngredient_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBoxFilterIngredient.Checked)
                {
                    await LoadAllRecipesAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Filterfehler (Zutat):\n" + ex.Message);
            }
        }

        #endregion

        #region Kategorienverwaltung
        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedCategory = comboBoxCategory.SelectedItem as Category;
        }
        private async void buttonCreateCategory_Click(object sender, EventArgs e)
        {
            var name = (textBoxCreateCategory.Text ?? "").Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Bitte Kategorienamen eingeben.");
                return;
            }

            try
            {
                var cat = await _categoryService.GetOrCreateAsync(name);
                textBoxCreateCategory.Clear();
                await LoadLookupsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kategorie anlegen fehlgeschlagen:\n" + ex.Message);
            }
        }
        private void buttonAddCategory_Click(object sender, EventArgs e)
        {
            if (comboBoxCategory.SelectedItem is Category cat)
            {
                AddCategoryChip(cat);
            }
        }
        private async void checkBoxFilterKategorie_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                await LoadAllRecipesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Filterfehler:\n" + ex.Message);
            }
        }
        private async void comboBoxFilterKategorie_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBoxFilterKategorie.Checked)
                {
                    await LoadAllRecipesAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Filterfehler:\n" + ex.Message);
            }
        }
        private async void buttonSaveEditCategory_Click(object sender, EventArgs e)
        {
            if (comboBoxEditCategory.SelectedItem is not Category sel)
            {
                MessageBox.Show("Bitte zuerst eine Kategorie auswählen.");
                return;
            }

            var newName = (textBoxEditCategorie.Text ?? "").Trim();
            if (string.IsNullOrWhiteSpace(newName))
            {
                MessageBox.Show("Neuen Kategorienamen eingeben.");
                return;
            }

            try
            {
                buttonSaveEditCategory.Enabled = false;
                await _categoryService.UpdateNameAsync(sel.Id, newName);

                // UI aktualisieren: Filter-Combo, „Neues Rezept“-Combos, Edit-Combo und Grids
                await LoadFilterCategoryLookupsAsync();
                await LoadFilterIngredientLookupAsync();
                await LoadEditCategoryLookupAsync(sel.Id);
                await LoadAllRecipesAsync();
                await LoadMyRecipesAsync();

                MessageBox.Show("Kategorie wurde umbenannt.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Änderung fehlgeschlagen:\n" + ex.Message);
            }
            finally
            {
                buttonSaveEditCategory.Enabled = true;
            }
        }
        private void comboBoxEditCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEditCategory.SelectedItem is Category cat)
            {
                textBoxEditCategorie.Text = cat.Name;
            }
            else
            {
                textBoxEditCategorie.Clear();
            }
        }

        #endregion

        #region Rezeptbearbeitung (Edit-Tab)
        private async void dgvMyRecipes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_currentUser == null || e.RowIndex < 0)
            {
                return;
            }

            try
            {
                var row = dgvMyRecipes.Rows[e.RowIndex];
                var idVal = row.Cells["colId"]?.Value;

                if (idVal is null || idVal == DBNull.Value)
                {
                    MessageBox.Show("Rezept-ID nicht gefunden.");
                    return;
                }

                // ID extrahieren
                int recipeId = Convert.ToInt32(idVal);

                await OpenEditTabAsync(recipeId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Öffnen fehlgeschlagen:\n" + ex.Message);
            }
        }

        private async Task OpenEditTabAsync(int recipeId)
        {
            // Rezept inkl. Zutaten und Kategorien laden
            var r = await _db.Recipes
                .Include(x => x.Ingredients)
                .Include(x => x.Categories)
                .FirstAsync(x => x.Id == recipeId && x.UserId == _currentUser!.Id);

            _editMode = true;
            _editRecipeId = recipeId;

            // Falls der Edit-Tab noch nicht sichtbar ist → einblenden
            if (tabPageEdit.Parent == null)
            {
                tabControl.TabPages.Add(tabPageEdit);
            }

            // Felder füllen
            textBoxEditName.Text = r.Name;
            textBoxEditDescription.Text = r.Description;

            // Bestehende Auswahl zurücksetzen
            _editIngredientIds.Clear();
            _editCategoryIds.Clear();
            flowLayoutPanelEditIngredients.Controls.Clear();
            flowLayoutPanelEditCategories.Controls.Clear();

            // Zutatenchips aufbauen
            foreach (var ing in r.Ingredients)
            {
                AddEditChip(flowLayoutPanelEditIngredients, _editIngredientIds, ing.Id, ing.Name);
            }

            // Kategorienchips aufbauen
            foreach (var cat in r.Categories)
            {
                AddEditChip(flowLayoutPanelEditCategories, _editCategoryIds, cat.Id, cat.Name);
            }

            // Dropdowns befüllen (alle Zutaten/Kategorien)
            await LoadEditLookupsAsync();

            // Tab auswählen
            tabControl.SelectedTab = tabPageEdit;
        }


        private async void buttonCreateIngredientInEdit_Click(object sender, EventArgs e)
        {
            var name = (textBoxCreateIngredientInEdit.Text ?? "").Trim();
            if (string.IsNullOrWhiteSpace(name)) { MessageBox.Show("Bitte einen Zutaten-Namen eingeben."); return; }

            try
            {
                buttonCreateIngredientInEdit.Enabled = false;

                // anlegen/finden
                var ing = await _ingredientService.GetOrCreateAsync(name);

                // Combobox neu laden & auswählen
                await LoadEditIngredientsLookupAsync(ing.Id);

                // sofort zum Rezept hinzufügen (Chip)
                if (!_editIngredientIds.Contains(ing.Id))
                    AddEditChip(flowLayoutPanelEditIngredients, _editIngredientIds, ing.Id, ing.Name);

                textBoxCreateIngredientInEdit.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Zutat anlegen fehlgeschlagen:\n" + ex.Message);
            }
            finally
            {
                buttonCreateIngredientInEdit.Enabled = true;
            }
        }
        private async void buttonCreateCategoryInEdit_Click(object sender, EventArgs e)
        {
            var name = (textBoxCreateCategoryInEdit.Text ?? "").Trim();
            if (string.IsNullOrWhiteSpace(name)) { MessageBox.Show("Bitte einen Kategorienamen eingeben."); return; }

            try
            {
                buttonCreateCategoryInEdit.Enabled = false;

                var cat = await _categoryService.GetOrCreateAsync(name);

                await LoadEditCategoriesLookupAsync(cat.Id);

                if (!_editCategoryIds.Contains(cat.Id))
                    AddEditChip(flowLayoutPanelEditCategories, _editCategoryIds, cat.Id, cat.Name);

                textBoxCreateCategoryInEdit.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kategorie anlegen fehlgeschlagen:\n" + ex.Message);
            }
            finally
            {
                buttonCreateCategoryInEdit.Enabled = true;
            }
        }
        private void buttonEditAddCategory_Click(object sender, EventArgs e)
        {
            if (cmbEditCategory.SelectedItem is Category cat)
                AddEditChip(flowLayoutPanelEditCategories, _editCategoryIds, cat.Id, cat.Name);
        }
        private void buttonEditAddIngredient_Click(object sender, EventArgs e)
        {
            if (cmbEditIngredient.SelectedItem is Ingredient ing)
                AddEditChip(flowLayoutPanelEditIngredients, _editIngredientIds, ing.Id, ing.Name);
        }
        private async void buttonEditSave_Click(object sender, EventArgs e)
        {
            var name = (textBoxEditName.Text ?? "").Trim();
            var desc = (textBoxEditDescription.Text ?? "").Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Name fehlt.");
                return;
            }
            if (string.IsNullOrWhiteSpace(desc))
            {
                MessageBox.Show("Beschreibung fehlt.");
                return;
            }
            if (_editIngredientIds.Count == 0)
            {
                MessageBox.Show("Mind. eine Zutat wählen.");
                return;
            }
            if (_editCategoryIds.Count == 0)
            {
                MessageBox.Show("Mind. eine Kategorie wählen.");
                return;
            }

            try
            {
                await _recipeService.UpdateAsync(
                    _editRecipeId, _currentUser!.Id,
                    name, desc,
                    _editIngredientIds, _editCategoryIds
                );
                await LoadMyRecipesAsync();
                await LoadAllRecipesAsync();
                CloseEditTab();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Speichern fehlgeschlagen:\n" + ex.Message);
            }
        }
        private void buttonEditCancel_Click(object sender, EventArgs e)
        {
            CloseEditTab();
        }
        private void CloseEditTab()
        {
            _editMode = false;
            _editRecipeId = 0;
            textBoxEditName.Clear();
            textBoxEditDescription.Clear();
            flowLayoutPanelEditIngredients.Controls.Clear();
            flowLayoutPanelEditCategories.Controls.Clear();
            _editIngredientIds.Clear();
            _editCategoryIds.Clear();

            // zurück zu "Meine Rezepte"
            tabControl.SelectedTab = tabPageOwnRecipes;

            // Edit-Tab wieder verstecken
            tabControl.TabPages.Remove(tabPageEdit);
            tabPageEdit.Parent = null;
        }
        #endregion

        #region UI-Helfer
        private void ClearChips()
        {
            _selectedIngredients.Clear();
            foreach (Control c in flowSelectedIngredients.Controls) c.Dispose();
            flowSelectedIngredients.Controls.Clear();
        }
        private void AddChip(Ingredient ing)
        {
            // Duplikate verhindern
            if (_selectedIngredients.Any(x => x.Id == ing.Id))
            {
                return;
            }

            _selectedIngredients.Add(ing);

            var chip = new FlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = Color.WhiteSmoke,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(4),
                Padding = new Padding(8, 4, 8, 4),
                Tag = ing.Id
            };

            var lbl = new Label
            {
                AutoSize = true,
                Text = ing.Name,
                Margin = new Padding(0, 3, 6, 0)
            };

            var btn = new Button
            {
                Text = "×",
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = Padding.Empty,
                Margin = new Padding(0, 0, 0, 0),
                FlatStyle = FlatStyle.Flat,
                TabStop = false,
                MinimumSize = new Size(18, 18)       // klein halten
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += (_, __) => RemoveChip(ing.Id, chip);

            chip.Controls.Add(lbl);
            chip.Controls.Add(btn);
            flowSelectedIngredients.Controls.Add(chip);
        }
        private void RemoveChip(int ingredientId, Control chip)
        {
            _selectedIngredients.RemoveAll(x => x.Id == ingredientId);
            flowSelectedIngredients.Controls.Remove(chip);
            chip.Dispose();
        }
        private void AddEditChip(FlowLayoutPanel host, HashSet<int> bag, int id, string name)
        {
            // Duplikate verhindern
            if (!bag.Add(id))
            {
                return;
            }

            var chip = new FlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = Color.WhiteSmoke,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(4),
                Padding = new Padding(8, 4, 8, 4),
                Tag = id
            };
            var lbl = new Label
            {
                AutoSize = true,
                Text = name,
                Margin = new Padding(0, 3, 6, 0)
            };

            var btn = new Button
            {
                Text = "×",
                AutoSize = true,
                FlatStyle = FlatStyle.Flat,
                Padding = Padding.Empty,
                Margin = new Padding(0)
            };

            btn.FlatAppearance.BorderSize = 0;
            btn.Click += (_, __) =>
                {
                    bag.Remove(id);
                    host.Controls.Remove(chip);
                    chip.Dispose();
                };

            chip.Controls.Add(lbl);
            chip.Controls.Add(btn);
            host.Controls.Add(chip);
        }
        private void AddCategoryChip(Category cat)
        {
            // Duplikate verhindern
            if (_selectedCategories.Any(c => c.Id == cat.Id))
            {
                return;
            }
            _selectedCategories.Add(cat);

            var chip = new FlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = Color.WhiteSmoke,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(4),
                Padding = new Padding(8, 4, 8, 4),
                Tag = cat.Id
            };

            var lbl = new Label
            {
                AutoSize = true,
                Text = cat.Name,
                Margin = new Padding(0, 3, 6, 0)
            };

            var btn = new Button
            {
                Text = "×",
                AutoSize = true,
                FlatStyle = FlatStyle.Flat,
                Padding = Padding.Empty,
                Margin = new Padding(0)
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += (_, __) =>
            {
                // aus Sammlung entfernen
                var id = (int)chip.Tag!;
                var toRemove = _selectedCategories.FirstOrDefault(c => c.Id == id);
                if (toRemove != null) _selectedCategories.Remove(toRemove);

                // aus UI entfernen
                flowLayoutPanelKategory.Controls.Remove(chip);
                chip.Dispose();
            };

            chip.Controls.Add(lbl);
            chip.Controls.Add(btn);
            flowLayoutPanelKategory.Controls.Add(chip);
        }
        #endregion

        private async void dgvMyRecipes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_currentUser == null)
            {
                return;
            }
            if (e.RowIndex < 0)
            {
                return;
            }

            var grid = dgvMyRecipes;
            var colName = grid.Columns[e.ColumnIndex].Name;

            // Nur Löschen-Spalte behandeln
            if (colName != "colDelete")
            {
                return; 
            }

            // ID aus versteckter Spalte holen
            var idObj = grid.Rows[e.RowIndex].Cells["colId"].Value;
            if (idObj is null || idObj == DBNull.Value) return;
            int recipeId = Convert.ToInt32(idObj);

            // Bestätigen
            if (MessageBox.Show("Rezept wirklich löschen?", "Bestätigen",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                await _recipeService.DeleteAsync(recipeId, _currentUser.Id);
                await LoadMyRecipesAsync();
                await LoadAllRecipesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Löschen fehlgeschlagen:\n" + ex.Message);
            }
        }
    }
}
