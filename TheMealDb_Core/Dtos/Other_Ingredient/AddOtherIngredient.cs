using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMealDb_Core.Dtos.Other_Ingredient
{
    public class AddOtherIngredient { 

    public string Name { get; set; }
    public string? ImageName { get; set; }
    public int Unit_Id { get; set; }
}
}
