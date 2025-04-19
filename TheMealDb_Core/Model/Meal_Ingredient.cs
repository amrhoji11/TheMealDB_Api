using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMealDb_Core.Model
{
    public class Meal_Ingredient
    {
        [ForeignKey(nameof(Meal))]
        public int MealId { get; set; }

        [ForeignKey(nameof(Ingredient))]
        public int IngredientId { get; set; }

        public Meal Meal { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
