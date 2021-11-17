using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using smartmat.Data;

namespace smartmat.Models
{
    public class RecipeImageViewModel
    {
        // Bool to check if post is success
        public bool IsSuccess { get; set; }
        
        // IFormFile used to add image to wwwroot/recipeimages
        [Required]
        public IFormFile Image { get; set; }
    
        // Recipes columns
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
   
        [Required]
        [StringLength(1000)]
        public string Introduction { get; set; }

        [Required]
        [StringLength(1000)]
        public string Ingredients { get; set; }

        [Required]
        [StringLength(1000)] 
        public string Instructions { get; set; }

        [Required]
        [StringLength(1000)] 
        public string Nutrients { get; set; }

        [Required]
        [StringLength(100)]
        public string Visibility { get; set; }
        
        [Required]
        public bool Glutenfree { get; set; }
        
        [Required]
        [StringLength(1000)]
        public string Category { get; set; }
        
        [Required]
        public bool Vegetarian { get; set; }
    }
}