namespace Catman.Blogger.Core.Services.Shared.OperationResults
{
    using System;
    using Catman.Blogger.Core.Services.Shared.Responses.Failure;
    using Catman.Blogger.Core.Services.Shared.Responses.Success;

    public class OperationResult : OperationResult<OperationSuccess>
    {
        public OperationResult()
            : base(new OperationSuccess())
        {
        }

        public OperationResult(IFailureResponse failureResponse)
            : base(failureResponse)
        {
        }
    }

    public class OperationResult<TSuccessResponse>
    {
        public bool IsSuccess { get; }

        private readonly TSuccessResponse _successResponse;
        private readonly IFailureResponse _failureResponse;

        public OperationResult(TSuccessResponse successResponse)
        {
            IsSuccess = true;
            _successResponse = successResponse;
        }

        public OperationResult(IFailureResponse failureResponse)
        {
            IsSuccess = false;
            _failureResponse = failureResponse;
        }

        public TSelected Select<TSelected>(
            Func<TSuccessResponse, TSelected> onSuccess,
            Func<IFailureResponse, TSelected> onFailure) =>
            IsSuccess
                ? onSuccess(_successResponse)
                : onFailure(_failureResponse);

        public void Consume(
            Action<TSuccessResponse> onSuccess,
            Action<IFailureResponse> onFailure)
        {
            if (IsSuccess)
            {
                onSuccess(_successResponse);
            }
            else
            {
                onFailure(_failureResponse);
            }
        }
    }
}
