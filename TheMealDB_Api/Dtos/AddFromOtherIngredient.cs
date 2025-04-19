using System.ComponentModel.DataAnnotations.Schema;
using TheMealDb_Core.Model;

namespace TheMealDB_Api.Dtos
{
    public class AddFromOtherIngredient
    {
        public string Name { get; set; }
        public IFormFile? Image { get; set; }
        public int Unit_Id { get; set; }
    }
}
