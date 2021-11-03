using System;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
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
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            //testdata
            var recipes = new[]
            {
                new Recipe("Eggerøre", "intro1", "1 Egg, 2dl Melk, 400g Ost, 1stk Paprika, Salt, Pepper",
                    "Bland alt i en bolle og hiv det i panna.", "20g protein, 50g karbohydrat, 10g fett", "https://images.matprat.no/nr32p8psxu-jumbotron/large"),
                new Recipe("Grønnsakssuppe", "intro2", "1stk Brokkoli, 1/2 Blomkål, 4 Potet, 1 Sellerirot, 2 Gulrot, Salt, Pepper",
                    "Kutt opp grønnsakene og kok i 20min. Smak til med salt og pepper.", "10g protein, 50g karbohydrat, 5g fett", "https://images.matprat.no/fmg25b4e53-jumbotron/large"),
                new Recipe("Stekt laks med grønnsaker", "intro3", "400g Laks, 1 Brokkoli, 5 Potet, 3 Gulrot, 100g Smør, Salt, Pepper",
                    "Kutt opp grønnsakene og damp i 10min. Stek laksen i panne i 10min. Smak til med salt og pepper.",
                    "30g protein, 50g karbohydrat, 10g fett", "https://images.matprat.no/ur4kxnnmfa-jumbotron/large")
                
            };

            db.Recipes.AddRange(recipes);
            db.SaveChanges();
            
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
                        Image = $"{i}"
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