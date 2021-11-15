using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using smartmat.Data;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace smartmat.Models
{
    public class Recipe
    {
        public Recipe()
        {
        }

        public Recipe(string title, string introduction, string ingredients, string instructions, string nutrients, string visibility)
        {
            Title = title;
            Introduction = introduction;
            Ingredients = ingredients;
            Instructions = instructions;
            Nutrients = nutrients;
            Visibility = visibility;
        }
        // If you need to add more fields here
        // Make sure to add the same field into RecipeUserViewModel.cs
        // Edit the controller
        // Then the controller will add the value from our view model
        // Into Recipes.cs, then will push into the db
        public int Id { get; set; }
        
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
        
        // Image path in a string
        // can be used with <img> to display
        public string ImagePath { get; set; }
        
        [Required]
        [StringLength(100)]
        [DisplayName("Offentlig")]
        public string Visibility { get; set; }

        // Foreign keys
        public string UserId { get; set; } 
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public List<Review> Reviews { get; set; }
    }
    
}