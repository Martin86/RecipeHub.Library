using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeHub.Library.Models;

// Globale Zutaten (userunabhängig)
public class Ingredient
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    // N:M Beziehung zu Rezepten
    public List<Recipe> Recipes { get; set; } = new();
}
