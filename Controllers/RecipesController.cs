using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using smartmat.Data;
using smartmat.Models;

using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Twilio.TwiML;
using Twilio.AspNet.Mvc;

namespace smartmat.Controllers
{
    public class RecipesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RecipesController(ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager, 
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Recipes
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var recipes = _context.Recipes
                .Where(recipe => recipe.Visibility == "Public" || recipe.ApplicationUser == user)
                .OrderByDescending(recipe => recipe.Id)
                .ToList();
            
            var viewModel = new RecipeUserViewModel
            {
                Recipes = recipes.ToList(),
                Users = _userManager.Users.ToList(),
                Reviews = _context.Reviews.ToList()
            };
            return View(viewModel);
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

            var favorite = user != null && _context.Favorites
                .Any(favorite => favorite.ApplicationUserId == user.Id && favorite.RecipeId == id);

            ICollection<Recipe> r = new List<Recipe>{recipe};
            var viewModel = new RecipeUserViewModel
            {
                Recipes = r,
                Users = _userManager.Users.ToList(),
                Reviews = _context.Reviews.ToList(),
                Favorite = favorite
            };
            return View(viewModel);
        }

        [HttpPost]
        public async void Details(int id, bool favorite)
        {
            var user = await _userManager.GetUserAsync(User);
            if (favorite)
                _context.Favorites.Add(new FavoritesRecipeUser {RecipeId = id, ApplicationUserId = user.Id});
            else
            {
                var connection = _context.Favorites
                    .FirstOrDefault(c => c.ApplicationUserId == user.Id && c.RecipeId == id);
                if (connection != null)
                    _context.Favorites.Remove(connection);
            }
            await _context.SaveChangesAsync();
        }
        
        // GET: Recipes/SendSms/5
        public async Task<IActionResult> SendSms(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var recipe = await _context.Recipes
                .Where(recipe => recipe.Visibility == "Public" || recipe.Id == id)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (recipe == null)
            {
                return NotFound();
            }
            
            ICollection<Recipe> r = new List<Recipe>{recipe};

            var viewModel = new RecipeSmsViewModel
            {
                Recipes = r
            };
            return View(viewModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> SendSms(RecipeSmsViewModel sms)
        {
            var accountSid = "ACe8b8974db6fac84ebd80d84897e9acf4";
            var authToken = "9f24065c619953c08a82b18cd13813b2";
            TwilioClient.Init(accountSid,authToken);
            
            var to = sms.phoneNumber;
            var from = "+19894049330";

            var message = MessageResource.Create(
                to: to,
                from: from,
                body: sms.ingredientList);
            return Redirect("https://localhost:5001/Recipes");

        }

        // GET: Recipes/ByUser/5
        public IActionResult ByUser(string id)
        {
            var user = _userManager.Users.Where(u => u.Id == id);
            
            if (!user.Any())
            {
                return NotFound();
            }
            
            var recipe = _context.Recipes
                .Where(recipe => recipe.Visibility == "Public" && recipe.UserId == id)
                .OrderByDescending(recipe => recipe.Id);

            var viewModel = new RecipeUserViewModel
            {
                Recipes = recipe.ToList(),
                Users = user.ToList(),
                Reviews = _context.Reviews.ToList()
            };
            return View(viewModel);
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
        public async Task<IActionResult> Create(RecipeImageViewModel recipe)
        {
            // No changes needed here, refer to ImageCreate to add new attributes
            if (!ModelState.IsValid) return View(recipe);
            var res = await ImageCreate(recipe);
                
            if (res.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(recipe);
        }
        
        private async Task<RecipeImageViewModel> ImageCreate(RecipeImageViewModel recipe)
        {
            // new RecipeImage vm
            var res = new RecipeImageViewModel();
            
            // Check if model is not empty
            if (recipe == null) return res;
            
            // New recipe
            var recipes = new Recipe();
            var user = await _userManager.GetUserAsync(User);
                
            // Add non-file attributes and
            // Adding the foreign key to recipes
            // Too add new attribute from recipe model, simply follow example below:
            // For example; recipes.NewAttribute = recipe.NewAttribute
            recipes.UserId = user.Id;
            recipes.Title = recipe.Title;
            recipes.Time = recipe.Time;
            recipes.Ingredients = recipe.Ingredients;
            recipes.Instructions = recipe.Instructions;
            recipes.Introduction = recipe.Introduction;
            recipes.Nutrients = recipe.Nutrients;
            recipes.Visibility = recipe.Visibility;
            recipes.Glutenfree = recipe.Glutenfree;
            recipes.Category = recipe.Category;
            recipes.Vegetarian = recipe.Vegetarian;

            var image = recipe.Image;
                
            // Check if image is uploaded
            if (image != null)
            {
                // New file name using Guid
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                var deletePath =Path.Combine("wwwroot", "recipeimages", fileName);

                // Creating full file path string and appending the file name
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "recipeimages", fileName);
                    
                // open-create the file in a stream and copying the uploaded
                // Into the new file
                await using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
                    
                // Assigning the generated filePath
                recipes.ImageDelete = deletePath;
                recipes.ImagePath = $"{Request.Scheme}://{Request.Host}/recipeimages/{fileName}";

            }
            // Set the success flag and push data onto the db
            res.IsSuccess = true;
            _context.Add(recipes);
            await _context.SaveChangesAsync();

            return res;

            
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
            var model = new ImageEditViewModel()
            {
                Recipe = _context.Recipes.FirstOrDefault(s => s.Id == id)
            };
            if (recipe == null)
            {
                return NotFound();
            }
            if (recipe.ApplicationUser != user)
            {
                return RedirectToAction(nameof(NotAllowed));
            }
            return View(model);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, ImageEditViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            var recipeInDb = _context.Recipes.AsNoTracking()
                .Any(r => r.ApplicationUser == user && r.Id == id);
            var currentRecipe = _context.Recipes.AsNoTracking().FirstOrDefault(s => s.Id == model.Recipe.Id);
            var image = model.Image;

            if (id != model.Recipe.Id || !recipeInDb)
            {
                return RedirectToAction(nameof(NotAllowed));
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            try
            {
                if (image != null)
                {
                 System.IO.File.Delete(model.Recipe.ImageDelete);
                    
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
     
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "recipeimages", fileName);
                    
                    await using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }
                    
                    model.Recipe.ImagePath = $"{Request.Scheme}://{Request.Host}/recipeimages/{fileName}";

                }
                
                currentRecipe.Glutenfree = model.Recipe.Glutenfree;
                currentRecipe.Ingredients = model.Recipe.Ingredients;
                currentRecipe.Instructions = model.Recipe.Instructions;
                currentRecipe.Introduction = model.Recipe.Introduction;
                currentRecipe.Nutrients = model.Recipe.Nutrients;
                currentRecipe.Time = model.Recipe.Time;
                currentRecipe.Title = model.Recipe.Title;
                currentRecipe.Vegetarian = model.Recipe.Vegetarian;
                currentRecipe.Category = model.Recipe.Category;
                currentRecipe.Visibility = model.Recipe.Visibility;
                model.Recipe.ApplicationUser = user;
                _context.Update(model.Recipe);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(model.Recipe.Id))
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
