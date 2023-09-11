namespace Service.DTOs
{
    public class ResponseRecipeDTO
    {
        public string Title { get; set; }
        public string Photo { get; set; }
        public string PreparationMethod { get; set; }
        public string PreparationTime { get; set; }
        public decimal Cost { get; set; }
        public string Difficulty { get; set; }
        public int CategoryId { get; set; }
        public List<string>?Ingredients { get; set; }
    }
}
