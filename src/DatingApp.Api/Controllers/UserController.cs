using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Api.Controllers
{
    [Route("api/user")]
    public class UserController : AbstractController
    {
        public UserController(IMediator mediator) 
            : base(mediator)
        {
        }

        [HttpGet("register")]
        public async Task<ActionResult> Get()
            => Ok("xdd");
    }
}
