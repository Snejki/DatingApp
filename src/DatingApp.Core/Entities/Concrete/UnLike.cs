namespace DatingApp.Core.Entities.Concrete
{
    using System;
    using DatingApp.Core.Entities.Abstract;

    public class UnLike : Entity
    {
        public int UserId { get; protected set; }
        public virtual User User { get; set; }

        public int UnlikedUserId { get; protected set; }

        public DateTime AddedAt { get; protected set; }
    }
}
