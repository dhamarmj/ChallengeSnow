using FluentValidation;

namespace ChallengeSnow.Models.Core
{
    // Fluent Validator for the Models with bussiness rules
    public class OrdersValidator : AbstractValidator<Order>
    {
        public OrdersValidator()
        {
            RuleFor(x => x.Item_NumberId).NotNull();
            RuleFor(x => x.Quantity).GreaterThan(0).NotNull();
        }
    }
}