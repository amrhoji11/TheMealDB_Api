using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMealDb_Core.Dtos.Area;
using TheMealDb_Core.Dtos.Ingredient;

namespace TheMealDb_Core.Interface
{
    public interface IIngredientRepository
    {
        Task<string> AddIngredient(AddIngredient dto);
        Task<string> UpdateIngredient(int id, AddIngredient dto);

        Task<string> DeleteIngredient(int Id);
    }
}
