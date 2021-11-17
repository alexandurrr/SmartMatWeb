using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using smartmat.Models;

namespace smartmat.Data
{
    public class ApplicationDbInitializer
    {
        public static void Initialize(ApplicationDbContext db, UserManager<ApplicationUser> um, RoleManager<IdentityRole> rm)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            
            
            // User Test data
            var user = new ApplicationUser
            {
                UserName = "user@uia.no", 
                Email = "user@uia.no", 
                Firstname = "Nicky", 
                Lastname = "Hansen",
                Bio = "",
                ActivityReminder = true,
                EmailConfirmed = true
            };
            um.CreateAsync(user, "Password1.").Wait();
            db.SaveChanges();
            
            
            // Recipe test data (while also adding a USER relation)
            foreach (var u in db.Users.Include(b => b.Recipes) )
            {
                u.Recipes.Add(new Recipe
                {
                    Title = "Eggerøre",
                    Introduction = "intro1",
                    Ingredients = "1 Egg, 2dl Melk, 400g Ost, 1stk Paprika, Salt, Pepper",
                    Category = "Frokost",
                    Glutenfree = false,
                    Vegetarian = true,
                    Instructions = "Kutt opp grønnsakene og damp i 10min. Stek laksen i panne i 10min. Smak til med salt og pepper.",
                    Nutrients = "20g protein, 50g karbohydrat, 10g fett",
                    Visibility = "Public",
                    ImagePath = "https://localhost:5001/recipeimages/test1.jpg"
                    
                });
                u.Recipes.Add(new Recipe
                {
                    Title = "Grønnsakssuppe",
                    Introduction = "intro2",
                    Ingredients = "1stk Brokkoli, 1/2 Blomkål, 4 Potet, 1 Sellerirot, 2 Gulrot, Salt, Pepper",
                    Category = "Middag",
                    Glutenfree = true,
                    Vegetarian = true,
                    Instructions = "Kutt opp grønnsakene og kok i 20min. Smak til med salt og pepper.",
                    Nutrients = "10g protein, 50g karbohydrat, 5g fett",
                    Visibility = "Public",
                    ImagePath = "https://localhost:5001/recipeimages/test2.jpg"
                    
                });
                u.Recipes.Add(new Recipe
                {
                    Title = "Stekt laks med grønnsakere",
                    Introduction = "intro3",
                    Ingredients = "400g Laks, 1 Brokkoli, 5 Potet, 3 Gulrot, 100g Smør, Salt, Pepper",
                    Category = "Middag",
                    Glutenfree = true,
                    Vegetarian = true,
                    Instructions = "Kutt opp grønnsakene og damp i 10min. Stek laksen i panne i 10min. Smak til med salt og pepper.",
                    Nutrients = "30g protein, 50g karbohydrat, 10g fett",
                    Visibility = "Public",
                    ImagePath = "https://images.matprat.no/hs2frzl2jk-jumbotron/large"
                    
                });
                u.Recipes.Add(new Recipe
                {
                    Title = "Kylling Nuggets",
                    Introduction = "intro3",
                    Ingredients = "600g kyllingfilet, 4ss majones, 3ss dijonsennep, 4dl brødkrum, 1ts paprikapulver, 1ts hvitløkspulver, 1ts oregano, 0.5ts salt, 0.25 ts pepper",
                    Category = "Middag",
                    Glutenfree = false,
                    Vegetarian = false,
                    Instructions = "Kutt opp grønnsakene og damp i 10min. Stek laksen i panne i 10min. Smak til med salt og pepper.",
                    Nutrients = "30g protein, 50g karbohydrat, 10g fett",
                    Visibility = "Public",
                    ImagePath = "https://localhost:5001/recipeimages/test3.jpg"
                    
                });
            }
            
            
            // Review Test Data
            for (int i = 1; i < 3; i++)
            {
                db.Add(new Review
                {
                    RecipeId = i,
                    ApplicationUserId = user.Id,
                    Stars = i+2,
                    Title = $"Test tittel {i}",
                    Description = $"Test beskrivelse {i}"
                });
                if (i % 2 == 0)
                {
                    db.Add(new Review
                    {
                        RecipeId = i,
                        ApplicationUserId = user.Id,
                        Stars = i+1,
                        Title = $"Test tittel {i}",
                        Description = $"Test beskrivelse {i}"
                    });
                }
            }
            
            
            db.SaveChanges();
        }
    }
}