using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using smartmat.Data;
using smartmat.Models;

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

            return View(recipes);
        }
        
        // GET
        [Authorize]
        public async Task<IActionResult> MyReviews()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var vm = new ReviewViewModel();

            vm.MyReviews = _context.Reviews
                .Where(r => r.ApplicationUserId == user.Id)
                .ToList();
            
            vm.OthersReviews = _context.Reviews
                .Where(r => r.Recipe.UserId == user.Id)
                .ToList();
            
            vm.Recipes = _context.Recipes
                .ToList();

            return View(vm);
        }
    }
}