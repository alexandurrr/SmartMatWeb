using smartmat.Data;

namespace smartmat.Models
{
    public class FavoritesRecipeUser
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}