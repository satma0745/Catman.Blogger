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
        private readonly IUnitOfWorkAsyncFactory _unitOfWorkFactory;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWorkAsyncFactory unitOfWorkFactory, IMapper mapper)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _mapper = mapper;
        }
        
        public async Task<OperationResult<ICollection<User>>> GetUsers()
        {
            await using var unitOfWork = await _unitOfWorkFactory.CreateAsync();

            try
            {
                var users = await unitOfWork.Users.GetUsersAsync();

                var response = _mapper.Map<ICollection<User>>(users);
                return Success(response);
            }
            catch (Exception exception)
            {
                var response = new UnexpectedFailure(exception);
                return Failure<ICollection<User>>(response);
            }
        }

        public async Task<OperationResult> RegisterUser(RegisterUser request)
        {
            await using var unitOfWork = await _unitOfWorkFactory.CreateAsync();

            try
            {
                if (!await unitOfWork.Users.UsernameIsAvailableAsync(request.Username))
                {
                    var failure = new ConflictFailure("This username is already taken");
                    return Failure(failure);
                }

                var creationData = _mapper.Map<UserCreationData>(request);
                await unitOfWork.Users.CreateUserAsync(creationData);
                await unitOfWork.CommitAsync();

                return Success();
            }
            catch (Exception exception)
            {
                await unitOfWork.RollbackAsync();
                
                var response = new UnexpectedFailure(exception);
                return Failure(response);
            }
        }

        public async Task<OperationResult> DeleteUser(Guid id)
        {
            await using var unitOfWork = await _unitOfWorkFactory.CreateAsync();

            try
            {
                if (!await unitOfWork.Users.UserExistsAsync(id))
                {
                    var failure = new NotFoundFailure();
                    return Failure(failure);
                }

                await unitOfWork.Users.DeleteUserAsync(id);
                await unitOfWork.CommitAsync();

                return Success();
            }
            catch (Exception exception)
            {
                await unitOfWork.RollbackAsync();
                
                var response = new UnexpectedFailure(exception);
                return Failure(response);
            }
        }
    }
}
