using FluentValidation;
using WebApi.Models.Orders;

namespace WebApi.Validators
{
    public class CreateOrderModelValidator : AbstractValidator<OrderModel>
    {
        public CreateOrderModelValidator()
        {
            RuleFor(x => x.Items)
                .NotEmpty()
                .WithMessage("Order must contain at least one item.");

            RuleForEach(x => x.Items).SetValidator(new OrderItemModelValidator());
        }
    }
}