using BankacılılSistemi_BL;
using BankacılılSistemi_DAL;
using BankacılılSistemi_EL;
using System;
using System.Linq;

using (var context = new BankDbContext())
{
    if (!context.Users.Any())
    {
        context.Users.Add(new User { NameSurname = "Ali Kaan", IdentityNumber = "12345", Email = "ali@kaan.com", Password = "123" });
        context.SaveChanges();
    }
}

// 1. SERVİSLERİ BAŞLAT
UserService userService = new UserService();
AccountService accountService = new AccountService();
User loggedInUser = null;

// 2. GİRİŞ EKRANI (Login)
while (loggedInUser == null)
{
    Console.WriteLine("=== GİRİŞ YAPIN ===");
    Console.Write("Email: ");
    string email = Console.ReadLine();
    Console.Write("Şifre: ");
    string password = Console.ReadLine();

    loggedInUser = userService.Login(email, password);

    if (loggedInUser == null)
    {
        Console.WriteLine("Hatalı giriş! Lütfen tekrar deneyin.");
    }
}

Console.Clear();
Console.WriteLine($"Hoş geldin, {loggedInUser.NameSurname}!");

// 3. ANA MENÜ DÖNGÜSÜ
bool devamEdilsinMi = true;

while (devamEdilsinMi)
{
    Console.WriteLine("\n--- BANKACILIK SİSTEMİ ANA MENÜ ---");
    Console.WriteLine("1 - Yeni Hesap Aç");
    Console.WriteLine("2 - Para Yatır");
    Console.WriteLine("3 - Para Transferi");
    Console.WriteLine("4 - Hesaplarımı Listele");
    Console.WriteLine("0 - Çıkış");
    Console.Write("Seçiminiz: ");

    string secim = Console.ReadLine();

    switch (secim)
    {
        case "1":
            Console.Write("IBAN Giriniz: ");
            string iban = Console.ReadLine();

            Console.Write("Döviz Türü (TL/USD/EUR): ");
            string doviz = Console.ReadLine();

            Account yeniHesap = new Account
            {
                IBAN = iban,
                CurrencyType = doviz,
                Balance = 0,
                UserId = loggedInUser.Id 
            };

            string sonuc = accountService.CreateAccount(yeniHesap);
            Console.WriteLine(sonuc);
            break;

        case "2":
            Console.Write("Para yatırmak istediğiniz Hesap ID: ");
            int accountId = int.Parse(Console.ReadLine());

            Console.Write("Yatırılacak Tutar: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            string depositResult = accountService.DepositMoney(accountId, amount);
            Console.WriteLine(depositResult);
            break;

        case "3":
            Console.WriteLine("\n--- Para Transferi ---");
            Console.Write("Gönderen Hesap ID: ");
            int senderId = int.Parse(Console.ReadLine());

            Console.Write("Alıcı Hesap ID: ");
            int receiverId = int.Parse(Console.ReadLine());

            Console.Write("Gönderilecek Tutar: ");
            decimal transferAmount = decimal.Parse(Console.ReadLine());

            string transferResult = accountService.TransferMoney(senderId, receiverId, transferAmount);
            Console.WriteLine("\n" + transferResult);
            break;

        case "4":
            var accounts = accountService.GetAllAccounts();
            Console.WriteLine("\n--- Kayıtlı Hesaplar ---");
            foreach (var acc in accounts)
            {
                Console.WriteLine($"ID: {acc.Id} | IBAN: {acc.IBAN} | Bakiye: {acc.Balance} {acc.CurrencyType}");
            }
            break;
        case "5":
            Console.Write("İşlem geçmişini görmek istediğiniz Hesap ID: ");
            int historyAccountId = int.Parse(Console.ReadLine());

            var history = accountService.GetTransactionHistory(historyAccountId);

            Console.WriteLine($"\n--- ID: {historyAccountId} Nolu Hesabın İşlem Geçmişi ---");
            Console.WriteLine("Tarih       | Tür      | Tutar     | Açıklama");
            Console.WriteLine("---------------------------------------------------------");

            foreach (var item in history)
            {
                string date = item.TransactionDate.ToShortDateString();
                Console.WriteLine($"{date} | {item.Type,-8} | {item.Amount,8} TL | {item.Description}");
            }
            break;

        case "0":
            devamEdilsinMi = false;
            Console.WriteLine("Bizi tercih ettiğiniz için teşekkürler!");
            break;

        default:
            Console.WriteLine("Geçersiz seçim, lütfen tekrar deneyin.");
            break;
    }
}