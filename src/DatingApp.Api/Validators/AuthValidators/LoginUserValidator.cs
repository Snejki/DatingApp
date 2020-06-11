namespace DatingApp.Api.Validators.AuthValidators
{
    using DatingApp.Infrastructure.Queries.AuthQueries;
    using FluentValidation;

    public class LoginUserValidator : AbstractValidator<LoginUserQuery>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.Username)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty();
        }
    }
}
