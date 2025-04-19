using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMealDb_Core.Dtos.Area;
using TheMealDb_Core.Dtos.Category;

namespace TheMealDb_Core.Interface
{
    public interface ICategoryRepository
    {
        Task<string> AddCategory(AddCategory dto);
        Task<string> UpdateCategory(int id, AddCategory dto);

        Task<string> DeleteCategory(int id);
        Task<IEnumerable<AddCategory>> AllCategory();
    }
}
