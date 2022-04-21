using FluentValidation;

namespace ChallengeSnow.Models.Core
{
    // Fluent Validator for the Models with bussiness rules
    public class ItemsValidator : AbstractValidator<ItemBase>
    {
        public ItemsValidator()
        {
            RuleFor(x => x.Available_Quantity).GreaterThan(0).NotNull();
            RuleFor(x => x.Price).GreaterThan(0).NotNull();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}