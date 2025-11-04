using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeHub.Library.Models;

public class Recipe
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = "";

    [Required]
    public string Description { get; set; } = "";

    // FK zu User
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    public User? User { get; set; }

    // Navigationen
    public List<Ingredient> Ingredients { get; set; } = new();
    public List<Category> Categories { get; set; } = new();
}
