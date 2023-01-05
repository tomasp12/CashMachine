using CashMachine;
using CashMachine.Models;

Bank bank = new();
bank.LoadAccounts();
CashMachineTerminal cashMachineTerminal = new();

while (true)
{
    var login = cashMachineTerminal.DisplayLoginMenu();
    var attempt = 1;
    while (!bank.Login(login))
    {
        attempt++;
        if (attempt > 3)
        {
            Console.Clear();
            Console.WriteLine("Too many login attempts");
            Console.ReadKey();
            return;
        }
        Console.Clear();
        Console.WriteLine("Invalid Login");
        login = cashMachineTerminal.DisplayLoginMenu();
    }
    var account = bank.GetAccount(login.User!);
    cashMachineTerminal.DisplayMenu(account);
    bank.SaveAccounts();
}