using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.Library.Models;
using RecipeHub.Library.Repositories;

namespace RecipeHub.Library.Services;

// Verwaltet Zutaten (global, unabhängig vom Benutzer)
public class IngredientService
{
    private readonly IRepository<Ingredient> _ingredients;

    public IngredientService(IRepository<Ingredient> ingredients)
    {
        _ingredients = ingredients;
    }

    // Gibt vorhandene Zutat zurück oder legt sie neu an
    public async Task<Ingredient> GetOrCreateAsync(string name)
    {
        // Eingabe prüfen
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Zutat darf nicht leer sein.");

        var all = await _ingredients.GetAllAsync();

        // Prüfen, ob Zutat schon existiert (case-insensitive)
        var existing = all.FirstOrDefault(i =>
            i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        if (existing is not null)
            return existing;

        // Neue Zutat anlegen
        var ingredient = new Ingredient { Name = name.Trim() };
        await _ingredients.AddAsync(ingredient);
        return ingredient;
    }

    // Gibt alle Zutaten zurück (für Listen etc.)
    public async Task<List<Ingredient>> GetAllAsync()
    {
        return await _ingredients.GetAllAsync();
    }
}

