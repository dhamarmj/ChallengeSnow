using FluentValidation;

namespace ChallengeSnow.Models.Core
{
    public class ItemsValidator : AbstractValidator<Item>
    {
        public ItemsValidator()
        {
            RuleFor(x => x.Available_Quantity).GreaterThan(0).NotNull();
            RuleFor(x => x.Price).GreaterThan(0).NotNull();
        }
    }
}