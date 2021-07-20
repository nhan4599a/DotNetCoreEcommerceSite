using FluentValidation;

namespace SharedModel
{
    public class ProductInputModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public int CategoryId { get; set; }
    }

    public class ProductInputModelValidator : AbstractValidator<ProductInputModel>
    {
        public ProductInputModelValidator()
        {
            RuleFor(x => x.Name).Length(1, 50);
            RuleFor(x => x.Description).MaximumLength(1024);
            RuleFor(x => x.Price).GreaterThan(0);
        }
    }
}
