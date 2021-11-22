using Microsoft.AspNetCore.Http;

namespace smartmat.Models
{
    public class ImageEditViewModel
    {
        public Recipe Recipe { get; set; }
        public IFormFile Image { get; set; }
    }
}