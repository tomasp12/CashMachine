using CashMachine.Models;

namespace CashMachine
{
    public class CashMachineTerminal
    {
        public void DisplayMenu(BankAccount account)
        {
            var exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Welcome! What would you like to do?");
                Console.WriteLine("1. View balance");
                Console.WriteLine("2. View recent transactions");
                Console.WriteLine("3. Withdraw cash");
                Console.WriteLine("4. Log out");
                Console.Write("Enter a number: ");
                var choice = Console.ReadLine()!;
                switch (choice)
                {
                    case "1":
                        DisplayWarning($"Your balance is {account.Balance}");
                        break;
                    case "2":
                        ShowTransaction(account.CardNumber);
                        break;
                    case "3":
                        WithdrawCash(account);
                        break;
                    case "4":
                        exit = true;
                        break;
                    case "000-005":
                        if (account.Admin )
                        {
                            var administrator = new Administrator();
                        }
                        break;
                }
            }
        }

        private void ShowTransaction(string cardNumber)
        {
            Console.Clear();
            Console.WriteLine("Last 5 transaction:");
            using OperationDataRepository repository = new();
            var transactionsList = repository.GetListOfLastTransaction(cardNumber);
            foreach (var transaction in transactionsList)
            {
                DisplayWarning($"{transaction.Amount} Eur, at {transaction.Date:MM/dd/yy HH/mm} from: {transaction.FromCardNumber} ");
            }
            Console.ReadKey();
        }

        private void WithdrawCash(BankAccount account)
        {
            Console.Clear();
            using OperationDataRepository repository = new();
            Console.Write("Enter amount to withdraw: ");
            var amount = int.Parse(Console.ReadLine()!);
            if (amount > 1000)
            {
                DisplayWarning("Maximum withdrawal amount is 1000");
            }
            else if (repository.GetCountOfTransactionToday(account.CardNumber) >= 10)
            {
                DisplayWarning("Maximum number of transactions per day exceeded");
            }
            else if (account.Balance < amount)
            {
                DisplayWarning("Insufficient balance");
            }
            else
            {
                WithdrawCashProcess(account, amount);
            }
            Console.Clear();
        }

        private void WithdrawCashProcess(BankAccount account, int amount)
        {
            account.Balance -= amount;
            var transaction = new Transaction
            {
                Amount = -amount,
                FromCardNumber = account.CardNumber,
                ToCardNumber = account.CardNumber
            };
            using OperationDataRepository repository = new();
            repository.AddTransaction(transaction);
            DisplayWarning("Transaction successful");
        }

        public LoginCredentials DisplayLoginMenu()
        {
            Console.Clear();
            Console.Write("Enter your card number: ");
            var userId = Console.ReadLine()!;
            Console.Write("Enter your password: ");
            var userPassword = Security.GetHashString(Console.ReadLine()!);
            return new LoginCredentials(userId, userPassword);
        }

        public void DisplayWarning(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            Console.ReadKey();
        }
    }
}

