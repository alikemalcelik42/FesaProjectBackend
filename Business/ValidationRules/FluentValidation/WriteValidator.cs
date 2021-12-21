using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class WriteValidator : AbstractValidator<Write>
    {
        public WriteValidator()
        {
            RuleFor(w => w.Title).MinimumLength(10).MaximumLength(100);
            RuleFor(w => w.Content).MinimumLength(500).MaximumLength(4000);
        }
    }
}
