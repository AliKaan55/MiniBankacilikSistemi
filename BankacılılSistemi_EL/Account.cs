using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankacılılSistemi_EL
{
    public class Account
    {
        public int Id { get; set; }
        public string IBAN { get; set; }
        public decimal Balance { get; set; }
        public string CurrencyType { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
