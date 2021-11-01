using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using smartmat.Models;

namespace smartmat.Data
{
    public class ApplicationUser : IdentityUser
    {
        // Configuring relation (1 to many)
        // user-recipes
        public ICollection<Recipe> Recipes { get; set; }
    }
}