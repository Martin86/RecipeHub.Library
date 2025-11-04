using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipeHub.Library.Data;
using RecipeHub.Library.Models;

namespace RecipeHub.Library.Services;

// Verwaltet Favoriten-Funktionalitäten
public class FavoriteService
{
    private readonly RecipeHubDbContext _context;

    public FavoriteService(RecipeHubDbContext context)
    {
        _context = context;
    }

    // Rezept als Favorit markieren
    public async Task AddFavoriteAsync(int userId, int recipeId)
    {
        var user = await _context.Users
            .Include(u => u.FavoriteRecipes)
            .FirstOrDefaultAsync(u => u.Id == userId);

        var recipe = await _context.Recipes.FindAsync(recipeId);

        if (user == null || recipe == null)
            throw new InvalidOperationException("User oder Rezept nicht gefunden.");

        if (recipe.UserId == userId)
            throw new InvalidOperationException("Eigene Rezepte können nicht favorisiert werden.");

        if (user.FavoriteRecipes.Any(r => r.Id == recipeId))
            return; // schon als Favorit vorhanden

        user.FavoriteRecipes.Add(recipe);
        await _context.SaveChangesAsync();
    }

    // Favorit wieder entfernen
    public async Task RemoveFavoriteAsync(int userId, int recipeId)
    {
        var user = await _context.Users
            .Include(u => u.FavoriteRecipes)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            throw new InvalidOperationException("User nicht gefunden.");

        var recipe = user.FavoriteRecipes.FirstOrDefault(r => r.Id == recipeId);

        if (recipe != null)
        {
            user.FavoriteRecipes.Remove(recipe);
            await _context.SaveChangesAsync();
        }
    }

    // Favoriten eines Users abrufen
    public async Task<List<Recipe>> GetFavoritesAsync(int userId)
    {
        var user = await _context.Users
            .Include(u => u.FavoriteRecipes)
            .FirstOrDefaultAsync(u => u.Id == userId);

        return user?.FavoriteRecipes ?? new List<Recipe>();
    }
}
