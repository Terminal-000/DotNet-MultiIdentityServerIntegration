using FluentResults;
using MediatR;

namespace MultiIdentityServerIntegration.Application.Commands;

public class AddClientCommand : IRequest<Result<bool>>
{
    public required string Name { get; set; }
    public required string PhoneNumber { get; set; }
}