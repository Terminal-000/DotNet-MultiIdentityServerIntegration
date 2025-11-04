using FluentResults;
using MediatR;

namespace MultiIdentityServerIntegration.Application.Commands;

public class SomeInternalOperationCommand : IRequest<Result<bool>>
{
    public required string Request { get; set; }
}