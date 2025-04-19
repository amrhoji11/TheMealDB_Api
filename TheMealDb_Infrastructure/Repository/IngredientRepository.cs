using Landing.PL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMealDb_Core.Dtos.Area;
using TheMealDb_Core.Dtos.Ingredient;
using TheMealDb_Core.Interface;
using TheMealDb_Core.Model;
using TheMealDb_Infrastructure.Data;

namespace TheMealDb_Infrastructure.Repository
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly AppDbContext appDbContext;

        public IngredientRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<string> AddIngredient(AddIngredient dto)
        {
            var ingredient = new Ingredient
            {
                Name = dto.Name,
                ImageName = dto.ImageName
            };

            await appDbContext.Ingredients.AddAsync(ingredient);

            await appDbContext.SaveChangesAsync();
            return "Successfuly";
        }

        public async Task<string> DeleteIngredient(int Id)
        {

            var ingredient = await appDbContext.Ingredients.FindAsync(Id);
            if (ingredient.ImageName != null)
            {
                FileSetting.DeleteFile(ingredient.ImageName, "Images");
            }
            if (ingredient == null)
            {
                return "Ingredient is not found";
            }
            appDbContext.Ingredients.Remove(ingredient);
            await appDbContext.SaveChangesAsync();
            return "The Delete was completed successfully";
        }

        public async Task<string> UpdateIngredient(int id, AddIngredient dto)
        {
            var ingredient = await appDbContext.Ingredients.FindAsync(id);
            if (ingredient == null)
            {
                return "id is false or not found the ingredient";
            }


            ingredient.Name = dto.Name;
            ingredient.ImageName = dto.ImageName;
            await appDbContext.SaveChangesAsync();

            return "The Update was completed successfully";

        }

    }
}
