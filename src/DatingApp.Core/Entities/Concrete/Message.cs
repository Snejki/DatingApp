namespace DatingApp.Core.Entities.Concrete
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using DatingApp.Core.Entities.Abstract;

    public class Message : Entity
    {
        public string Text { get; protected set; }
        public DateTime AddedAt { get; protected set; }

        [ForeignKey("Sender")]
        public int SenderId { get; protected set; }
        public virtual User Sender { get; set; }
        public int MatchId { get; protected set; }
        public virtual Match Match { get; protected set; }

        public bool IsRead { get; protected set; }
    }
}
