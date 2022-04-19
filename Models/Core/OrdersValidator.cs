using FluentValidation;

namespace ChallengeSnow.Models.Core
{
    public class OrdersValidator : AbstractValidator<Order>
    {
        public OrdersValidator()
        {
            //RuleFor(x=>x.Date_Created).LessThanOrEqualTo(System.DateTime.Now).NotNull();
            RuleFor(x=>x.Item_Number).NotNull();
            RuleFor(x=>x.Quantity).GreaterThan(0).NotNull();
        }
    }
}