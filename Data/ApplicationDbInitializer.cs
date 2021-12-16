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
                    Introduction = "Enkel eggerøre med ost. Her kan du putte oppi hva enn du har tilgjengelig av" +
                                   "grønnsaker.",
                    Ingredients = "3 Egg, 2dl Melk, 400g Ost, 1stk Paprika, Salt, Pepper",
                    Category = "Frokost",
                    Glutenfree = true,
                    Vegetarian = true,
                    Instructions = "Kutt opp grønnsakene, rasp osten og bland i en røre med eggene og melken." +
                                   "Ha oppi ca en halv teskje salt." +
                                   "Hell røren i en stekepanne og stek i 5 minutter. Gjerne server med noe salat" +
                                   "på siden.",
                    Nutrients = "20g protein, 60g karbohydrat, 20g fett",
                    Visibility = "Public",
                    Time = 20,
                    ImagePath = "https://localhost:5001/recipeimages/test8.jpg"
                    
                });
                u.Recipes.Add(new Recipe
                {
                    Title = "Grønnsakssuppe",
                    Introduction = "Kjempe god suppe for kalde vinterdager. Velg de grønnsakene du liker best.",
                    Ingredients = "1stk Brokkoli, 1/2 Blomkål, 4 Potet, 1 Sellerirot, 2 Gulrot, Purre, Buljong, Salt, Pepper",
                    Category = "Middag",
                    Glutenfree = true,
                    Vegetarian = true,
                    Instructions = "Kutt opp grønnsakene og kok i 20min. Smak til med salt og pepper.",
                    Nutrients = "10g protein, 85g karbohydrat, 5g fett",
                    Visibility = "Public",
                    Time = 45,
                    ImagePath = "https://localhost:5001/recipeimages/test7.jpg"
                    
                });
                u.Recipes.Add(new Recipe
                {
                    Title = "Stekt laks med grønnsaker",
                    Introduction = "Enkel og rask oppskrift, veldig god med fersk fisk!",
                    Ingredients = "400g Laks, 1 Brokkoli, 5 Potet, 3 Gulrot, 100g Smør, Salt, Pepper",
                    Category = "Middag",
                    Glutenfree = true,
                    Vegetarian = false,
                    Instructions = "Kok potetene. Kutt opp grønnsakene og damp i 10min. Stek laksen i panne i 10min. Smak til med " +
                                   "salt og pepper.",
                    Nutrients = "40g protein, 50g karbohydrat, 10g fett",
                    Visibility = "Public",
                    Time = 30,
                    ImagePath = "https://localhost:5001/recipeimages/test6.jpg"
                    
                });
                u.Recipes.Add(new Recipe
                {
                    Title = "Kylling Nuggets",
                    Introduction = "Verdens beste hjemmelaget kyllingnuggets!",
                    Ingredients = "600g kyllingfilet, 4ss majones, 3ss dijonsennep, brødsmuler, 4dl hvetemel, " +
                                  "1 egg, 1ts paprikapulver, 1ts hvitløkspulver, 1ts oregano, 0.5ts salt, 0.25 ts pepper",
                    Category = "Middag",
                    Glutenfree = false,
                    Vegetarian = false,
                    Instructions = "Rull kyllingbitene i blanding av egg, hvete og brødsmuler. Krydre slik du vil ha det. " +
                                   "Stek i ovnen til de er sprø.",
                    Nutrients = "45g protein, 35g karbohydrat, 20g fett",
                    Visibility = "Public",
                    Time = 50,
                    ImagePath = "https://localhost:5001/recipeimages/test4.jpg"
                    
                });
                
                u.Recipes.Add(new Recipe
                {
                    Title = "Donuts",
                    Introduction = "Amerikansk smultering som smaker kjempe godt! Moro å lage med familien, her er det " +
                                   "bare å være kreativ med pynten!",
                    Ingredients = "11dl hvetemel, 3ss sukker, 3 egg, 2ts vaniljesukker, 2L rapsolje, 3dl melk, 1 pakke gjær",
                    Category = "Dessert",
                    Glutenfree = false,
                    Vegetarian = true,
                    Instructions = "Rør ut gjæren i en lunken blanding av smør og melk. Tilsett hvetemel, " +
                                   "eggeplommer, sukker og vaniljesukker. Kjevl ut stikk ut runde formeer med f.eks et " +
                                   "glass. La heve i ca. 1 time. Varm opp olje i en stekepanne. La donutsene steke litt " +
                                   "på hver side til de har en gyllen farge. Pynt som du vil!",
                    Nutrients = "5g protein, 80g karbohydrat, 5g fett",
                    Visibility = "Public",
                    Time = 120,
                    ImagePath = "https://localhost:5001/recipeimages/test5.jpg"
                    
                });
                
                u.Recipes.Add(new Recipe
                {
                    Title = "Havregrøt",
                    Introduction = "Kjapp og varm frokost eller mellommåltid. Topp med det du vil!",
                    Ingredients = "1.5 dl Havregryn, 1ts kanel, 1/4 ts salt, nøtter, blåbær, 3dl melk, sukker",
                    Category = "Frokost",
                    Glutenfree = false,
                    Vegetarian = true,
                    Instructions = "Rør sammen havregryn, melk(eller vann) og litt salt i en kjele og varm opp. " +
                                   "La koke i ca. 5 minutter. Topp med blåbær, kanel, nøtter og litt sukker om ønskelig.",
                    Nutrients = "15g protein, 80g karbohydrat, 5g fett",
                    Visibility = "Public",
                    Time = 10,
                    ImagePath = "https://localhost:5001/recipeimages/test10.jpg"
                    
                });
            }
            
            
            // Review Test Data
            for (int i = 1; i < 4; i++)
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