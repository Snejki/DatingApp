namespace DatingApp.Core.Entities.Concrete
{
    using System;
    using System.Collections.Generic;
    using DatingApp.Core.Entities.Abstract;
    using DatingApp.Core.Enums;
    using DatingApp.Core.Exceptions;

    public class User : Entity
    {
        public string Email { get; protected set; }

        public string Username { get; protected set; }

        public string Firstname { get; protected set; }

        public string Lastname { get; protected set; }

        public string Hash { get; protected set; }

        public string Salt { get; protected set; }

        public DateTime BirthDate { get; protected set; }

        public AgeRange AgeRange { get; protected set; }

        public string Interests { get; protected set; }

        public string AboutMe { get; protected set; }

        public string Work { get; protected set; }

        public int MaxDistance { get; protected set; }

        public bool Privacy { get; protected set; }

        public string RememberPasswordToken { get; protected set; }

        public virtual List<Photo> Photos { get; set; }

        public virtual List<Like> Likes { get; set; }

        public virtual List<UnLike> UnLikes { get; set; }

        public virtual List<ExternalLogin> ExternalLogins { get; set; }

        public DateTime AddedAt { get; protected set; }

        public User(string email, string username, string hash, string salt, string firstname, string lastname, DateTime birthDate, DateTime addedAt)
        {
            this.SetEmail(email);
            this.SetUsername(username);
            this.SetPassword(hash, salt);
            this.SetFristname(firstname);
            this.SetLastname(lastname);
            this.SetBirthDate(birthDate);
            this.SetAddedAt(addedAt);
        }

        protected User()
        {
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new DatingAppException(ErrorCode.Empty(nameof(email)));
            }

            this.Email = email;
        }

        public void SetUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new DatingAppException(ErrorCode.Empty(nameof(username)));
            }

            this.Username = username;
        }

        public void SetPassword(string hash, string salt)
        {
            if (string.IsNullOrEmpty(hash) || string.IsNullOrEmpty(salt))
            {
                throw new DatingAppException(ErrorCode.Empty("password"));
            }

            this.Hash = hash;
            this.Salt = salt;
        }

        public void SetFristname(string firstname)
        {
            if (string.IsNullOrEmpty(firstname))
            {
                throw new DatingAppException(ErrorCode.Empty(nameof(firstname)));
            }

            this.Firstname = firstname;
        }

        public void SetLastname(string lastname)
        {
            if (string.IsNullOrEmpty(lastname))
            {
                throw new DatingAppException(ErrorCode.Empty(nameof(lastname)));
            }

            this.Lastname = lastname;
        }

        public void SetBirthDate(DateTime birthDate)
        {
            this.BirthDate = birthDate;
        }

        public void SetAddedAt(DateTime addedAt)
        {
            this.AddedAt = addedAt;
        }

        public void SetAgeRange(AgeRange ageRange)
        {
            this.AgeRange = ageRange;
        }

        public void SetInterests(string interests)
        {
            this.Interests = interests;
        }

        public void SetAboutMe(string aboutMe)
        {
            this.AboutMe = aboutMe;
        }

        public void SetWork(string work)
        {
            this.Work = work;
        }

        public void SetMaxDistance(int maxDistance)
        {
            this.MaxDistance = maxDistance;
        }
    }
}
