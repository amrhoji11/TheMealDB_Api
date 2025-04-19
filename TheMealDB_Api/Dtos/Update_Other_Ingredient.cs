namespace TheMealDB_Api.Dtos
{
    public class Update_Other_Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile? Image { get; set; }
        public int Unit_Id { get; set; }
    }
}
