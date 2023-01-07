using CashMachine.Config;
using Microsoft.Data.Sqlite;
using System.Data.Common;
using CashMachine.Models;
using SQLitePCL;

namespace CashMachine;

public class Database : IDisposable
{
    private  SqliteConnection _myConnection;

    public Database()
    {
        _myConnection = new SqliteConnection(Constants.ConnectionString);
        _myConnection.Open();
    }

    public void CreateInitialData()
    {
        CreateTable("BankTransaction", "Id INTEGER PRIMARY KEY, Date TEXT , FromCardNumber TEXT, ToCardNumber TEXT, Amount INTEGER");
        ExecuteNonQueryForList(BankTransactionInitialData());
    }
    
    private void CreateTable(string tableName, string tableColumns)
    {
        var myCommand = new SqliteCommand("", _myConnection);
        myCommand.CommandText = $"CREATE TABLE {tableName} ({tableColumns})";
        myCommand.ExecuteNonQuery();
    }

    private void ExecuteNonQueryForList(List<string> sqlList)
    {
        var myCommand = new SqliteCommand("", _myConnection);
        foreach (var sql in sqlList)
        {
            myCommand.CommandText = sql;
            myCommand.ExecuteNonQuery();
        }
    }

    private List<string> BankTransactionInitialData()
    {
        List<string> sqlList = new()
        {
            $"INSERT INTO BankTransaction (`Date`, `FromCardNumber`, `ToCardNumber`, Amount) VALUES " +
            $"('{DateTime.Now.ToString("s")!}','24a4096e-f012-49eb-8525-e3f03a332e84', '24a4096e-f012-49eb-8525-e3f03a332e84', -100)",
            $"INSERT INTO BankTransaction (`Date`, `FromCardNumber`, `ToCardNumber`, Amount) VALUES " +
            $"('{DateTime.Now.ToString("s")!}','24a4096e-f012-49eb-8525-e3f03a332e84', '24a4096e-f012-49eb-8525-e3f03a332e84', -10)",
            $"INSERT INTO BankTransaction (`Date`, `FromCardNumber`, `ToCardNumber`, Amount) VALUES " +
            $"('{DateTime.Now.ToString("s")!}','24a4096e-f012-49eb-8525-e3f03a332e84', '24a4096e-f012-49eb-8525-e3f03a332e84', -200)",
            $"INSERT INTO BankTransaction (`Date`, `FromCardNumber`, `ToCardNumber`, Amount) VALUES " +
            $"('{DateTime.Now.ToString("s")!}','24a4096e-f012-49eb-8525-e3f03a332e84', '24a4096e-f012-49eb-8525-e3f03a332e84', -40)",
            $"INSERT INTO BankTransaction (`Date`, `FromCardNumber`, `ToCardNumber`, Amount) VALUES " +
            $"('{DateTime.Now.ToString("s")!}','24a4096e-f012-49eb-8525-e3f03a332e84', '24a4096e-f012-49eb-8525-e3f03a332e84', -40)",
            $"INSERT INTO BankTransaction (`Date`, `FromCardNumber`, `ToCardNumber`, Amount) VALUES " +
            $"('{DateTime.Now.ToString("s")!}','24a4096e-f012-49eb-8525-e3f03a332e84', '24a4096e-f012-49eb-8525-e3f03a332e84', -50)",
            $"INSERT INTO BankTransaction (`Date`, `FromCardNumber`, `ToCardNumber`, Amount) VALUES " +
            $"('{DateTime.Now.ToString("s")!}','24a4096e-f012-49eb-8525-e3f03a332e84', '24a4096e-f012-49eb-8525-e3f03a332e84', -50)",
            $"INSERT INTO BankTransaction (`Date`, `FromCardNumber`, `ToCardNumber`, Amount) VALUES " +
            $"('{DateTime.Now.ToString("s")!}','24a4096e-f012-49eb-8525-e3f03a332e84', '24a4096e-f012-49eb-8525-e3f03a332e84', -10)",
            $"INSERT INTO BankTransaction (`Date`, `FromCardNumber`, `ToCardNumber`, Amount) VALUES " +
            $"('{DateTime.Now.ToString("s")!}','24a4096e-f012-49eb-8525-e3f03a332e84', '24a4096e-f012-49eb-8525-e3f03a332e84', -10)",
            $"INSERT INTO BankTransaction (`Date`, `FromCardNumber`, `ToCardNumber`, Amount) VALUES " +
            $"('{DateTime.Now.ToString("s")!}','6e51d135-a870-4854-84e6-85e170f37634', '6e51d135-a870-4854-84e6-85e170f37634', -500)",
            $"INSERT INTO BankTransaction (`Date`, `FromCardNumber`, `ToCardNumber`, Amount) VALUES " +
            $"('{DateTime.Now.ToString("s")!}','6e51d135-a870-4854-84e6-85e170f37634', '6e51d135-a870-4854-84e6-85e170f37634', -400)",
            $"INSERT INTO BankTransaction (`Date`, `FromCardNumber`, `ToCardNumber`, Amount) VALUES " +
            $"('{DateTime.Now.ToString("s")!}','6e51d135-a870-4854-84e6-85e170f37634', '6e51d135-a870-4854-84e6-85e170f37634', -100)",
            $"INSERT INTO BankTransaction (`Date`, `FromCardNumber`, `ToCardNumber`, Amount) VALUES " +
            $"('{DateTime.Now.ToString("s")!}','6e51d135-a870-4854-84e6-85e170f37634', '6e51d135-a870-4854-84e6-85e170f37634', -300)"
        };
        return sqlList;
    }

    public void Dispose()
    {
        _myConnection.Close();
    }
}