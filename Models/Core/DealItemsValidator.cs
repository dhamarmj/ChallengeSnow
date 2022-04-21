using FluentValidation;

namespace ChallengeSnow.Models.Core
{
    // Fluent Validator for the Models with bussiness rules
    public class DealItemsValidator : AbstractValidator<Deal_Item>
    {
        public DealItemsValidator()
        {
            RuleFor(x => x.Available_Quantity).GreaterThan(0).NotNull();
            RuleFor(x => x.Discount).GreaterThan(0).NotNull();
            RuleFor(x => x.Price).GreaterThan(0).NotNull();
            RuleFor(x => x.Start_Date).NotNull();
            RuleFor(x => x.End_Date).NotNull();
        }
    }
}