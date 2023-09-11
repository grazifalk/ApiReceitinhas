using FluentValidation;

namespace Service.DTOs
{
    public class CategoryDTOValidator : AbstractValidator<CategoryDTO>
    {
        public CategoryDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name must be informed");
        }
    }
}
