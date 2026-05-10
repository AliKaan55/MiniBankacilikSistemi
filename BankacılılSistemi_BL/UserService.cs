using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankacılılSistemi_DAL;
using BankacılılSistemi_EL;

namespace BankacılılSistemi_BL
{
    public class UserService
    {
        private readonly BankDbContext _context;

        public UserService()
        {
            _context = new BankDbContext();
        }

        public User Login(string email, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public string Register(User newUser)
        {
            if (_context.Users.Any(u => u.Email == newUser.Email))
                return "Hata: Bu email zaten kullanımda.";

            _context.Users.Add(newUser);
            _context.SaveChanges();
            return "Kayıt başarılı!";
        }
    }
}
