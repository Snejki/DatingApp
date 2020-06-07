using DatingApp.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatingApp.Core.Entities.Concrete
{
    public class Photo : Entity
    {
        public string Path { get; protected set; }

        public string Order { get; protected set; }

        public int UserId { get; protected set; }

        public virtual User User { get; protected set; }
    }
}
