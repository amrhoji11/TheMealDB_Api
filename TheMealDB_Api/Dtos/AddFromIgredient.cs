namespace TheMealDB_Api.Dtos
{
    public class AddFromIgredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile? Image { get; set; }
    }
}
