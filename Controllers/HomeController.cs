using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using smartmat.Data;
using smartmat.Models;
using SQLitePCL;

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
        
        public IActionResult Index()
        {
            //var reviews = _context.Reviews.ToList();
            var vm = new RecipeUserViewModel();
            vm.Users = _userManager.Users.ToList();
            vm.Recipes = _db.Recipes
                .OrderByDescending(recipe => recipe.Reviews.Average(r => r.Stars))
                .ToList();
            vm.Reviews = _db.Reviews.ToList();
            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
        
        public IActionResult Recipes(string search)
        {

            if (search == null)
            {
                return NotFound();
            }
            
            string[] ingredients = search.Split(", ");  //for å kunne søke på flere ingredienser

            // Grabbing all recipes from database
            var recipes = _db.Recipes.ToList();

            // Filtering out recipes based on ingredients
            foreach (var i in ingredients)
            {
                var list = searchfor(recipes, i);
                recipes = list;
            }
            
            // Creating a UserRecipe, with filtered recipe
            var ur = new UserRecipes
            {
                Recipes = recipes,
                ApplicationUsers = _userManager.Users.ToList()
            };

            return PartialView("_RecipesPartial", ur);
        }

        private List<Recipe> searchfor(List<Recipe> list, string i)
        {
            
            var result = list.Where(a => a.Ingredients.ToLower().Contains(i.ToLower()));
            return result.ToList();
        }
    }
}