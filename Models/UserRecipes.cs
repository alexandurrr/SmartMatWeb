using System.Collections.Generic;
using smartmat.Data;

namespace smartmat.Models
{
    public class UserRecipes
    {
        public IEnumerable<ApplicationUser> ApplicationUsers { get; set; }
        public IEnumerable<Recipe> Recipes { get; set; }
    }
}