using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMealDb_Core.Model
{
    public class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Other_Ingredient> Other_Ingredients { get; set; }=new HashSet<Other_Ingredient>();
    }
}
