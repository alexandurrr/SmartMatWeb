using System;
using Microsoft.AspNetCore.Identity;

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
        }
    }
}