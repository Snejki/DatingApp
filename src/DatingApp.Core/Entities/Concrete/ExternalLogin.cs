namespace DatingApp.Core.Entities.Concrete
{
    using System;
    using DatingApp.Core.Entities.Abstract;
    using DatingApp.Core.Enums;

    public class ExternalLogin : Entity
    {
        public ExternalLoginProvider ExternalLoginProvider { get; protected set; }

        public string Token { get; protected set; }

        public DateTime AddedAt { get; protected set; }

        public int UserId { get; protected set; }

        public User User { get; set; }
    }
}
