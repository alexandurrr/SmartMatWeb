using System;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using smartmat.Models;

namespace smartmat.Data
{
    public class ApplicationDbInitializer
    {
        public static void Initialize(ApplicationDbContext db, UserManager<ApplicationUser> um, RoleManager<IdentityRole> rm)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            
            var user = new ApplicationUser
                { UserName = "user@uia.no", Email = "user@uia.no", EmailConfirmed = true };
            um.CreateAsync(user, "Password1.").Wait();
            db.SaveChanges();
            
            foreach (var u in db.Users.Include(b => b.Recipes) )
            {
                for (int i = 1; i < 3; i++)
                {
                    u.Recipes.Add(new Recipe
                    {
                        Title = $"Tittel {i}",
                        Introduction =  $"Introduksjon {i}",
                        Ingredients =  $"Ingredienser {i}",
                        Instructions =  $"Fremgangsmåte {i}", 
                        Nutrients =  $"Næringsstoffer {i}",
                        Visibility = $"Visibility {i}"
                    });
                }
            }
            
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
            }
            
            
            db.SaveChanges();
        }
    }
}