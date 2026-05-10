using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankacılılSistemi_EL
{
    public class User
    {
        public int Id { get; set; }
        public string NameSurname { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Account> Accounts { get; set; }
        

    }
}
