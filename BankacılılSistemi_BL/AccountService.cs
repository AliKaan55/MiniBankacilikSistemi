using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankacılılSistemi_EL;
using BankacılılSistemi_DAL;


namespace BankacılılSistemi_BL
{
    public class AccountService
    {
        private readonly BankDbContext _context;
        public AccountService()
        {
            
            _context = new BankDbContext();
        }
        public string CreateAccount(Account newAccount)
        {
            
            var isIbanExist = _context.Accounts.Any(a => a.IBAN == newAccount.IBAN);

            if (isIbanExist)
            {
                return "Hata: Bu IBAN numarası sistemde zaten kayıtlı!";
            }

            
            if (newAccount.Balance < 0)
            {
                newAccount.Balance = 0; 
            }

            
            newAccount.CreatedDate = DateTime.Now;

            try
            {
                
                _context.Accounts.Add(newAccount);
                _context.SaveChanges();

                return "Hesap başarıyla oluşturuldu.";
            }
            catch (Exception ex)
            {
                return "Bir hata oluştu: " + ex.Message;
            }
        }
        public string DepositMoney(int accountId, decimal amount)
        {
            
            if (amount <= 0)
            {
                return "Hata: Yatırılacak tutar 0'dan büyük olmalıdır.";
            }

            var account = _context.Accounts.FirstOrDefault(a => a.Id == accountId);

            if (account == null)
            {
                return "Hata: Hesap bulunamadı.";
            }

            account.Balance += amount;

            var transaction = new Transaction
            {
                Amount = amount,
                TransactionDate = DateTime.Now,
                Type = TransactionType.Deposit,
                ReceiverAccountId = accountId, // Paranın girdiği hesap
                SenderAccountId = null, // Veya bu satırı tamamen sil/null yap (eğer SQL izin veriyorsa)
                Description = "Nakit Para Yatırma"
            };

            try
            {
                _context.Transactions.Add(transaction); 
                _context.SaveChanges(); 

                return $"{amount} TL başarıyla yatırıldı. Yeni bakiye: {account.Balance}";
            }
            catch (Exception ex)
            {
                
                return "HATA DETAYI: " + (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }
        public string TransferMoney(int senderAccountId, int receiverAccountId, decimal amount)
        {
            
            if (amount <= 0) return "Hata: Transfer tutarı 0'dan büyük olmalıdır.";

            
            using (var dbTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    
                    var sender = _context.Accounts.FirstOrDefault(a => a.Id == senderAccountId);
                    var receiver = _context.Accounts.FirstOrDefault(a => a.Id == receiverAccountId);

                    if (sender == null || receiver == null)
                        return "Hata: Gönderen veya alıcı hesap bulunamadı.";

                    
                    if (sender.Balance < amount)
                        return "Hata: Yetersiz bakiye.";

                    
                    sender.Balance -= amount;
                    receiver.Balance += amount;

                    
                    var log = new Transaction
                    {
                        Amount = amount,
                        TransactionDate = DateTime.Now,
                        Type = TransactionType.Transfer,
                        SenderAccountId = senderAccountId,
                        ReceiverAccountId = receiverAccountId,
                        Description = $"{senderAccountId} nolu hesaptan {receiverAccountId} nolu hesaba havale."
                    };

                    _context.Transactions.Add(log);
                    _context.SaveChanges(); 

                    
                    dbTransaction.Commit();

                    return $"Transfer başarılı. Yeni bakiyeniz: {sender.Balance}";
                }
                catch (Exception ex)
                {
                    
                    dbTransaction.Rollback();
                    return "İşlem sırasında bir hata oluştu, para iade edildi: " + ex.Message;
                }
            }
        }
        public List<Account> GetAllAccounts()
        {
            return _context.Accounts.ToList();
        }
        public List<Transaction> GetTransactionHistory(int accountId)
        {
            return _context.Transactions
                .Where(t => t.SenderAccountId == accountId || t.ReceiverAccountId == accountId)
                .OrderByDescending(t => t.TransactionDate) 
                .ToList();
        }
    }
}
