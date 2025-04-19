using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMealDb_Core.Dtos.Area;
using TheMealDb_Core.Dtos.Meal;


namespace TheMealDb_Core.Interface
{
    public interface IMealRepository
    {
        Task<string> AddMeals(MealDto dto);
        Task<string> UpdateMeal(int id, MealDto dto);

        Task<string> DeleteMeal(int Id);
        Task<IEnumerable<AllMealByLatter>> AllMealByLatters(string a);
        Task<IEnumerable<AllMealByLatter>> RandomMeals(int a);
        Task<IEnumerable<MealByIngredient>> MealsByIngredient(string a);
        Task<IEnumerable<MealByIngredient>> MealsByCategory(string a);
        Task<IEnumerable<MealsBy_Category_Area_OtherIngredient>> MealsByAllpossibilities(int pageIndex , int PageSize);

    }
}
