namespace DatingApp.Core.Repositories.Concrete
{
    using System.Threading.Tasks;
    using DatingApp.Core.Entities.Abstract;
    using DatingApp.Core.Repositories.Abstract;
    using Microsoft.EntityFrameworkCore;

    public class SaveRepository<T> : ISaveRepository<T>
        where T : Entity
    {
        private readonly DatingAppContext context;
        private readonly DbSet<T> dbSet;

        public SaveRepository(DatingAppContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public async Task Add(T entity)
            => await this.dbSet.AddAsync(entity).ConfigureAwait(false);

        public async Task Update(T entity)
            => await Task.FromResult(this.dbSet.Update(entity)).ConfigureAwait(false);

        public async Task Remove(T entity)
            => await Task.FromResult(this.dbSet.Remove(entity)).ConfigureAwait(false);

        public async Task<int> Commit()
            => await this.context.SaveChangesAsync().ConfigureAwait(false);
    }
}
