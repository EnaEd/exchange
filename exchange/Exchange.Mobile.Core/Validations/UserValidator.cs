using Exchange.Mobile.Core.Models;
using FluentValidation;

namespace Exchange.Mobile.Core.Validations
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Email).NotNull().EmailAddress();
            RuleFor(user => user.Password).NotNull().Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
        }
    }
}
