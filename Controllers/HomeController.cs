using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using smartmat.Data;
using smartmat.Models;

namespace smartmat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        
        public async Task<IActionResult> Index()
        {
            // Selects the best 5 recipes based on the average review score 
            var user = await _userManager.GetUserAsync(User);
            var vm = new RecipeUserViewModel
            {
                Users = _userManager.Users.ToList(),
                Recipes = _db.Recipes
                    .Where(recipe => recipe.Visibility == "Public" || recipe.ApplicationUser == user)
                    .OrderByDescending(recipe => recipe.Reviews.Average(r => r.Stars))
                    .Take(5)
                    .ToList(),
                Reviews = _db.Reviews.ToList()
            };
            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
        
        public async Task<IActionResult> Recipes(string search)
        {

            if (search == null)
            {
                return NotFound();
            }

            // Selecting all recipes from database that are public or the users own recipes
            var user = await _userManager.GetUserAsync(User);
            var recipes = _db.Recipes
                .Where(recipe => recipe.Visibility == "Public" || recipe.ApplicationUser == user);

            // Calculates a rating for each ingredient based on the number of results
            var searchIngredients = search.ToLower().Split(", ");
            if (searchIngredients.Any())
                searchIngredients[^1] = searchIngredients[^1].Replace(",", "");
            
            var searchRatings = new Dictionary<int, float>();

            foreach (var recipe in recipes)
            {
                var recipeIngredients = recipe.Ingredients.ToLower().Split(", ");
                var currentSearchResults = (
                        from searchIngredient in searchIngredients 
                        from recipeIngredient in recipeIngredients 
                        where recipeIngredient.Contains(searchIngredient) 
                        select searchIngredient)
                    .Count();
                var totalIngredients = recipeIngredients.Length;
                if (totalIngredients < 1)
                {
                    currentSearchResults = 0;
                    totalIngredients = 1;
                }

                // Search by title
                var rating = (float)currentSearchResults / totalIngredients;
                if (recipe.Title.ToLower().Contains(search.ToLower()))
                    rating += 1;
                
                searchRatings.Add(recipe.Id, rating);
            }
            
            var result = recipes.AsEnumerable()
                .Where(recipe => searchRatings[recipe.Id] > 0)
                .OrderByDescending(recipe => searchRatings[recipe.Id]);
            
            
            // Uses the RecipeUserViewModel to send the recipes to the Partial View
            var ur = new RecipeUserViewModel
            {
                Recipes = result.ToList(),
                Users = _userManager.Users.ToList(),
                Reviews = _db.Reviews.ToList()
            };

            return PartialView("_RecipesPartial", ur);
        }
    }
}