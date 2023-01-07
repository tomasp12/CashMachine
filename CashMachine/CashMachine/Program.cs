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
    dataBase.InitialDataBase();
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
    var loginData = cashMachineTerminal.DisplayLoginMenu();
    var attempt = 1;
    while (!bank.Login(loginData))
    {
        attempt++;
        if (attempt > 3)
        {
            cashMachineTerminal.DisplayWarning("Too many login attempts.");
            return;
        }
        cashMachineTerminal.DisplayWarning("Invalid Login.");
        loginData = cashMachineTerminal.DisplayLoginMenu();
    }
    var accountData = bank.GetAccount(loginData.User!);
    cashMachineTerminal.DisplayMenu(accountData);
}