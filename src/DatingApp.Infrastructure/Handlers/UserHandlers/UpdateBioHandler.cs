namespace DatingApp.Infrastructure.Handlers.UserHandlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using DatingApp.Core.Entities.Concrete;
    using DatingApp.Core.Exceptions;
    using DatingApp.Core.Repositories.Abstract;
    using DatingApp.Infrastructure.Commands.UserCommands;
    using MediatR;

    public class UpdateBioHandler : IRequestHandler<UpdateBioCommand>
    {
        private readonly IUserRepository userRepository;
        private readonly ISaveRepository<User> saveRepository;

        public UpdateBioHandler(ISaveRepository<User> saveRepository, IUserRepository userRepository)
        {
            this.saveRepository = saveRepository;
            this.userRepository = userRepository;
        }

        public async Task<Unit> Handle(UpdateBioCommand request, CancellationToken cancellationToken)
        {
            var user = await this.userRepository.GetById(request.UserId);
            if (user == null)
            {
                throw new DatingAppException(ErrorCode.UnverifiedUser);
            }

            user.SetFristname(request.Firstname);
            user.SetLastname(request.Lastname);
            user.SetBirthDate(request.BirthDate);
            user.SetAgeRange(request.AgeRange);
            user.SetInterests(request.Interests);
            user.SetAboutMe(request.AboutMe);
            user.SetWork(request.Work);
            user.SetMaxDistance(request.MaxDistance);

            await this.saveRepository.Update(user);
            await this.saveRepository.Commit();

            return Unit.Value;
        }
    }
}
