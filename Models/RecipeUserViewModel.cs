using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using smartmat.Data;

namespace smartmat.Models
{
    public class RecipeUserViewModel
    {
        public ICollection<Recipe> Recipes { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }
        public ICollection<Review> Reviews { get; set; }
        
        // Bool to check if post is success
        public bool IsSuccess { get; set; }
        
        // IFormFile used to add image to wwwroot/recipeimages
        [Required]
        public IFormFile Image { get; set; }
    
        // Recipes columns
        [Required]
        public string Title { get; set; }
   
        [Required]
        public string Introduction { get; set; }

        [Required]
        public string Ingredients { get; set; }

        [Required]
        public string Instructions { get; set; }

        [Required]
        public string Nutrients { get; set; }

        [Required]
        public string Visibility { get; set; }

    }
}