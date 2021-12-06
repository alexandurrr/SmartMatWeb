using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using smartmat.Areas.Identity.Pages.Account.Manage;
using smartmat.Data;
using smartmat.Models;
using static Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal.IndexModel;

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
        // Get Index
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return RedirectToPage("Identity/Pages/Account/Manage/Index");
        }
        
        // GET Recipes
        [Authorize]
        public async Task<IActionResult> MyRecipes()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var recipes = _context.Recipes.Where(r => r.UserId == user.Id);
            var viewModel = new RecipeUserViewModel
            {
                Recipes = recipes.ToList(),
                Users = _userManager.Users.ToList(),
                Reviews = _context.Reviews.ToList()
            };
            return View(viewModel);
        }
        
        // GET Reviews
        [Authorize]
        public async Task<IActionResult> MyReviews()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var vm = new ReviewViewModel
            {
                MyReviews = _context.Reviews
                    .Where(r => r.ApplicationUserId == user.Id)
                    .ToList(),
                OthersReviews = _context.Reviews
                    .Where(r => r.Recipe.UserId == user.Id)
                    .ToList(),
                Recipes = _context.Recipes
                    .ToList()
            };

            return View(vm);
        }

        [Authorize]
        public async Task<IActionResult> MyFavorites()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            if (user.Favorites != null && user.Favorites.Length != 0)
            {
                string userFavorites = user.Favorites;
                userFavorites = userFavorites.Remove(userFavorites.Length - 1);
                var values = userFavorites.Split(',').Select(int.Parse).ToList();
                var recipes = _context.Recipes;
                var query = recipes.Where(r => values.Contains(r.Id));
                var viewModel = new RecipeUserViewModel
                {
                    Recipes = query.ToList(),
                    Users = _userManager.Users.ToList(),
                    Reviews = _context.Reviews.ToList()
                };
                return View(viewModel);
            }
            else
            {
                var viewModel = new RecipeUserViewModel { };
                return View(viewModel);
            }
            

        }
    }
}