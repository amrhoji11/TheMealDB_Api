namespace TheMealDB_Api.Dtos
{
    public class UpdateFromCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
    }
}
