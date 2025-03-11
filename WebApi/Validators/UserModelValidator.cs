using FluentValidation;
using WebApi.Models.Users;

namespace WebApi.Validators
{
    public class UserModelValidator: AbstractValidator<UserModel>
    {
        public UserModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(1, 100).WithMessage("Name length should be between 1 and 200.");

            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Email is required.")
               .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Type)
    .           IsInEnum().WithMessage("Invalid user type.");

            RuleFor(x => x.AnnualSalary)
                .GreaterThanOrEqualTo(0).When(x => x.AnnualSalary.HasValue)
                .WithMessage("Annual salary must be greater than or equal to 0.");
        }
    }
}