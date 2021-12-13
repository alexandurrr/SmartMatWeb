using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using smartmat.Models;
namespace smartmat.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<FavoritesRecipeUser> Favorites  { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
	
            builder.Entity<FavoritesRecipeUser>()
                .HasKey(t => new { t.RecipeId, t.ApplicationUserId });
		
            builder.Entity<FavoritesRecipeUser>()
                .HasOne(ab => ab.Recipe)
                .WithMany(ab => ab.Favorites)
                .HasForeignKey(ab => ab.RecipeId);
	
            builder.Entity<FavoritesRecipeUser>()
                .HasOne(ab => ab.ApplicationUser)
                .WithMany(ab => ab.Favorites)
                .HasForeignKey(ab => ab.ApplicationUserId);
        }
    }
}