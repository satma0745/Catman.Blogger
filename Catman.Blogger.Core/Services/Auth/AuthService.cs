namespace Catman.Blogger.Core.Services.Auth
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.Blogger.Core.Persistence.UnitOfWork;
    using Catman.Blogger.Core.Services.Auth.Data.Requests;
    using Catman.Blogger.Core.Services.Auth.Data.Responses;
    using Catman.Blogger.Core.Services.Shared;
    using Catman.Blogger.Core.Services.Shared.OperationResults;
    using Catman.Blogger.Core.Services.Shared.Responses.Failure;

    internal class AuthService : ServiceBase, IAuthService
    {
        private readonly IMapper _mapper;
        
        public AuthService(IUnitOfWorkAsyncFactory unitOfWorkFactory, IMapper mapper)
            : base(unitOfWorkFactory)
        {
            _mapper = mapper;
        }
        
        public Task<OperationResult<Session>> Authenticate(AuthenticateUser request) =>
            Operation(async unitOfWork =>
            {
                if (!await unitOfWork.Users.UserExistsAsync(request.Username))
                {
                    var notFoundResponse = new NotFoundFailure();
                    return Failure<Session>(notFoundResponse);
                }
                
                var user = await unitOfWork.Users.GetUserAsync(request.Username);
                
                if (!await unitOfWork.Users.UserHasPasswordAsync(user.Id, request.Password))
                {
                    var incorrectPasswordResponse = new IncorrectPasswordFailure();
                    return Failure<Session>(incorrectPasswordResponse);
                }

                var session = _mapper.Map<Session>(user);
                return Success(session);
            });
    }
}
