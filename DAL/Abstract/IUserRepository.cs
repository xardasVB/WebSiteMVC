using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IUserRepository
    {
        User Add(User user);
        IQueryable<User> GetAllUsers();
        User GetUserByEmail(string email);
        User GetUserById(int id);
        void SaveChanges();
    }
}
