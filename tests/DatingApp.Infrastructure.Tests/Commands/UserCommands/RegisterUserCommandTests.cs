using AutoFixture.Xunit2;
using DatingApp.Core.Entities.Concrete;
using DatingApp.Core.Exceptions;
using DatingApp.Core.Repositories.Abstract;
using DatingApp.Infrastructure.Commands.UserCommands;
using DatingApp.Infrastructure.Handlers.UserHandlers;
using DatingApp.Infrastructure.Services.Abstract;
using FluentAssertions;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DatingApp.Infrastructure.Tests.Commands.UserCommands
{
    public class RegisterUserCommandTests
    {
        private readonly Mock<IUserRepository> userRepositoryMock;
        private readonly Mock<IEncrypter> encrypterMock;
        private readonly Mock<ISaveRepository<User>> saveRepositoryMock;
        private RegisterUserHandler sut;

        public RegisterUserCommandTests()
        {
            this.userRepositoryMock = new Mock<IUserRepository>();
            this.encrypterMock = new Mock<IEncrypter>();
            this.saveRepositoryMock = new Mock<ISaveRepository<User>>();
        }

        [Theory]
        [AutoData]
        public async Task Handle_WhenRegisterUserCommandIsValid_ShouldReturnUnitValue(RegisterUserCommand command)
        {
            // Arrange
            User user = null;

            this.userRepositoryMock.Setup(u => u.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult(user));
            this.userRepositoryMock.Setup(u => u.GetByUsername(It.IsAny<string>())).Returns(Task.FromResult(user));

            this.encrypterMock.Setup(e => e.GetSalt(It.IsAny<string>())).Returns("salt");
            this.encrypterMock.Setup(e => e.GetHash(It.IsAny<string>(), It.IsAny<string>())).Returns("hash");

            this.saveRepositoryMock.Setup(s => s.Add(It.IsAny<User>())).Verifiable();
            this.saveRepositoryMock.Setup(s => s.Commit()).Verifiable();

            this.sut = new RegisterUserHandler(this.userRepositoryMock.Object, this.encrypterMock.Object, this.saveRepositoryMock.Object);

            // Act
            var result = await this.sut.Handle(command, new System.Threading.CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(Unit.Value);
        }

        [Theory]
        [AutoData]
        public async Task Handle_WhenTryToAddUserWithSameEmail_ShoulThrowException(RegisterUserCommand command)
        {
            // Arrange
            User user = new User("mail", "u", "f", "l ", "i", "d", DateTime.Now, DateTime.Now);

            this.userRepositoryMock.Setup(u => u.GetByEmail(It.IsAny<string>())).Returns(Task.FromResult(user));

            this.sut = new RegisterUserHandler(this.userRepositoryMock.Object, this.encrypterMock.Object, this.saveRepositoryMock.Object);

            // Act
            Func<Task> act = async () => await this.sut.Handle(command, new System.Threading.CancellationToken());

            // Assert
            act.Should().Throw<DatingAppException>().Where(e => e.Message == "User with provided email already exists");
        }
    }
}
