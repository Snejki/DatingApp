namespace DatingApp.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    public abstract class AbstractController : ControllerBase
    {
        private readonly IMediator mediator;

        protected AbstractController(IMediator mediator)
        {
            this.mediator = mediator;
        }


    }
}
