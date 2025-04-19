using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMealDb_Core.Model
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageName { get; set; }
        public ICollection<Meal_Ingredient> Meal_Ingredients { get; set; } = new HashSet<Meal_Ingredient>();
    }
}
