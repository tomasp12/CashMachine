using CashMachine.Models;
using System.Security.Principal;

namespace CashMachine
{
    public class CashMachineTerminal
    {
        public CashMachineTerminal()
        {

        }
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
                        Console.Clear();
                        Console.WriteLine($"Your balance is {account.Balance}");
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Clear();
                        ShowTransaction(account.CardNumber);
                        Console.ReadKey();
                        break;
                    case "3":
                        Console.Clear();
                        WithdrawCash(account);
                        Console.ReadKey();
                        break;
                    case "4":
                        Console.Clear();
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
            Console.WriteLine("Last 5 transaction:");
            // Show 5 last transaction from db
        }

        private void WithdrawCash(BankAccount account)
        {
            Console.Write("Enter amount to withdraw: ");
            int amount = int.Parse(Console.ReadLine()!);
            if (amount > 1000)
            {
                Console.WriteLine("Maximum withdrawal amount is 1000");
            }
            else
            {
                if (account.Balance < amount)
                {
                    Console.WriteLine("Insufficient balance");
                }
                else
                {
                    account.Balance -= amount;
                    //add transaction
                    Console.WriteLine("Transaction successful");
                }
            }
        }

        public LoginCredentials DisplayLoginMenu()
        {
            Console.Write("Enter your card number(Empty to default Guid value): ");
            var userId = Console.ReadLine()!;
            userId = string.IsNullOrEmpty(userId) ? "24a4096e-f012-49eb-8525-e3f03a332e84" : userId;
            Console.Write("Enter your password(valid 1234): ");
            var userPassword = Security.GetHashString(Console.ReadLine()!);
            return new LoginCredentials(userId, userPassword);
        }
    }
}

