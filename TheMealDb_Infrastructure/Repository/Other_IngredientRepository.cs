using Landing.PL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMealDb_Core.Dtos.Other_Ingredient;
using TheMealDb_Core.Interface;
using TheMealDb_Core.Model;
using TheMealDb_Infrastructure.Data;

namespace TheMealDb_Infrastructure.Repository
{
    public class Other_IngredientRepository : IOther_IngredientRepository
    {
        private readonly AppDbContext appDbContext;

        public Other_IngredientRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<string> AddOther_Ingredient(AddOtherIngredient dto)
        {
            var other = new Other_Ingredient
            {
                Name = dto.Name,
                ImageName = dto.ImageName,
                Unit_Id = dto.Unit_Id
            };

            await appDbContext.Other_Ingredients.AddAsync(other);

            await appDbContext.SaveChangesAsync();
            return "Successfuly";
        }

        public async Task<string> DeleteOther_Ingredient(int Id)
        {

            var other = await appDbContext.Other_Ingredients.FindAsync(Id);
            if (other.ImageName != null)
            {
                FileSetting.DeleteFile(other.ImageName, "Images");
            }
            if (other == null)
            {
                return "Area is not found";
            }
            appDbContext.Other_Ingredients.Remove(other);
            await appDbContext.SaveChangesAsync();
            return "The Delete was completed successfully";
        }

        public async Task<string> UpdateOther_Ingredient(int id, AddOtherIngredient dto)
        {
            var other = await appDbContext.Other_Ingredients.FindAsync(id);
            if (other == null)
            {
                return "id is false or not found the Other_Ingredient";
            }


            other.Name = dto.Name;
            other.ImageName = dto.ImageName;
            other.Unit_Id = dto.Unit_Id;
            await appDbContext.SaveChangesAsync();

            return "The Update was completed successfully";
        }
    }
}
