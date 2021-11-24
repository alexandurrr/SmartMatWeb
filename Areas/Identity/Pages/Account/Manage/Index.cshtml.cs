using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using smartmat.Data;

namespace smartmat.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string Bio { get; set; }
            
            public IFormFile Image { get; set; }
            
            public string ImagePath { get; set; }
            
            public string ImageDelete { get; set; }
            public bool ActivityReminder { get; set; }
        }
        
        private void Load(ApplicationUser user)
        {
            Input = new InputModel
            {
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Username = user.UserName,
                Email = user.Email,
                Bio = user.Bio,
                ImagePath = user.ImagePath,
                ActivityReminder = user.ActivityReminder
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            Load(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var image = Input.Image;
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                Load(user);
                return Page();
            }
            
            if (image != null)
            {
                if (user.ImagePath != null)
                {
                    System.IO.File.Delete(user.ImageDelete);
                }
                // New file name using Guid
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                var deletePath =Path.Combine("wwwroot", "userimages", fileName);

                // Creating full file path string and appending the file name
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "userimages", fileName);
                    
                // open-create the file in a stream and copying the uploaded
                // Into the new file
                await using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
                
                // Assigning the generated filePath
                user.ImageDelete = deletePath;
                user.ImagePath = $"{Request.Scheme}://{Request.Host}/userimages/{fileName}";
            }
            
            Input.ImagePath = user.ImagePath;
            user.Firstname = Input.Firstname;
            user.Lastname = Input.Lastname;
            user.UserName = Input.Username;
            user.Email = Input.Email;
            user.Bio = Input.Bio;
            user.ActivityReminder = Input.ActivityReminder;
            
            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
