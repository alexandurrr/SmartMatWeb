using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using smartmat.Data;
using smartmat.Models;

namespace smartmat.Controllers
{
    public class MealsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public MealsController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        
        public ViewResult Breakfast()
        {
            return View();
        }

        public ViewResult FastFood()
        {
            return View();
        }
        
        
        public ViewResult Dinner()
        {
            return View();
        }
        
        public ViewResult Dessert()
        {
            return View();
        }
        public async Task<IActionResult> GetFilteredRecipes(string category, bool vegetarian, bool glutenfree)
        {
            var user = await _userManager.GetUserAsync(User);
            var vm = new RecipeUserViewModel
            {
                Users = _userManager.Users.ToList(),
                Recipes = _db.Recipes
                    .Where(recipe => recipe.Visibility == "Public" || recipe.ApplicationUser == user)
                    .Where(recipe => (category == "Rask Mat" && recipe.Time <= 20) || recipe.Category == category)
                    .Where(recipe => recipe.Vegetarian || !vegetarian)
                    .Where(recipe => recipe.Glutenfree || !glutenfree)
                    .OrderByDescending(recipe => recipe.Reviews.Average(r => r.Stars)).ToList(),
                Reviews = _db.Reviews.ToList()
            };
            return PartialView("_RecipePartial", vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}