namespace CashMachine.Models
{
    public class BankAccount
    { 
        public string CardNumber { get; set; }
        public string PasswordHash { get; set; }
        public int Balance { get; set; }
        public bool Admin { get; set; }

        public BankAccount(string cardNumber, string passwordHash, int balance, bool admin)
        {
            CardNumber = cardNumber;
            PasswordHash = passwordHash;
            Balance = balance;
            Admin = admin;
        }
    }
}
