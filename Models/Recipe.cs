using System;
using System.Collections.Generic;
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

        public Recipe(string title, string introduction, string category, string ingredients, string instructions, string nutrients, string visibility, string image)
        {
            Title = title;
            Introduction = introduction;
            Category = category;
            Ingredients = ingredients;
            Instructions = instructions;
            Nutrients = nutrients;
            Visibility = visibility;
            Image = image;
        }

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
        public string Image { get; set; }
        
        [Required]
        [StringLength(100)]
        [DisplayName("Synlighet")]
        public string Visibility { get; set; }
        
        // Foreign keys
        public string UserId { get; set; } 
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public List<Review> Reviews { get; set; }
    }
    
}