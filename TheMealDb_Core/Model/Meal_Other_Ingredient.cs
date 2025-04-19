using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMealDb_Core.Model
{
    public class Meal_Other_Ingredient
    {
       [ForeignKey(nameof(Meal))]
        public int MealId { get; set; }

        [ForeignKey(nameof(Other_Ingredient))]
        public int Other_IngredientId { get; set; }

        public Meal Meal { get; set; }
        public Other_Ingredient Other_Ingredient { get; set; }

        public double Quantity { get; set; }
    }
}
