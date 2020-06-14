namespace DatingApp.Api.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using DatingApp.Core.Exceptions;
    using DatingApp.Infrastructure.Commands;
    using DatingApp.Infrastructure.Queries;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public abstract class AbstractController : ControllerBase
    {
        private readonly IMediator mediator;

        protected AbstractController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        private int UserId => this.GetLoggedUserId();

        protected async Task<T> Handle<T>(IRequest<T> request)
        {
            if (request is AuthQuery)
            {
                (request as AuthQuery).UserId = this.UserId;
            }

            if (request is AuthCommand)
            {
                (request as AuthCommand).UserId = this.UserId;
            }

            return await this.mediator.Send(request);
        }

        private int GetLoggedUserId()
        {
            if (this.User?.Identity?.IsAuthenticated == true)
            {
                var userIdString = this.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                if (int.TryParse(userIdString, out int userId))
                {
                    return userId;
                }
            }

            throw new DatingAppException(ErrorCode.UnverifiedUser);
        }
    }
}
