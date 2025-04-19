using Landing.PL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMealDb_Core.Dtos.Area;
using TheMealDb_Core.Interface;
using TheMealDb_Core.Model;
using TheMealDb_Infrastructure.Data;

namespace TheMealDb_Infrastructure.Repository
{
    public class AreaRepository : IAreaRepository
    {
        private readonly AppDbContext appDbContext;

        public AreaRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<string> AddArea(AreaDto dto)
        {
            var area = new Area
            {
                Name = dto.Name,
                ImageName = dto.ImageName
            };

           await appDbContext.Areas.AddAsync(area);
           
            await appDbContext.SaveChangesAsync();
            return "Successfuly";
            
        }

        public async Task<string> DeleteArea(int Id)
        {

            var area = await appDbContext.Areas.FindAsync(Id);
            if (area.ImageName!= null)
            {
                FileSetting.DeleteFile(area.ImageName,"Images");
            }
            if (area == null)
            {
                return "Area is not found";
            }
             appDbContext.Areas.Remove(area);
            await appDbContext.SaveChangesAsync();
            return "The Delete was completed successfully";
        }

        public async Task<string> UpdateArea(int id,AreaDto dto)
        {
           var area = await appDbContext.Areas.FindAsync(id);
            if (area == null)
            {
                return "id is false or not found the area";
            }


            area.Name = dto.Name;
            area.ImageName = dto.ImageName;
            await appDbContext.SaveChangesAsync();

            return "The Update was completed successfully";

        }
    }
}
