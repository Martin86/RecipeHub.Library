using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeHub.Library.Models;

// Repräsentiert einen registrierten Benutzer
public class User
{
    [Key] // Primärschlüssel
    public int Id { get; set; }

    [Required] // Pflichtfeld
    [MaxLength(100)] // Optionale Begrenzung
    public string Username { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Password { get; set; } = string.Empty;

    // Navigation: eigene Rezepte des Users
    public List<Recipe> Recipes { get; set; } = new();

    // Many-to-Many: Favoriten anderer Rezepte
    public List<Recipe> FavoriteRecipes { get; set; } = new();
}
