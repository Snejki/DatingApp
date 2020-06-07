namespace DatingApp.Core
{
    using DatingApp.Core.Entities.Concrete;
    using Microsoft.EntityFrameworkCore;

    public class DatingAppContext : DbContext
    {
        public DatingAppContext(DbContextOptions<DatingAppContext> context)
            : base(context)
        {
        }

        public DbSet<ExternalLogin> ExternalLogins { get; set; }

        public DbSet<Like> Likes { get; set; }

        public DbSet<Match> Matches { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<UnLike> UnLikes { get; set; }

        public DbSet<UnMatch> UnMatches { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
