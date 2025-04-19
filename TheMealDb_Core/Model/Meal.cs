using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TheMealDb_Core.Model
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImageName { get; set; }
        public string Preparation_Method { get; set; } // طريقة التحضير

        [ForeignKey(nameof(Category))]
        public int Category_Id { get; set; }
        public Category Category { get; set; }

        [ForeignKey(nameof(Area))]

        public int Area_Id { get; set; }
        public Area Area { get; set; }

        public ICollection<Meal_Ingredient> Meal_Ingredients { get; set; } = new HashSet<Meal_Ingredient>();
        public ICollection<Meal_Other_Ingredient> Meal_OtherIngredients { get; set; } = new HashSet<Meal_Other_Ingredient>();

    }
}
