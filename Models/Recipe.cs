using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using smartmat.Data;

namespace smartmat.Models
{
    public class Recipe
    {
        public Recipe()
        {
        }

        public Recipe(string title, string introduction, string ingredients, string instructions, string nutrients)
        {
            Title = title;
            Introduction = introduction;
            Ingredients = ingredients;
            Instructions = instructions;
            Nutrients = nutrients;
        }

        public int Id { get; set; }
        
        [Required] 
        [StringLength(200)] 
        [DisplayName("Title")]
        public string Title { get; set; }
        [Required]
        [StringLength(1000)]
        [DisplayName("Introduction")]
        public string Introduction { get; set; }

        [Required]
        [StringLength(1000)]
        [DisplayName("Ingredients")]
        public string Ingredients { get; set; }

        [Required] 
        [StringLength(1000)] 
        [DisplayName("Instructions")]
        public string Instructions { get; set; }
            
        [Required] 
        [StringLength(1000)] 
        [DisplayName("Nutrients")]
        public string Nutrients { get; set; }
        
        // Foreign key for Users based on name
        public string UserId { get; set; } 
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
       
        
    }
    
}