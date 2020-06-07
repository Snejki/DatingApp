namespace DatingApp.Core.Repositories.Abstract
{
    using System.Threading.Tasks;
    using DatingApp.Core.Entities.Abstract;

    public interface ISaveRepository<T>
        where T : Entity
    {
        Task Add(T entity);

        Task Update(T entity);

        Task Remove(T entity);

        Task<int> Commit();
    }
}
