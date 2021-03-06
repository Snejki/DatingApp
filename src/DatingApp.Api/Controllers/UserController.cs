﻿namespace DatingApp.Api.Controllers
{
    using System.Threading.Tasks;
    using DatingApp.Infrastructure.Commands.UserCommands;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/user")]
    public class UserController : AbstractController
    {
        public UserController(IMediator mediator)
            : base(mediator)
        {
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Get([FromBody] RegisterUserCommand command)
            => this.Ok(await this.Handle(command));

        [HttpPut("change-password")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
            => this.Ok(await this.Handle(command));

        [HttpPut("update-bio")]
        public async Task<ActionResult> UpdateBio([FromBody] UpdateBioCommand command)
            => this.Ok(await this.Handle(command));

    }
}
