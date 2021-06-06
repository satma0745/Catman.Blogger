namespace Catman.Blogger.Core.Services.User
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.Blogger.Core.Persistence.Repositories.User.Data;
    using Catman.Blogger.Core.Persistence.UnitOfWork;
    using Catman.Blogger.Core.Services.Shared;
    using Catman.Blogger.Core.Services.Shared.OperationResults;
    using Catman.Blogger.Core.Services.Shared.Responses.Failure;
    using Catman.Blogger.Core.Services.User.Data.Requests;
    using Catman.Blogger.Core.Services.User.Data.Responses;

    internal class UserService : ServiceBase, IUserService
    {
        private readonly IMapper _mapper;

        public UserService(IUnitOfWorkAsyncFactory unitOfWorkFactory, IMapper mapper)
            : base(unitOfWorkFactory)
        {
            _mapper = mapper;
        }
        
        public Task<OperationResult<ICollection<User>>> GetUsers() =>
            Operation(async unitOfWork =>
            {
                var users = await unitOfWork.Users.GetUsersAsync();

                var response = _mapper.Map<ICollection<User>>(users);
                return Success(response);
            });

        public Task<OperationResult> RegisterUser(RegisterUser request) =>
            Operation(async unitOfWork =>
            {
                if (!await unitOfWork.Users.UsernameIsAvailableAsync(request.Username))
                {
                    var failure = new ConflictFailure("This username is already taken");
                    return Failure(failure);
                }

                var creationData = _mapper.Map<UserCreationData>(request);
                await unitOfWork.Users.CreateUserAsync(creationData);

                return Success();
            });

        public Task<OperationResult> DeleteUser(Guid userId) =>
            Operation(async unitOfWork =>
            {
                if (!await unitOfWork.Users.UserExistsAsync(userId))
                {
                    var failure = new NotFoundFailure();
                    return Failure(failure);
                }

                await unitOfWork.Users.DeleteUserAsync(userId);
                return Success();
            });
    }
}
