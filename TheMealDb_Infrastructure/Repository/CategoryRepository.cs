using Landing.PL.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMealDb_Core.Dtos.Area;
using TheMealDb_Core.Dtos.Category;
using TheMealDb_Core.Interface;
using TheMealDb_Core.Model;
using TheMealDb_Infrastructure.Data;

namespace TheMealDb_Infrastructure.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext appDbContext;

        public CategoryRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<string> AddCategory(AddCategory dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description,
                ImageName = dto.ImageName
            };
            var result = await appDbContext.Categories.AddAsync(category);
            await appDbContext.SaveChangesAsync();
            return "The Add was completed successfully";
        }

        public async Task<IEnumerable<AddCategory>> AllCategory()
        {
            var category = await appDbContext.Categories.ToListAsync();
            var AllCategory = category.Select(i => new AddCategory
            {
                Name = i.Name,
                Description = i.Description,
                ImageName = i.ImageName
            }).ToList();

            return AllCategory;
        }

        public async Task<string> DeleteCategory(int Id)
        {

            var category = await appDbContext.Categories.FindAsync(Id);

            if (category == null)
            {
                return "category is not found";
            }

            if (category.ImageName != null)
            {
                FileSetting.DeleteFile(category.ImageName, "Images");
            }
            
            appDbContext.Categories.Remove(category);
            await appDbContext.SaveChangesAsync();
            return "The Delete was completed successfully";
        }

        public async Task<string> UpdateCategory(int id, AddCategory dto)
        {
            var category = await appDbContext.Categories.FindAsync(id);
            if (category == null)
            {
                return "id is false or not found the category";
            }


            category.Name = dto.Name;
            category.Description = dto.Description;
            category.ImageName = dto.ImageName;
            await appDbContext.SaveChangesAsync();

            return "The Update was completed successfully";

        }
    }
}
