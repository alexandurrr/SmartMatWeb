using System;
using System.Net.Mime;
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
            //testdata
            var recipes = new[]
            {
                new Recipe("Eggerøre", "intro1", "Egg, Melk, Ost, Paprika, Salt, Pepper",
                    "Bland alt i en bolle og hiv det i panna.", "20g protein, 50g karbohydrat, 10g fett", "https://images.matprat.no/fmg25b4e53-jumbotron/large"),
                new Recipe("Grønnsakssuppe", "intro2", "Brokkoli, Blomkål, Potet, Sellerirot, Gulrot, Salt, Pepper",
                    "Kutt opp grønnsakene og kok i 20min. Smak til med salt og pepper.", "10g protein, 50g karbohydrat, 5g fett", "https://images.matprat.no/fmg25b4e53-jumbotron/large"),
                new Recipe("Stekt laks med grønnsaker", "intro3", "Laks, Brokkoli, Potet, Gulrot, Smør, Salt, Pepper",
                    "Kutt opp grønnsakene og damp i 10min. Stek laksen i panne i 10min. Smak til med salt og pepper.",
                    "30g protein, 50g karbohydrat, 10g fett", "https://images.matprat.no/fmg25b4e53-jumbotron/large")
                
            };

            db.Recipes.AddRange(recipes);
            db.SaveChanges();
            
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
                        Nutrients =  $"{i}",
                        Image = $"{i}"
                        
                    });
                }
            }
            db.SaveChanges();
        }
    }
}