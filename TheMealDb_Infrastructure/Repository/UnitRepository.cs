using Landing.PL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMealDb_Core.Dtos.Area;
using TheMealDb_Core.Dtos.Unit;
using TheMealDb_Core.Interface;
using TheMealDb_Core.Model;
using TheMealDb_Infrastructure.Data;

namespace TheMealDb_Infrastructure.Repository
{
    public class UnitRepository : IUnitRepository
    {
        private readonly AppDbContext appDbContext;

        public UnitRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<string> AddUnit(AddUnitdto dto)
        {
            var unit = new Unit
            {
                Name = dto.Name
                
            };

            await appDbContext.Units.AddAsync(unit);

            await appDbContext.SaveChangesAsync();
            return "Successfuly";

        }

       

        public async Task<string> DeleteUnit(int Id)
        {
            var unit = await appDbContext.Units.FindAsync(Id);
           
            if (unit == null)
            {
                return "Area is not found";
            }
            appDbContext.Units.Remove(unit);
            await appDbContext.SaveChangesAsync();
            return "The Delete was completed successfully";
        }

        public async Task<string> UpdateUnit(UpdateUnitDto dto)
        {
            var unit = await appDbContext.Units.FindAsync(dto.Id);
            if (unit == null)
            {
                return "id is false or not found the unit";
            }


            unit.Name = dto.Name;
           
            await appDbContext.SaveChangesAsync();

            return "The Update was completed successfully";
        }
    }
}
