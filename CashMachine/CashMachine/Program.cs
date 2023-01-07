using CashMachine;
using CashMachine.Models;
using CashMachine.Config;
//Login data:
//      User:24a4096e-f012-49eb-8525-e3f03a332e84
//  Password:1234
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
using Bank bank = new();
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
            cashMachineTerminal.DisplayWarning("Too many login attempts");
            
            return;
        }
        cashMachineTerminal.DisplayWarning("Invalid Login");
        login = cashMachineTerminal.DisplayLoginMenu();
    }
    var account = bank.GetAccount(login.User!);
    cashMachineTerminal.DisplayMenu(account);
}