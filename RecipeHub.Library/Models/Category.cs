using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeHub.Library.Models;

// Kategorie eines Rezepts (z. B. Dessert, Hauptgericht)
public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    // N:M Beziehung zu Rezepten
    public List<Recipe> Recipes { get; set; } = new();
}
