using CashMachine.Config;
using CashMachine.Models;

namespace CashMachine
{
    public class Bank : IDisposable
    {
        private readonly Dictionary<string, BankAccount> _accounts = new();
        private readonly List<ILoginRule> _loginRules = new();

        public Bank()
        {
            LoadAccounts();
        }

        private void LoadAccounts()
        {
            using StreamReader sr = new(Constants.AccountsFile);
            while (!sr.EndOfStream)
            {
                var parts = sr.ReadLine()!.Split(',');
                var cardNumber = parts[0];
                var pwHash = parts[1];
                var balance = int.Parse(parts[2]);
                var admin = bool.Parse(parts[3]);
                _accounts[cardNumber] = new BankAccount(cardNumber, pwHash, balance, admin);
            }
            _loginRules.Add(new CardNumberRule());
            _loginRules.Add(new AccountMatchRule(_accounts));
        }

        private void SaveAccounts()
        {
            using StreamWriter sw = new(Constants.AccountsFile);
            foreach (var account in _accounts.Select(x => x.Value))
            {
                sw.WriteLine($"{account.CardNumber},{account.PasswordHash},{account.Balance},{account.Admin}");
            }
        }
        public BankAccount GetAccount(string cardNumber)
        {
            return _accounts[cardNumber];
        }

        public bool Login(LoginCredentials login)
        {
            return _loginRules.All(rule => rule.Check(login.User!, login.Password!));
        }

        public void Dispose()
        {
            SaveAccounts();
        }
    }
}
