using Microsoft.EntityFrameworkCore;
using RecipeHub.Library.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.Library.Data;

public class RecipeHubDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Recipe> Recipes => Set<Recipe>();
    public DbSet<Ingredient> Ingredients => Set<Ingredient>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // DB liegt zentral im Projekt (immer gleicher Pfad, egal ob Debug/Release)
        var slnRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
        var dbPath = Path.Combine(slnRoot, "recipehub.db");

        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Eindeutige Werte 
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

        modelBuilder.Entity<Recipe>()
            .HasIndex(r => r.Name)
            .IsUnique();

        modelBuilder.Entity<Category>()
            .HasIndex(c => c.Name)
            .IsUnique();

        modelBuilder.Entity<Ingredient>()
            .HasIndex(i => i.Name)
            .IsUnique();

        // ---- Beziehungen ----
        // Beziehung: Ein User hat viele Rezepte, ein Rezept gehört zu einem User
        modelBuilder.Entity<Recipe>()
            .HasOne(r => r.User)
            .WithMany(u => u.Recipes)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Beziehung: N:M für Favoriten (User <-> Recipe)
        modelBuilder.Entity<User>()
            .HasMany(u => u.FavoriteRecipes)
            .WithMany()
            .UsingEntity(j => j.ToTable("UserFavorites"));
    }
}
