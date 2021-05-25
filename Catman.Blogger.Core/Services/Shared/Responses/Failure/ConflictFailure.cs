namespace Catman.Blogger.Core.Services.Shared.Responses.Failure
{
    public class ConflictFailure : IFailureResponse
    {
        public string Cause { get; }

        public ConflictFailure(string cause)
        {
            Cause = cause;
        }
    }
}
