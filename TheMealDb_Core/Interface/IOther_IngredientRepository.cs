using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMealDb_Core.Dtos.Area;
using TheMealDb_Core.Dtos.Other_Ingredient;

namespace TheMealDb_Core.Interface
{
    public interface IOther_IngredientRepository
    {
        Task<string> AddOther_Ingredient(AddOtherIngredient dto);
        Task<string> UpdateOther_Ingredient(int id, AddOtherIngredient dto);

        Task<string> DeleteOther_Ingredient(int Id);
    }
}
