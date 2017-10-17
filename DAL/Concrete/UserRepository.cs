using DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entity;

namespace DAL.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly IEFContext _ctx;

        public UserRepository(IEFContext ctx)
        {
            _ctx = ctx;
        }

        public User Add(User user)
        {
            _ctx.Set<User>().Add(user);
            return user;
        }

        public IQueryable<User> GetAllUsers()
        {
            return _ctx.Set<User>();
        }

        public User GetUserByEmail(string email)
        {
            return _ctx.Set<User>().SingleOrDefault(u => u.Email == email);
        }

        public User GetUserById(int id)
        {
            return _ctx.Set<User>().SingleOrDefault(u => u.Id == id);
        }

        public void SaveChanges()
        {
            _ctx.SaveChanges();
        }
    }
}
