using DatingApp.Infrastructure.DTOs.AuthDTOs;
using MediatR;

namespace DatingApp.Infrastructure.Queries.AuthQueries
{
    public class LoginUserQuery : IRequest<LoginUserDTO>
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
