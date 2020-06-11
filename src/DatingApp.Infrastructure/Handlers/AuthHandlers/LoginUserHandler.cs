namespace DatingApp.Infrastructure.Handlers.AuthHandlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using DatingApp.Core.Exceptions;
    using DatingApp.Core.Repositories.Abstract;
    using DatingApp.Infrastructure.DTOs.AuthDTOs;
    using DatingApp.Infrastructure.Queries.AuthQueries;
    using DatingApp.Infrastructure.Services.Abstract;
    using MediatR;

    public class LoginUserHandler : IRequestHandler<LoginUserQuery, LoginUserDTO>
    {
        private readonly IUserRepository userRepository;
        private readonly IEncrypter encrypter;
        private readonly IJwtService jwsService;
        private readonly IMapper mapper;

        public LoginUserHandler(IUserRepository userRepository, IEncrypter encrypter, IJwtService jwsService, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.encrypter = encrypter;
            this.jwsService = jwsService;
            this.mapper = mapper;
        }

        public async Task<LoginUserDTO> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await this.userRepository.GetByUsername(request.Username);
            if (user == null)
            {
                throw new DatingAppException(ErrorCode.WrongPassword);
            }

            var generatedHash = this.encrypter.GetHash(request.Password, user.Salt);
            if (generatedHash != user.Hash)
            {
                throw new DatingAppException(ErrorCode.WrongPassword);
            }

            var token = this.jwsService.GenerateToken(user.Id);
            var loginDTO = this.mapper.Map<LoginUserDTO>(token);

            return loginDTO;
        }
    }
}
