using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

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
        [DisplayName("Tittel")]
        public string Title { get; set; }
   
        [Required]
        [StringLength(1000)]
        [DisplayName("Introduksjon")]
        public string Introduction { get; set; }

        [Required]
        [StringLength(1000)]
        [DisplayName("Kategori")]
        public string Category { get; set; }
        
        [Required]
        [DisplayName("Glutenfri")]
        public bool Glutenfree { get; set; }
        
        [Required]
        [DisplayName("Vegetar")]
        public bool Vegetarian { get; set; }

        [Required]
        [StringLength(1000)]
        [DisplayName("Ingredienser")]
        public string Ingredients { get; set; }

        [Required]
        [StringLength(1000)]
        [DisplayName("Fremgangsmåte")]
        public string Instructions { get; set; }
        
        [Required]
        [StringLength(1000)] 
        [DisplayName("Næringsstoffer")]
        public string Nutrients { get; set; }
        
        [Required]
        [StringLength(100)]
        [DisplayName("Synlighet")]
        public string Visibility { get; set; }
        
        [Required]
        [DisplayName("Tilberedningstid")]
        public int Time { get; set; }
    }
}