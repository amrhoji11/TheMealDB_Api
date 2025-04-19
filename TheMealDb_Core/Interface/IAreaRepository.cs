using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMealDb_Core.Dtos.Area;

namespace TheMealDb_Core.Interface
{
    public interface IAreaRepository
    {
        Task<string> AddArea(AreaDto dto);
        Task<string> UpdateArea(int id,AreaDto dto);

        Task<string> DeleteArea(int Id);

    }
}
