namespace DatingApp.Api.Validators.UserValidators
{
    using DatingApp.Infrastructure.Commands.UserCommands;
    using FluentValidation;

    public class RegisteruserValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisteruserValidator()
        {
            RuleFor(u => u.Firstname)
                .NotNull().WithMessage("Firstname can not be null")
                .NotEmpty().WithMessage("Firstname can not be empty");
        }
    }
}
