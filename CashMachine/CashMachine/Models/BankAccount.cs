namespace CashMachine.Model
{
    public class BankAccount
    { 
        public string CardNumber { get; set; }
        public string PasswordHash { get; set; }
        public int Balance { get; set; }
        public List<string> Transactions { get; set; }
        public BankAccount(string cardNumber, string passwordHash, int balance, List<string> transactions)
        {
            CardNumber = cardNumber;
            PasswordHash = passwordHash;
            Balance = balance;
            Transactions = transactions;
        }
    }
}
