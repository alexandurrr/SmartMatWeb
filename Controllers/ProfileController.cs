using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using smartmat.Data;

namespace smartmat.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        // GET
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        // GET
        [Authorize]
        public async Task<IActionResult> MyRecipes()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var recipes = _context.Recipes.Where(r => r.UserId == user.Id);
            /*var recipes = await _context.Recipes
                .FirstOrDefaultAsync(r => r.UserId == user.Id);*/

            return View(recipes);
        }
    }
}