using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMealDb_Core.Dtos.Area;
using TheMealDb_Core.Dtos.Unit;

namespace TheMealDb_Core.Interface
{
    public  interface IUnitRepository
    {
        Task<string> AddUnit(AddUnitdto dto);
        Task<string> UpdateUnit(UpdateUnitDto dto);

        Task<string> DeleteUnit(int Id);
    }
}
