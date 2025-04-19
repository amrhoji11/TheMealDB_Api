using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TheMealDb_Core.Dtos.Meal
{
    public class MealDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImageName { get; set; }
        public string Preparation_Method { get; set; }
        public int Category_Id { get; set; }
        public int  Area_Id { get; set; }
    }
}
