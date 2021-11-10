using System.Threading.Tasks;
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

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                Load(user);
                return Page();
            }

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
