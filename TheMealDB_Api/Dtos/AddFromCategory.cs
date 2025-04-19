namespace TheMealDB_Api.Dtos
{
    public class AddFromCategory
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
    }
}
