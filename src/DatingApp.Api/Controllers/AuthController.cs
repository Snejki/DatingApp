namespace DatingApp.Api.Controllers
{
    using System.Threading.Tasks;
    using DatingApp.Infrastructure.Queries.AuthQueries;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/auth")]
    public class AuthController : AbstractController
    {
        public AuthController(IMediator mediator)
            : base(mediator)
        {
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginUserQuery query)
            => this.Ok(await this.Handle(query));
    }
}
