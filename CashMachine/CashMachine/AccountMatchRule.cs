using CashMachine.Models;

namespace CashMachine
{
    public class AccountMatchRule : ILoginRule
    {
        private Dictionary<string, BankAccount> _accounts;

        public AccountMatchRule(Dictionary<string, BankAccount> accounts)
        {
            _accounts=accounts;
        }
        public bool Check(string cardNumber, string passwordHash)
        {
            if (!_accounts.ContainsKey(cardNumber))
            {
                return false;
            }
            else
            {
                return _accounts[cardNumber].PasswordHash == passwordHash;
            }
        }
    }
}
