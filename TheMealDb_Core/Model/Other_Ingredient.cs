using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMealDb_Core.Model
{
    public class Other_Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageName { get; set; }

        [ForeignKey(nameof(Unit))]
        public int Unit_Id { get; set; }
        public Unit Unit { get; set; }

        public ICollection<Meal_Other_Ingredient> Meal_OtherIngredients { get; set; } = new HashSet<Meal_Other_Ingredient>();

    }
}
