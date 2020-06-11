using AutoFixture.Xunit2;
using AutoMapper;
using DatingApp.Core.Entities.Concrete;
using DatingApp.Core.Exceptions;
using DatingApp.Core.Repositories.Abstract;
using DatingApp.Infrastructure.DTOs.AuthDTOs;
using DatingApp.Infrastructure.Handlers.AuthHandlers;
using DatingApp.Infrastructure.Queries.AuthQueries;
using DatingApp.Infrastructure.Services.Abstract;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DatingApp.Infrastructure.Tests.Commands.AuthCommands
{
    public class LoginUserHandlerTests
    {
        private readonly Mock<IUserRepository> userRepositoryMock;
        private readonly Mock<IEncrypter> encrypterMock;
        private readonly Mock<IJwtService> jwtServiceMock;
        private readonly Mock<IMapper> mapperMock;

        private readonly LoginUserHandler sut;

        public LoginUserHandlerTests()
        {
            this.userRepositoryMock = new Mock<IUserRepository>();
            this.encrypterMock = new Mock<IEncrypter>();
            this.jwtServiceMock = new Mock<IJwtService>();
            this.mapperMock = new Mock<IMapper>();

            this.sut = new LoginUserHandler(
                this.userRepositoryMock.Object, this.encrypterMock.Object, this.jwtServiceMock.Object, this.mapperMock.Object);
        }

        [Theory]
        [AutoData]
        public async Task Handle_WhenUsernameAndPasswordAreCorrect_ShouldReturnLoginUserDTOWithToken(LoginUserQuery query)
        {
            // Arrange
            var user = new User(query.Username, "tesUser", "testHash", "testSalt", "John", "Doe", new DateTime(1999, 5, 15), new DateTime(2019, 12, 3));
            var loginUserToken = new LoginUserDTO
            {
                Token = "testToken"
            };

            this.userRepositoryMock.Setup(x => x.GetByUsername(It.IsAny<string>())).ReturnsAsync(user);
            this.encrypterMock.Setup(x => x.GetHash(It.IsAny<string>(), It.IsAny<string>())).Returns(user.Hash);
            this.jwtServiceMock.Setup(x => x.GenerateToken(It.IsAny<int>())).Returns(loginUserToken.Token);

            this.mapperMock
                .Setup(x => x.Map<LoginUserDTO>(It.IsAny<string>()))
                .Returns(loginUserToken);

            // Act
            var result = await this.sut.Handle(query, new System.Threading.CancellationToken());

            // Assert
            result.Should().BeOfType<LoginUserDTO>();
            result.Should().BeEquivalentTo(loginUserToken);
        }

        [Theory]
        [AutoData]
        public async Task Handle_WhenUserWithProvidedUsernameDoesNotExists_ShouldThrowWrongPasswordException(LoginUserQuery query)
        {
            // Arrange
            var user = (User)null;
            var errorCode = ErrorCode.WrongPassword;

            this.userRepositoryMock.Setup(x => x.GetByUsername(It.IsAny<string>())).ReturnsAsync(user);

            // Act
            Func<Task> act = async () => await this.sut.Handle(query, new System.Threading.CancellationToken());

            // Assert
            act.Should().Throw<DatingAppException>().Where(
                x => x.ErrorCode.StatusCode == errorCode.StatusCode &&
                x.ErrorCode.Message == errorCode.Message);
        }

        [Theory]
        [AutoData]
        public async Task Handle_WhenProvidedWrongPasswordForUser_ShouldThrowWringPasswordException(LoginUserQuery query)
        {
            // Arrange
            var user = new User(query.Username, "tesUser", "testHash", "testSalt", "John", "Doe", new DateTime(1999, 5, 15), new DateTime(2019, 12, 3));
            var errorCode = ErrorCode.WrongPassword;

            this.userRepositoryMock.Setup(x => x.GetByUsername(It.IsAny<string>())).ReturnsAsync(user);
            this.encrypterMock.Setup(x => x.GetHash(It.IsAny<string>(), It.IsAny<string>())).Returns("diffrentHash");

            // Act
            Func<Task> act = async () => await this.sut.Handle(query, new System.Threading.CancellationToken());

            // Assert
            act.Should().Throw<DatingAppException>().Where(
                x => x.ErrorCode.StatusCode == errorCode.StatusCode &&
                x.ErrorCode.Message == errorCode.Message);
        }
    }
}
