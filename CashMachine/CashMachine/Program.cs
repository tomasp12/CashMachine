using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using CashMachine;
using CashMachine.Models;
using CashMachine.Config;

// Creating Database if not exist
if (!File.Exists(Constants.OperationDataBase))
{ 
    using Database dataBase = new();
    dataBase.CreateInitialData();
}

// Creating Account file if not exist
if (!File.Exists(Constants.AccountsFile))
{
    using StreamWriter sw = new(Constants.AccountsFile);
    sw.WriteLine(Constants.AccountsFileInfo);
}

//Main program
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