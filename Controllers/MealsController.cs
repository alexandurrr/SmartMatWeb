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
    public class MealsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public MealsController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        
        public async Task<IActionResult> Breakfast()
        {
            var user = await _userManager.GetUserAsync(User);
            var vm = new RecipeUserViewModel
            {
                Users = _userManager.Users.ToList(),
                Recipes = _db.Recipes
                    .Where(recipe => recipe.Visibility == "Public" || recipe.ApplicationUser == user)
                    .OrderByDescending(recipe => recipe.Reviews.Average(r => r.Stars))
                    .ToList(),
                Reviews = _db.Reviews.ToList()
            };
            
            return View(vm);
        }
        
        public async Task<IActionResult> BreakfastVegetarian()
        {
            var user = await _userManager.GetUserAsync(User);
            var vm = new RecipeUserViewModel
            {
                Users = _userManager.Users.ToList(),
                Recipes = _db.Recipes
                    .Where(recipe => recipe.Visibility == "Public" || recipe.ApplicationUser == user)
                    .OrderByDescending(recipe => recipe.Reviews.Average(r => r.Stars))
                    .ToList(),
                Reviews = _db.Reviews.ToList()
            };
            
            return View(vm);
        }
        
        public async Task<IActionResult> BreakfastGlutenfree()
        {
            var user = await _userManager.GetUserAsync(User);
            var vm = new RecipeUserViewModel
            {
                Users = _userManager.Users.ToList(),
                Recipes = _db.Recipes
                    .Where(recipe => recipe.Visibility == "Public" || recipe.ApplicationUser == user)
                    .OrderByDescending(recipe => recipe.Reviews.Average(r => r.Stars))
                    .ToList(),
                Reviews = _db.Reviews.ToList()
            };
            
            return View(vm);
        }
        
        public async Task<IActionResult> BreakfastGlutVeg()
        {
            var user = await _userManager.GetUserAsync(User);
            var vm = new RecipeUserViewModel
            {
                Users = _userManager.Users.ToList(),
                Recipes = _db.Recipes
                    .Where(recipe => recipe.Visibility == "Public" || recipe.ApplicationUser == user)
                    .OrderByDescending(recipe => recipe.Reviews.Average(r => r.Stars))
                    .ToList(),
                Reviews = _db.Reviews.ToList()
            };
            
            return View(vm);
        }

        public async Task<IActionResult> FastFood()
        {
            var user = await _userManager.GetUserAsync(User);
            var vm = new RecipeUserViewModel
            {
                Users = _userManager.Users.ToList(),
                Recipes = _db.Recipes
                    .Where(recipe => recipe.Visibility == "Public" || recipe.ApplicationUser == user)
                    .OrderByDescending(recipe => recipe.Reviews.Average(r => r.Stars))
                    .ToList(),
                Reviews = _db.Reviews.ToList()
            };
            return View(vm);
        }
        
        public async Task<IActionResult> FastFoodVegetarian()
        {
            var user = await _userManager.GetUserAsync(User);
            var vm = new RecipeUserViewModel
            {
                Users = _userManager.Users.ToList(),
                Recipes = _db.Recipes
                    .Where(recipe => recipe.Visibility == "Public" || recipe.ApplicationUser == user)
                    .OrderByDescending(recipe => recipe.Reviews.Average(r => r.Stars))
                    .ToList(),
                Reviews = _db.Reviews.ToList()
            };
            
            return View(vm);
        }
        
        public async Task<IActionResult> FastFoodGlutenfree()
        {
            var user = await _userManager.GetUserAsync(User);
            var vm = new RecipeUserViewModel
            {
                Users = _userManager.Users.ToList(),
                Recipes = _db.Recipes
                    .Where(recipe => recipe.Visibility == "Public" || recipe.ApplicationUser == user)
                    .OrderByDescending(recipe => recipe.Reviews.Average(r => r.Stars))
                    .ToList(),
                Reviews = _db.Reviews.ToList()
            };
            
            return View(vm);
        }
        
        public async Task<IActionResult> FastFoodGlutVeg()
        {
            var user = await _userManager.GetUserAsync(User);
            var vm = new RecipeUserViewModel
            {
                Users = _userManager.Users.ToList(),
                Recipes = _db.Recipes
                    .Where(recipe => recipe.Visibility == "Public" || recipe.ApplicationUser == user)
                    .OrderByDescending(recipe => recipe.Reviews.Average(r => r.Stars))
                    .ToList(),
                Reviews = _db.Reviews.ToList()
            };
            
            return View(vm);
        }

        public async Task<IActionResult> Lunch()
        {
            var user = await _userManager.GetUserAsync(User);
            var vm = new RecipeUserViewModel
            {
                Users = _userManager.Users.ToList(),
                Recipes = _db.Recipes
                    .Where(recipe => recipe.Visibility == "Public" || recipe.ApplicationUser == user)
                    .OrderByDescending(recipe => recipe.Reviews.Average(r => r.Stars))
                    .ToList(),
                Reviews = _db.Reviews.ToList()
            };
            return View(vm);
        }       
        
        public async Task<IActionResult> LunchVegetarian()
        {
            var user = await _userManager.GetUserAsync(User);
            var vm = new RecipeUserViewModel
            {
                Users = _userManager.Users.ToList(),
                Recipes = _db.Recipes
                    .Where(recipe => recipe.Visibility == "Public" || recipe.ApplicationUser == user)
                    .OrderByDescending(recipe => recipe.Reviews.Average(r => r.Stars))
                    .ToList(),
                Reviews = _db.Reviews.ToList()
            };
            
            return View(vm);
        }
        
        public async Task<IActionResult> LunchGlutenfree()
        {
            var user = await _userManager.GetUserAsync(User);
            var vm = new RecipeUserViewModel
            {
                Users = _userManager.Users.ToList(),
                Recipes = _db.Recipes
                    .Where(recipe => recipe.Visibility == "Public" || recipe.ApplicationUser == user)
                    .OrderByDescending(recipe => recipe.Reviews.Average(r => r.Stars))
                    .ToList(),
                Reviews = _db.Reviews.ToList()
            };
            
            return View(vm);
        }
        
        public async Task<IActionResult> LunchGlutVeg()
        {
            var user = await _userManager.GetUserAsync(User);
            var vm = new RecipeUserViewModel
            {
                Users = _userManager.Users.ToList(),
                Recipes = _db.Recipes
                    .Where(recipe => recipe.Visibility == "Public" || recipe.ApplicationUser == user)
                    .OrderByDescending(recipe => recipe.Reviews.Average(r => r.Stars))
                    .ToList(),
                Reviews = _db.Reviews.ToList()
            };
            
            return View(vm);
        }
        
        public async Task<IActionResult> Dinner()
        {
            var user = await _userManager.GetUserAsync(User);
            var vm = new RecipeUserViewModel
            {
                Users = _userManager.Users.ToList(),
                Recipes = _db.Recipes
                    .Where(recipe => recipe.Visibility == "Public" || recipe.ApplicationUser == user)
                    .OrderByDescending(recipe => recipe.Reviews.Average(r => r.Stars))
                    .ToList(),
                Reviews = _db.Reviews.ToList()
            };
            return View(vm);
        }
        
        public async Task<IActionResult> DinnerGlutenfree()
        {
            var user = await _userManager.GetUserAsync(User);
            var vm = new RecipeUserViewModel
            {
                Users = _userManager.Users.ToList(),
                Recipes = _db.Recipes
                    .Where(recipe => recipe.Visibility == "Public" || recipe.ApplicationUser == user)
                    .OrderByDescending(recipe => recipe.Reviews.Average(r => r.Stars))
                    .ToList(),
                Reviews = _db.Reviews.ToList()
            };
            
            return View(vm);
        }
        
        public async Task<IActionResult> DinnerVegetarian()
        {
            var user = await _userManager.GetUserAsync(User);
            var vm = new RecipeUserViewModel
            {
                Users = _userManager.Users.ToList(),
                Recipes = _db.Recipes
                    .Where(recipe => recipe.Visibility == "Public" || recipe.ApplicationUser == user)
                    .OrderByDescending(recipe => recipe.Reviews.Average(r => r.Stars))
                    .ToList(),
                Reviews = _db.Reviews.ToList()
            };
            
            return View(vm);
        }
        
        public async Task<IActionResult> DinnerGlutVeg()
        {
            var user = await _userManager.GetUserAsync(User);
            var vm = new RecipeUserViewModel
            {
                Users = _userManager.Users.ToList(),
                Recipes = _db.Recipes
                    .Where(recipe => recipe.Visibility == "Public" || recipe.ApplicationUser == user)
                    .OrderByDescending(recipe => recipe.Reviews.Average(r => r.Stars))
                    .ToList(),
                Reviews = _db.Reviews.ToList()
            };
            
            return View(vm);
        }
        
        public async Task<IActionResult> Dessert()
        {
            var user = await _userManager.GetUserAsync(User);
            var vm = new RecipeUserViewModel
            {
                Users = _userManager.Users.ToList(),
                Recipes = _db.Recipes
                    .Where(recipe => recipe.Visibility == "Public" || recipe.ApplicationUser == user)
                    .OrderByDescending(recipe => recipe.Reviews.Average(r => r.Stars))
                    .ToList(),
                Reviews = _db.Reviews.ToList()
            };
            return View(vm);
        }
        
        public async Task<IActionResult> DessertVegetarian()
        {
            var user = await _userManager.GetUserAsync(User);
            var vm = new RecipeUserViewModel
            {
                Users = _userManager.Users.ToList(),
                Recipes = _db.Recipes
                    .Where(recipe => recipe.Visibility == "Public" || recipe.ApplicationUser == user)
                    .OrderByDescending(recipe => recipe.Reviews.Average(r => r.Stars))
                    .ToList(),
                Reviews = _db.Reviews.ToList()
            };
            
            return View(vm);
        }
        
        public async Task<IActionResult> DessertGlutenfree()
        {
            var user = await _userManager.GetUserAsync(User);
            var vm = new RecipeUserViewModel
            {
                Users = _userManager.Users.ToList(),
                Recipes = _db.Recipes
                    .Where(recipe => recipe.Visibility == "Public" || recipe.ApplicationUser == user)
                    .OrderByDescending(recipe => recipe.Reviews.Average(r => r.Stars))
                    .ToList(),
                Reviews = _db.Reviews.ToList()
            };
            
            return View(vm);
        }
        
        public async Task<IActionResult> DessertGlutVeg()
        {
            var user = await _userManager.GetUserAsync(User);
            var vm = new RecipeUserViewModel
            {
                Users = _userManager.Users.ToList(),
                Recipes = _db.Recipes
                    .Where(recipe => recipe.Visibility == "Public" || recipe.ApplicationUser == user)
                    .OrderByDescending(recipe => recipe.Reviews.Average(r => r.Stars))
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
    }
}