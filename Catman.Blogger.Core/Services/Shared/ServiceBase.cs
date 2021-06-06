namespace Catman.Blogger.Core.Services.Shared
{
    using System;
    using System.Threading.Tasks;
    using Catman.Blogger.Core.Persistence.UnitOfWork;
    using Catman.Blogger.Core.Services.Shared.OperationResults;
    using Catman.Blogger.Core.Services.Shared.Responses.Failure;
    using Catman.Blogger.Core.Services.Shared.Responses.Success;

    internal abstract class ServiceBase
    {
        protected readonly IUnitOfWorkAsyncFactory UnitOfWorkFactory;

        public ServiceBase(IUnitOfWorkAsyncFactory unitOfWorkFactory)
        {
            UnitOfWorkFactory = unitOfWorkFactory;
        }

        protected async Task<OperationResult<T>> Operation<T>(Func<IUnitOfWork, Task<OperationResult<T>>> handleAsync)
        {
            await using var unitOfWork = await UnitOfWorkFactory.CreateAsync();

            try
            {
                var result = await handleAsync(unitOfWork);
                
                await unitOfWork.CommitAsync();

                return result;
            }
            catch (Exception exception)
            {
                await unitOfWork.RollbackAsync();
                
                var response = new UnexpectedFailure(exception);
                return Failure<T>(response);
            }
        }

        protected async Task<OperationResult> Operation(Func<IUnitOfWork, Task<OperationResult>> handleAsync)
        {
            var result = await Operation<OperationSuccess>(async unitOfWork => await handleAsync(unitOfWork));
            return result as OperationResult;
        }
        
        protected static OperationResult Success() =>
            new OperationResult();
        
        protected static OperationResult<TResponse> Success<TResponse>(TResponse responseResponse) =>
            new OperationResult<TResponse>(responseResponse);

        protected static OperationResult Failure(IFailureResponse failureResponse) =>
            new OperationResult(failureResponse);
        
        protected static OperationResult<TResponse> Failure<TResponse>(IFailureResponse failureResponse) =>
            new OperationResult<TResponse>(failureResponse);
    }
}
