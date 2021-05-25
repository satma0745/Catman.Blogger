namespace Catman.Blogger.Core.Services.Shared
{
    using Catman.Blogger.Core.Services.Shared.OperationResults;
    using Catman.Blogger.Core.Services.Shared.Responses.Failure;

    internal abstract class ServiceBase
    {
        protected static OperationResult<TResponse> Success<TResponse>(TResponse responseResponse) =>
            new OperationResult<TResponse>(responseResponse);

        protected static OperationResult<TResponse> Failure<TResponse>(IFailureResponse failureResponse) =>
            new OperationResult<TResponse>(failureResponse);
    }
}
