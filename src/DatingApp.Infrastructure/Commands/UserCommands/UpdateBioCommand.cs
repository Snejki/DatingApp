namespace DatingApp.Infrastructure.Commands.UserCommands
{
    using System;
    using DatingApp.Core.Enums;
    using MediatR;

    public class UpdateBioCommand : AuthCommand, IRequest
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public DateTime BirthDate { get; set; }

        public AgeRange AgeRange { get; set; }

        public string Interests { get; set; }

        public string AboutMe { get; set; }

        public string Work { get; set; }

        public int MaxDistance { get; set; }
    }
}
