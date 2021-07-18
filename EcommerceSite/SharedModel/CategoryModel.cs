using FluentValidation;

namespace SharedModel
{
    public class CategoryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class CategoryModelValidator : AbstractValidator<CategoryModel>
    {
        public CategoryModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        }
    }
}
