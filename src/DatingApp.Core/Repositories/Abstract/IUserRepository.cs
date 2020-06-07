using DatingApp.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DatingApp.Core.Repositories.Abstract
{
    public interface IUserRepository : IRepository
    {
        Task<User> GetById(int id);

        Task<User> GetByEmail(string email);

        Task<User> GetByUsername(string username);
    }
}
