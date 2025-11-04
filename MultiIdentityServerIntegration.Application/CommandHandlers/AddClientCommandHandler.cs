using FluentResults;
using MediatR;
using MultiIdentityServerIntegration.Application.Commands;

namespace MultiIdentityServerIntegration.Application.CommandHandlers;

public class AddClientCommandHandler : IRequestHandler<AddClientCommand, Result<bool>>
{
    public AddClientCommandHandler()
    {
    }

    public async Task<Result<bool>> Handle(AddClientCommand request, CancellationToken cancellationToken)
    {
        var result = new Result<bool>();
        bool databaseOperationIsSuccessful = false;

        //-----------------------------------------
        // Your user adding operation goes here...
        //-----------------------------------------

        result = databaseOperationIsSuccessful
            ? result.WithValue(true)
            : result.WithError("Failed to insert user.");

        return result;
    }
}


