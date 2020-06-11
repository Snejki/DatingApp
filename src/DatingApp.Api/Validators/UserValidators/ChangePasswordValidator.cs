namespace DatingApp.Api.Validators.UserValidators
{
    using DatingApp.Infrastructure.Commands.UserCommands;
    using DatingApp.Shared.ValidationRules;
    using FluentValidation;

    public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.OldPassword)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.NewPassword)
                .NotNull()
                .NotEmpty()
                .Equal(x => x.OldPassword)
                .MinimumLength(UserValidationRules.PASSWORD_MIN_LENGTH)
                .MaximumLength(UserValidationRules.PASSWORD_MAX_LENGTH);
        }
    }
}
