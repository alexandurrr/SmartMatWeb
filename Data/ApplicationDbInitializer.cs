using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using smartmat.Models;

namespace smartmat.Data
{
    public class ApplicationDbInitializer
    {
        public static void Initialize(ApplicationDbContext db, UserManager<ApplicationUser> um, RoleManager<IdentityRole> rm)
        {
            // Delete and create the database
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
                        Title = $"{i}",
                        Introduction =  $"{i}",
                        Ingredients =  $"{i}",
                        Instructions =  $"{i}", 
                        Nutrients =  $"{i}"
                        
                    });
                }
            }
            db.SaveChanges();
        }
    }
}