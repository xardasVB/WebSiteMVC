using BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;
using DAL.Abstract;
using DAL.Entity;
using SimpleCrypto;

namespace BLL.Concrete
{
    public class UserProvider : IUserProvider
    {
        private readonly IUserRepository _userRepository;

        public UserProvider(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<RegisterViewModel> GetUsers()
        {
            return _userRepository.GetAllUsers().Select(u => new RegisterViewModel
            {
                Email = u.Email,
                Password = u.Password
            });
        }

        public StatusAccountViewModel Register(RegisterViewModel model)
        {
            var userDub =_userRepository.GetUserByEmail(model.Email);

            if (userDub != null)
                return StatusAccountViewModel.Dublication;

            var crypto = new PBKDF2();
            User user = new User();
            user.Email = model.Email;
            user.Password = crypto.Compute(model.Password);
            user.PasswordSalt = crypto.Salt;
            _userRepository.Add(user);
            _userRepository.SaveChanges();

            return StatusAccountViewModel.Success;
        }
    }
}
