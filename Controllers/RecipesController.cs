using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using smartmat.Data;
using smartmat.Models;

namespace smartmat.Controllers
{
    public class RecipesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RecipesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Recipes
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var recipes = _context.Recipes
                .Where(recipe => recipe.Visibility == "Public" || recipe.ApplicationUser == user)
                .ToList(); 
            return View(recipes);
        }

        // GET: Recipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            var recipe = await _context.Recipes
                .Where(recipe => recipe.Visibility == "Public" || recipe.ApplicationUser == user)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // GET: Recipes/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Title,Introduction,Ingredients,Category,Vegetarian,Glutenfree,Instructions,Nutrients,Visibility,Image")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                // Adding the foreign key to recipes
                var user = await _userManager.GetUserAsync(User);
                recipe.UserId = user.Id;
                
                _context.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        // GET: Recipes/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            if (recipe.ApplicationUser != user)
            {
                return RedirectToAction(nameof(NotAllowed));
            }
            return View(recipe);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Introduction,Ingredients,Category,Vegetarian,Glutenfree,Instructions,Nutrients,Visibility,Image")] Recipe recipe)
        {
            var user = await _userManager.GetUserAsync(User);
            var recipeInDb = _context.Recipes
                .Any(r => r.ApplicationUser == user && r.Id == id);
            if (id != recipe.Id || !recipeInDb)
            {
                return RedirectToAction(nameof(NotAllowed));
            }

            if (!ModelState.IsValid)
            {
                return View(recipe);
            }
            
            try
            {
                recipe.ApplicationUser = user;
                _context.Update(recipe);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(recipe.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
            
        }

        // GET: Recipes/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var user = await _userManager.GetUserAsync(User);
            var recipe = await _context.Recipes
                .Where(recipe => recipe.ApplicationUser == user)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var recipe = await _context.Recipes
                .FindAsync(id);
            if (recipe.ApplicationUser != user)
            {
                return RedirectToAction(nameof(NotAllowed));
            }
            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }
        
        // Not Allowed
        [HttpGet]
        public IActionResult NotAllowed()
        {
            return View();
        }
    }
}
