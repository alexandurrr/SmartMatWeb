using System.Collections.Generic;
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

        // IFormFile used to add image to wwwroot/image
        public IFormFile Image { get; set; }
    
        // Recipes columns
        public string Title { get; set; }
   
        public string Introduction { get; set; }

        public string Ingredients { get; set; }

        public string Instructions { get; set; }

        public string Nutrients { get; set; }

        public string Visibility { get; set; }

    }
}