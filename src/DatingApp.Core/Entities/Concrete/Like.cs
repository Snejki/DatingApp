namespace DatingApp.Core.Entities.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DatingApp.Core.Entities.Abstract;

    public class Like : Entity
    {
        public int UserId { get; protected set; }
        public virtual User User { get; set; }

        public int LikedUserId { get; protected set; }

        public int? MatchId { get; protected set; }
        public DateTime AddedAt { get; protected set; }
    }
}
