using System.Threading.Tasks;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiIdentityServerIntegration.Application.Commands;

namespace MultiIdentityServerIntegration.API.Controllers
{
    /// <summary>
    /// Handles client-related operations.
    /// All endpoints in this controller require a valid authorization token 
    /// that satisfies the "ClientScope" policy we defined in the Program.cs file.
    /// </summary>
    [ApiController]
    [Authorize(policy: "ClientScope")] // policy selector
    [Route("Client")]
    public class ClientServicesController : ControllerBase
    {
        // DI for mediator
        private readonly IMediator _mediator;

        public ClientServicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Example endpoint of a client-side operation
        [HttpPost("AddClient")]
        [Produces("application/json")]
        public async Task<ActionResult<Result<bool>>> AddClient(AddClientCommand cmd)
        {
            var result = await _mediator.Send(cmd);

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result.ToResult());
        }
    }
}
