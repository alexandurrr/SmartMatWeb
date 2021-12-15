using System.Collections.Generic;
using System.ComponentModel;
using smartmat.Data;

namespace smartmat.Models
{
    public class RecipeUserViewModel
    {
        public ICollection<Recipe> Recipes { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public bool Favorite { get; set; }

    }
}