namespace Service.DTOs
{
    public class RecipeDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Photo { get; set; }
        public string PreparationMethod { get; set; }
        public string PreparationTime { get; set; }
        public decimal Cost { get; set; }
        public string Difficulty { get; set; }
        public int CategoryId { get; set; }
        public List<string>? Ingredients { get; set; }

        public RecipeDTO() { }

        public RecipeDTO(int id, string title, string photo, string preparationMethod, string preparationTime, decimal cost, string difficulty, int categoryId, List<string> ingredients)
        {
            Id = id;
            Title = title;
            Photo = photo;
            PreparationMethod = preparationMethod;
            PreparationTime = preparationTime;
            Cost = cost;
            Difficulty = difficulty;
            CategoryId = categoryId;
            Ingredients = ingredients;
        }
    }
}
