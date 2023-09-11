using Domain.Entities.Base;
using Domain.Validations;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Recipe> Recipes { get; set; }

        public Category() { }
        public Category(string name, List<Recipe> recipe)
        {
            Validation(name);
            Recipes = recipe;
        }

        public Category(int id, string name)
        {
            DomainValidationException.When(id < 0, "Id deve ser informado");
            Id = id;
            Recipes = new List<Recipe>();
            Validation(name);
        }

        private void Validation(string name)
        {
            DomainValidationException.When(string.IsNullOrEmpty(name), "O nome deve ser informado");

            Name = name;
        }

        public override string? ToString()
        {
            return $"{Id}";
        }
    }
}
