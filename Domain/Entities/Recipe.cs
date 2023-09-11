using Domain.Entities.Base;
using Domain.Validations;

namespace Domain.Entities
{
    public sealed class Recipe : BaseEntity
    {
        public string Title { get; set; }
        public string Photo { get; set; }
        public string PreparationMethod { get; set; }
        public string PreparationTime { get; set; }
        public decimal Cost { get; set; }
        public string Difficulty { get; set; }
        public int CategoryId { get; set; } // Chave estrangeira para a categoria
        public Category Category { get; set; } // Propriedade de navegação para a categoria
        public List<string>? Ingredients { get; set; }

        public Recipe() { }

        public Recipe(string title, string photo, string preparationMethod, string preparationTime, decimal cost, string difficulty)
        {
            Validation(title, photo, preparationMethod, preparationTime, cost, difficulty);
        }

        public Recipe(int id, string title, string photo, string preparationMethod, string preparationTime, decimal cost, string difficulty)
        {
            DomainValidationException.When(id < 0, "Id deve ser maior do que zero");
            Id = id;
            Validation(title, photo, preparationMethod, preparationTime, cost, difficulty);
        }

        private void Validation(string title, string photo, string preparationMethod, string preparationTime, decimal cost, string difficulty)
        {
            DomainValidationException.When(string.IsNullOrEmpty(title), "O título deve ser preenchido corretamente");
            DomainValidationException.When(string.IsNullOrEmpty(photo), "A url da imagem deve ser informada corretamente");
            DomainValidationException.When(string.IsNullOrEmpty(preparationMethod), "O modo de preparo deve ser informado");
            DomainValidationException.When(string.IsNullOrEmpty(preparationTime), "O tempo de preparo deve ser informado");
            DomainValidationException.When(cost <= 0, "O valor deve ser informado");
            DomainValidationException.When(string.IsNullOrEmpty(difficulty), "A dificuldade deve ser informada");

            Title = title;
            Photo = photo;
            PreparationMethod = preparationMethod;
            PreparationTime = preparationTime;
            Cost = cost;
            Difficulty = difficulty;
        }

        public override string? ToString()
        {
            return $"{Id}";
        }
    }
}