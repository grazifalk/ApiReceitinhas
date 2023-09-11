using FluentValidation;

namespace Service.DTOs
{
    public class RecipeDTOValidator : AbstractValidator<RecipeDTO>
    {
        public RecipeDTOValidator()
        {
            RuleFor(x => x.Title).NotEmpty().NotNull().WithMessage("Title must be informed");
            RuleFor(x => x.Photo).NotEmpty().NotNull().WithMessage("Photo must be informed");
            RuleFor(x => x.PreparationMethod).NotEmpty().NotNull().WithMessage("Preparation Method must be informed");
            RuleFor(x => x.PreparationTime).NotEmpty().NotNull().WithMessage("Preparation Time must be informed");
            RuleFor(x => x.Cost).NotEmpty().NotNull().WithMessage("Cost must be informed");
            RuleFor(x => x.Difficulty).NotEmpty().NotNull().WithMessage("Difficulty must be informed");
            RuleFor(x => x.CategoryId).NotEmpty().NotNull().GreaterThan(0).WithMessage("Category must be selected");

            RuleForEach(x => x.Ingredients).NotEmpty().NotNull().WithMessage("Ingredients must be selected");
        }
    }
}
