using Core.Dtos;
using FluentValidation;
using WebApi.Models.Orders;

namespace WebApi.Validators
{
    public class OrderItemModelValidator : AbstractValidator<OrderItemModel>
    {
        public OrderItemModelValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage("ProductId is required.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero.");
        }
    }
}