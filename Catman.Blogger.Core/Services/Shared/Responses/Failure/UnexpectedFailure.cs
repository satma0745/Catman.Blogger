namespace Catman.Blogger.Core.Services.Shared.Responses.Failure
{
    using System;

    public class UnexpectedFailure : IFailureResponse
    {
        public Exception Cause { get; }

        public UnexpectedFailure(Exception cause)
        {
            Cause = cause;
        }
    }
}
