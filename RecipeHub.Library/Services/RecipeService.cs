using Microsoft.EntityFrameworkCore;
using RecipeHub.Library.Data;
using RecipeHub.Library.Models;
using RecipeHub.Library.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeHub.Library.Services
{
    public class RecipeService
    {
        private readonly IRepository<Recipe> _recipeRepo;
        private readonly UserService _userService;
        private readonly RecipeHubDbContext _context;

        public RecipeService(IRepository<Recipe> recipeRepo, UserService userService, RecipeHubDbContext context)
        {
            _recipeRepo = recipeRepo;
            _userService = userService;
            _context = context;  // DbContext direkt zuweisen
        }

        // Neues Rezept anlegen (mit Validierung)
        public async Task AddRecipeAsync(Recipe recipe)
        {
            // Prüfen, ob der User existiert
            if (recipe.UserId == 0 || !await _userService.ExistsAsync(recipe.UserId))
                throw new InvalidOperationException("Nur registrierte Benutzer dürfen Rezepte anlegen.");

            // Pflichtfelder prüfen
            if (string.IsNullOrWhiteSpace(recipe.Name))
                throw new ArgumentException("Rezeptname darf nicht leer sein.");
            if (string.IsNullOrWhiteSpace(recipe.Description))
                throw new ArgumentException("Rezeptbeschreibung darf nicht leer sein (mind. ein Zubereitungsschritt).");
            if (recipe.Ingredients.Count == 0)
                throw new ArgumentException("Mindestens eine Zutat erforderlich.");
            if (recipe.Categories.Count == 0)
                throw new ArgumentException("Mindestens eine Kategorie erforderlich.");

            // Eindeutigkeit des Namens prüfen
            var all = await _recipeRepo.GetAllAsync();
            if (all.Any(r => r.Name.Equals(recipe.Name, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("Ein Rezept mit diesem Namen existiert bereits.");

            // Jetzt wird der Repository verwendet und der _context (DbContext) intern genutzt
            await _recipeRepo.AddAsync(recipe);
        }

        public async Task<List<Recipe>> GetAllAsync()
            => await _context.Recipes.ToListAsync();

        public async Task UpdateAsync(int recipeId, int userId, string name, string description, IEnumerable<int> ingredientIds, IEnumerable<int> categoryIds)
        {
            // Überprüfen, ob der User existiert
            if (!await _userService.ExistsAsync(userId))
                throw new InvalidOperationException("Unbekannter Benutzer.");

            // Validierungen
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name darf nicht leer sein.");
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Beschreibung darf nicht leer sein.");

            // Rezept suchen und laden
            var recipe = await _context.Recipes
                .Include(r => r.Ingredients)
                .Include(r => r.Categories)
                .FirstOrDefaultAsync(r => r.Id == recipeId && r.UserId == userId)
                ?? throw new InvalidOperationException("Rezept nicht gefunden oder nicht berechtigt.");

            // Eindeutigkeit des Namens prüfen
            if (await _context.Recipes.AnyAsync(r => r.Id != recipeId && r.Name.ToLower() == name.Trim().ToLower()))
                throw new InvalidOperationException("Ein anderes Rezept mit diesem Namen existiert bereits.");

            // Rezept aktualisieren
            recipe.Name = name.Trim();
            recipe.Description = description.Trim();

            // Zutaten und Kategorien aktualisieren
            recipe.Ingredients.Clear();
            var ings = await _context.Ingredients.Where(i => ingredientIds.Contains(i.Id)).ToListAsync();
            recipe.Ingredients.AddRange(ings);

            recipe.Categories.Clear();
            var cats = await _context.Categories.Where(c => categoryIds.Contains(c.Id)).ToListAsync();
            recipe.Categories.AddRange(cats);

            // Änderungen speichern
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int recipeId, int userId)
        {
            var recipe = await _context.Recipes
                .FirstOrDefaultAsync(r => r.Id == recipeId && r.UserId == userId)
                ?? throw new InvalidOperationException("Rezept nicht gefunden oder nicht berechtigt.");

            await _recipeRepo.DeleteAsync(recipeId);
        }
    }

}
