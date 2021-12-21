using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<FesaUser>
    {
        public UserValidator()
        {
            RuleFor(u => u.FirstName).MinimumLength(3).MaximumLength(100);
            RuleFor(u => u.LastName).MinimumLength(3).MaximumLength(100);
            RuleFor(u => u.Email).MinimumLength(10).MaximumLength(100);
        }
    }
}
