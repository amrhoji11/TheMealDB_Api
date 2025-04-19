using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMealDb_Core.Model;

namespace TheMealDb_Infrastructure.Data
{
    public class AppDbContext :IdentityDbContext<User,IdentityRole<int>,int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Meal_Ingredient>().HasKey(i => new { i.MealId, i.IngredientId });
            builder.Entity<Meal_Other_Ingredient>().HasKey(a=> new {a.MealId,a.Other_IngredientId});
            
        }

        public DbSet<Area> Areas { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Other_Ingredient> Other_Ingredients { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<User> Users {  get; set; }
        public DbSet<Meal_Ingredient> Meal_Ingredients { get; set; }
        public DbSet<Meal_Other_Ingredient> Meal_OtherIngredients { get; set; }
    }
}
