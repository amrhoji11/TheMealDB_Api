using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMealDb_Core.Dtos.Other_Ingredient;

namespace TheMealDb_Core.Dtos.Meal
{
    public class MealsBy_Category_Area_OtherIngredient
    {
        public string  Name { get; set; }
        public string ImageName { get; set; }
        public string CategoryName{ get; set; }
        public string AreaName { get; set; }
        public List<Other_IngredientForMeal> Other_Ingredients { get; set; }
    }
}
