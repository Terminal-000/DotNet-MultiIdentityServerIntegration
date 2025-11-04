using FluentResults;
using MediatR;
using MultiIdentityServerIntegration.Application.Commands;

namespace MultiIdentityServerIntegration.Application.CommandHandlers;

public class SomeInternalOperationCommandHandler : IRequestHandler<SomeInternalOperationCommand, Result<bool>>
{
    public SomeInternalOperationCommandHandler()
    {
    }

    public async Task<Result<bool>> Handle(SomeInternalOperationCommand request, CancellationToken cancellationToken)
    {
        var result = new Result<bool>();
        bool databaseOperationIsSuccessful = false;

        //-----------------------------------------
        // Your internal operation goes here
        //-----------------------------------------

        result = databaseOperationIsSuccessful
            ? result.WithValue(true)
            : result.WithError("Failed to handle request.");

        return result;
    }
}


