namespace DatingApp.Infrastructure.Handlers.UserHandlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using DatingApp.Core.Entities.Concrete;
    using DatingApp.Core.Exceptions;
    using DatingApp.Core.Repositories.Abstract;
    using DatingApp.Core.Repositories.Concrete;
    using DatingApp.Infrastructure.Commands.UserCommands;
    using DatingApp.Infrastructure.Services.Abstract;
    using MediatR;

    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand>
    {
        private readonly IUserRepository userRepository;
        private readonly IEncrypter encrypter;
        private readonly SaveRepository<User> saveRepository;

        public ChangePasswordHandler(IUserRepository userRepository, IEncrypter encrypter, SaveRepository<User> saveRepository)
        {
            this.userRepository = userRepository;
            this.encrypter = encrypter;
            this.saveRepository = saveRepository;
        }

        public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await this.userRepository.GetById(request.UserId);
            if (user == null)
            {
                throw new DatingAppException(ErrorCode.NotFound);
            }

            var currentHash = this.encrypter.GetHash(request.OldPassword, user.Hash);
            if (currentHash != user.Hash)
            {
                throw new DatingAppException(ErrorCode.WrongPassword);
            }

            var newSalt = this.encrypter.GetSalt(request.NewPassword);
            var newHash = this.encrypter.GetHash(request.NewPassword, newSalt);

            user.UpdatePassword(newHash, newSalt);
            await this.saveRepository.Update(user);
            await this.saveRepository.Commit();

            return Unit.Value;
        }
    }
}
