namespace DatingApp.Core.Repositories.Concrete
{
    using System.Threading.Tasks;
    using DatingApp.Core.Entities.Concrete;
    using DatingApp.Core.Repositories.Abstract;
    using Microsoft.EntityFrameworkCore;

    internal class UserRepository : IUserRepository
    {
        private readonly DatingAppContext context;

        public UserRepository(DatingAppContext context)
        {
            this.context = context;
        }

        public async Task<User> GetByEmail(string email)
        {
            return await this.context.Users.SingleOrDefaultAsync(u => u.Email == email).ConfigureAwait(false);
        }

        public async Task<User> GetById(int id)
        {
            return await this.context.Users.SingleOrDefaultAsync(u => u.Id == id).ConfigureAwait(false);
        }

        public async Task<User> GetByUsername(string username)
        {
            return await this.context.Users.SingleOrDefaultAsync(u => u.Username == username).ConfigureAwait(false);
        }
    }
}
