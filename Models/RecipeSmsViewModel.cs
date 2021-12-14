using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using smartmat.Data;

namespace smartmat.Models
{
    public class RecipeSmsViewModel
    {
        public ICollection<Recipe> Recipes { get; set; }
        
        
        [DisplayName("SMS Ingredienser")]
        public string ingredientList { get; set; }
        
        
        [Required(ErrorMessage = "Dette feltet må fylles ut")]
        [DisplayName("Mobile nr.")]
        public string phoneNumber { get; set; }
        
    }
}