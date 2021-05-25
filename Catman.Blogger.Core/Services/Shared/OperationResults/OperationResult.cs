namespace Catman.Blogger.Core.Services.Shared.OperationResults
{
    using System;
    using Catman.Blogger.Core.Services.Shared.Responses.Failure;

    public class OperationResult<TSuccessResponse>
    {
        private readonly bool _isSuccess;

        private readonly TSuccessResponse _successResponse;
        private readonly IFailureResponse _failureResponse;

        public OperationResult(TSuccessResponse successResponse)
        {
            _isSuccess = true;
            _successResponse = successResponse;
        }

        public OperationResult(IFailureResponse failureResponse)
        {
            _isSuccess = false;
            _failureResponse = failureResponse;
        }

        public TSelected Select<TSelected>(
            Func<TSuccessResponse, TSelected> onSuccess,
            Func<IFailureResponse, TSelected> onFailure) =>
            _isSuccess
                ? onSuccess(_successResponse)
                : onFailure(_failureResponse);
    }
}
