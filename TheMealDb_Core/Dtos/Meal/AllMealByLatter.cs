using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMealDb_Core.Dtos.Meal
{
    public class AllMealByLatter
    {
        public string Name { get; set; }
        public string ImageName { get; set; }
        public string CategoryName { get; set; }
        public string  AreaName { get; set; }
        public string Preparation_Method { get; set; }
    }
}
