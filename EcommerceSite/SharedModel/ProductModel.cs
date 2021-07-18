using FluentValidation;

namespace SharedModel
{
    public class ProductModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }
    }

    public class ProductModelValidator : AbstractValidator<ProductModel>
    {
        public ProductModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(1, 50);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(1024);
            RuleFor(x => x.Price).GreaterThan(0);
        }
    }
}
