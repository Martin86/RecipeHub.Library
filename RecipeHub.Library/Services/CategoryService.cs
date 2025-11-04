using Microsoft.EntityFrameworkCore;
using RecipeHub.Library.Data;
using RecipeHub.Library.Models;
using RecipeHub.Library.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RecipeHub.Library.Services;

// Verwaltet Kategorien
public class CategoryService
{
    private readonly IRepository<Category> _categories;
    private readonly RecipeHubDbContext _db;

    public CategoryService(IRepository<Category> categories, RecipeHubDbContext db)
    {
        _categories = categories;
        _db = db;
    }

    // Kategorie holen oder neu anlegen, Namen sind eindeutig
    public async Task<Category> GetOrCreateAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Kategoriename darf nicht leer sein.");

        var all = await _categories.GetAllAsync();

        // existiert Kategorie?
        var existing = all.FirstOrDefault(c =>
            c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        if (existing is not null)
            return existing;

        // Neue Kategorie speichern
        var category = new Category { Name = name.Trim() };
        await _categories.AddAsync(category);
        return category;
    }

    public async Task UpdateNameAsync(int categoryId, string newName)
    {
        var n = (newName ?? "").Trim();
        if (string.IsNullOrWhiteSpace(n))
            throw new ArgumentException("Kategoriename darf nicht leer sein.");

        // Name muss eindeutig bleiben
        bool exists = await _db.Categories
            .AnyAsync(c => c.Id != categoryId && c.Name.ToLower() == n.ToLower());
        if (exists)
            throw new InvalidOperationException("Kategoriename ist bereits vergeben.");

        var cat = await _db.Categories.FindAsync(categoryId)
                  ?? throw new InvalidOperationException("Kategorie nicht gefunden.");

        cat.Name = n;
        await _db.SaveChangesAsync();
    }

    // Alle Kategorien abrufen
    public async Task<List<Category>> GetAllAsync()
    {
        return await _categories.GetAllAsync();
    }
}
