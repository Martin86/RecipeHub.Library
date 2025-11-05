using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.Library.Models;
using RecipeHub.Library.Repositories;

namespace RecipeHub.Library.Services;

// Verwaltet Zutaten
public class IngredientService
{
    private readonly IRepository<Ingredient> _ingredients;
    // globale Zutatenliste (Textdatei)
    private readonly string _txtFilePath = Path.Combine(Environment.CurrentDirectory, @"Zutatenliste.txt");

    public IngredientService(IRepository<Ingredient> ingredients)
    {
        _ingredients = ingredients;
    }

    
    // Methode zum Laden der Zutaten aus der Textdatei und in die DB einfügen
    public async Task LoadIngredientsToDatabaseAsync()
    {
        // Zutaten aus der Textdatei lesen
        var ingredients = ReadIngredientsFromTxt();

        foreach (var ingredientName in ingredients)
        {
            // Prüfen, ob die Zutat bereits existiert
            var existingIngredient = await _ingredients.GetAllAsync();

            if (!existingIngredient.Any(i => i.Name.Equals(ingredientName, StringComparison.OrdinalIgnoreCase)))
            {
                // Zutat existiert nicht, füge sie hinzu
                var ingredient = new Ingredient { Name = ingredientName.Trim() };
                await _ingredients.AddAsync(ingredient);
            }
        }
    }

    // Methode zum Lesen der Zutaten aus der Textdatei
    private List<string> ReadIngredientsFromTxt()
    {
        var ingredients = new List<string>();

        // Datei lesen
        var lines = File.ReadAllLines(_txtFilePath);

        foreach (var line in lines)
        {
            // Jede Zutat ist durch kommas getrennt
            var ingredientsInLine = line.Split(',');

            foreach (var ingredient in ingredientsInLine)
            {
                // Zutaten trimmen und hinzufügen
                ingredients.Add(ingredient.Trim());
            }
        }

        return ingredients;
    }

    // Gibt vorhandene Zutat zurück oder legt sie neu an
    public async Task<Ingredient> GetOrCreateAsync(string name)
    {
        // Eingabe prüfen
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Zutat darf nicht leer sein.");

        var all = await _ingredients.GetAllAsync();

        // Prüfen, ob Zutat schon existiert
        var existing = all.FirstOrDefault(i =>
            i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        if (existing is not null)
            throw new ArgumentException("Zutat existiert bereits.");

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

