using System.Threading.Tasks;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiIdentityServerIntegration.Application.Commands;

namespace MultiIdentityServerIntegration.API.Controllers
{
    /// <summary>
    /// Handles internal Machine to Machine operations.
    /// All endpoints in this controller require a valid authorization token 
    /// that satisfies the "InternalScope" policy we defined in the Program.cs file.
    /// </summary>
    [ApiController]
    [Authorize(policy: "InternalScope")] // policy selector
    [Route("Internal")]
    public class InternalServicesController : ControllerBase
    {
        // DI for mediator
        private readonly IMediator _mediator;

        public InternalServicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Example endpoint of a client-side operation
        [HttpPost("ExampleInternalOperation")]
        [Produces("application/json")]
        public async Task<ActionResult<Result<bool>>> AddClient(SomeInternalOperationCommand cmd)
        {
            var result = await _mediator.Send(cmd);

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result.ToResult());
        }
    }
}
