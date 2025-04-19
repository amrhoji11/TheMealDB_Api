using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMealDb_Core.Model
{
    public class Area
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageName { get; set; }
        public ICollection<Meal> Meals { get; set; } = new HashSet<Meal>();

    }
}
