namespace DatingApp.Infrastructure.Handlers.UserHandlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using DatingApp.Core.Entities.Concrete;
    using DatingApp.Core.Exceptions;
    using DatingApp.Core.Repositories.Abstract;
    using DatingApp.Infrastructure.Commands.UserCommands;
    using DatingApp.Infrastructure.Services.Abstract;
    using MediatR;

    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly IUserRepository userRepository;
        private readonly IEncrypter encrypter;
        private readonly ISaveRepository<User> saveRepository;

        public RegisterUserHandler(IUserRepository userRepository, IEncrypter encrypter, ISaveRepository<User> saveRepository)
        {
            this.userRepository = userRepository;
            this.encrypter = encrypter;
            this.saveRepository = saveRepository;
        }

        public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = await this.userRepository.GetByEmail(request.Email);
            if (user != null)
            {
                throw new DatingAppException(ErrorCode.AlreadyExists, "User with provided email already exists");
            }

            user = await this.userRepository.GetByUsername(request.Username);
            if (user != null)
            {
                throw new DatingAppException(ErrorCode.AlreadyExists);
            }

            var passwordSalt = this.encrypter.GetSalt(request.Password);
            var passwordHash = this.encrypter.GetHash(request.Password, passwordSalt);
            var now = DateTime.UtcNow;
            var birthDate = DateTime.UtcNow;

            user = new User(request.Email, request.Username, passwordHash, passwordSalt, request.Firstname, request.Lastname, birthDate, now);

            await this.saveRepository.Add(user);
            await this.saveRepository.Commit();

            return Unit.Value;
        }
    }
}
