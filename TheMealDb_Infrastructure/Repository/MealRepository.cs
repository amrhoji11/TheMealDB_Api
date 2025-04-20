using Landing.PL.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMealDb_Core.Dtos.Area;
using TheMealDb_Core.Dtos.Meal;
using TheMealDb_Core.Dtos.Other_Ingredient;
using TheMealDb_Core.Interface;
using TheMealDb_Core.Model;
using TheMealDb_Infrastructure.Data;

namespace TheMealDb_Infrastructure.Repository
{
    public class MealRepository : IMealRepository
    {
        private readonly Data.AppDbContext  appDbContext;

        public MealRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

       

        public async Task<string> AddMeals(MealDto dto)
        {
            var Meal = new Meal
            {
                Name = dto.Name,
                Description = dto.Description,
                ImageName = dto.ImageName,
                Preparation_Method = dto.Preparation_Method,
                Category_Id=dto.Category_Id,
                Area_Id=dto.Area_Id
            };

            await appDbContext.Meals.AddAsync(Meal);
            await appDbContext.SaveChangesAsync();
            return "The Meal is Added";
            

        }

        public async Task<IEnumerable<AllMealByLatter>> AllMealByLatters(string a)
        {
           a=a.ToLower();
            var model = await appDbContext.Meals.Include(i => i.Area).Include(b => b.Category).Where(meal=>meal.Name.ToLower().Contains(a)).ToListAsync();
            var Allmeal = model.Select(i => new AllMealByLatter
            {
                Name=i.Name,
                ImageName=i.ImageName,
                Preparation_Method=i.Preparation_Method,
                CategoryName=i.Category.Name,
                AreaName=i.Area.Name

            }).ToList();
            return Allmeal;
          
        }

        public async Task<string> DeleteMeal(int Id)
        {

            var meal = await appDbContext.Meals.FindAsync(Id);
            /*if (meal.ImageName != null)
            {
                FileSetting.DeleteFile(meal.ImageName, "Images");
            }*/
            if (meal == null)
            {
                return "meal is not found";
            }
            appDbContext.Meals.Remove(meal);
            await appDbContext.SaveChangesAsync();
            return "The Delete was completed successfully";
        }

        public async Task<IEnumerable<MealsBy_Category_Area_OtherIngredient>> MealsByAllpossibilities(int pageIndex ,int pageSize)
        {
            var model = await appDbContext.Meals
         .Include(a => a.Category)
         .Include(a => a.Area)
         .Include(a => a.Meal_OtherIngredients)
             .ThenInclude(m => m.Other_Ingredient)
         .Skip((pageIndex - 1) * pageSize)
         .Take(pageSize)
         .Select(i => new MealsBy_Category_Area_OtherIngredient
         {
             Name = i.Name,
             ImageName = i.ImageName,
             CategoryName = i.Category.Name,
             AreaName = i.Area.Name,
             Other_Ingredients = i.Meal_OtherIngredients.Select(a => new Other_IngredientForMeal
             {
                 Name = a.Other_Ingredient.Name,
                 Quantity = a.Quantity
             }).ToList()
         }).ToListAsync();
            return model;
        }

        public async Task<IEnumerable<MealByIngredient>> MealsByCategory(string a)
        {
            a = a.ToLower();
            var meals = await appDbContext.Meals.Include(i => i.Category).Where(b=>b.Category.Name.ToLower()==a).ToListAsync();
            var MealsByCategory = meals.Select(i => new MealByIngredient
            {
                Name = i.Name,
                ImageName = i.ImageName
            }).ToList();
            return MealsByCategory;
            
        }

        public async Task<IEnumerable<MealByIngredient>> MealsByIngredient(string a)
        {
            a= a.ToLower();
           
           var meals = await appDbContext.Meals.Include(b=>b.Meal_Ingredients).Where(d=>d.Meal_Ingredients.Any(o=>o.Ingredient.Name.ToLower()==a)).ToListAsync();
            if (meals == null)
            {
                return null;
            }
            var MealsByIngredient = meals.Select(i => new MealByIngredient
            {
                Name= i.Name,
                ImageName = i.ImageName  
                
            }).ToList();
            return MealsByIngredient;
        }

        public async Task<IEnumerable<AllMealByLatter>> RandomMeals(int a)
        {
            Random random = new Random();
            var meal = await appDbContext.Meals.Include(i => i.Area).Include(i => i.Category).ToListAsync();
            var randomMeal = meal.OrderBy(m=>random.Next()).Take(a).ToList();
            var AllMealRandom = randomMeal.Select(i => new AllMealByLatter
            {
                Name = i.Name,
                ImageName=i.ImageName,
                Preparation_Method = i.Preparation_Method,
                AreaName=i.Area.Name,
                CategoryName=i.Category.Name


            }).ToList();

            return AllMealRandom;

        }

        public async Task<string> UpdateMeal(int id, MealDto dto)
        {
            var meal = await appDbContext.Meals.FindAsync(id);
            if (meal == null)
            {
                return "id is false or not found the meal";
            }


            meal.Name = dto.Name;
            meal.ImageName = dto.ImageName;
            meal.Description = dto.Description;
            meal.Preparation_Method = dto.Preparation_Method;
            meal.Category_Id = dto.Category_Id;
            meal.Area_Id = dto.Area_Id;

            await appDbContext.SaveChangesAsync();

            return "The Update was completed successfully";

        }

        
    }
}
