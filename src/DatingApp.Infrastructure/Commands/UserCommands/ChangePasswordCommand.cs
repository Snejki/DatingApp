namespace DatingApp.Infrastructure.Commands.UserCommands
{
    using MediatR;

    public class ChangePasswordCommand : AuthCommand, IRequest
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
