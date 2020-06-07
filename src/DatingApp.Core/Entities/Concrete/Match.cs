namespace DatingApp.Core.Entities.Concrete
{
    using System;
    using System.Collections.Generic;
    using DatingApp.Core.Entities.Abstract;

    public class Match : Entity
    {
        public virtual List<Like> Likes { get; set; }
        public virtual List<Message> Messages { get; set; }

        public int? UnMatchId { get; set; }
        public virtual UnMatch UnMatch { get; set; }

        public DateTime AddedAt { get; protected set; }
    }
}
