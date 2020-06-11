namespace DatingApp.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/auth")]
    public class AuthController : AbstractController
    {
        public AuthController(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpPost]
        public async Task<ActionResult> Login()
        {
            return null;
        }
    }
}
