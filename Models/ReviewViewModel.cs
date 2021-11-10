using System.Collections.Generic;

namespace smartmat.Models
{
    public class ReviewViewModel
    {
        public ICollection<Review> MyReviews { get; set; }
        public ICollection<Review> OthersReviews { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
    }
}