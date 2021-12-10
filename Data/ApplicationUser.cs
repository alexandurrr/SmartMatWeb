using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using smartmat.Models;

namespace smartmat.Data
{
    public class ApplicationUser : IdentityUser
    {
        // Configuring relation (1 to many)
        // user-recipes
        public ICollection<Recipe> Recipes { get; set; }
        
        [MinLength(2)]
        public string Firstname { get; set; }
        
        [MinLength(2)]
        public string Lastname { get; set; }
        
        [MaxLength(400)]
        public string Bio { get; set; }
        
        public bool ActivityReminder { get; set; }
        
        public string ImagePath { get; set; }
        
        public string ImageDelete { get; set; }
        
        public List<FavoritesRecipeUser> Favorites { get; set; }
        
        public bool DarkMode { get; set; }
    }
}