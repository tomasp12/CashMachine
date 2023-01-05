using CashMachine.Config;
using CashMachine.Models;
using Microsoft.Data.Sqlite;
using System.Globalization;

namespace CashMachine
{
    public class OperationDataRepository : IDisposable
    {
        private readonly SqliteConnection _myConnection;
        public OperationDataRepository()
        {
            _myConnection = new SqliteConnection(Constants.ConnectionString);
            _myConnection.Open();

        }

        public void AddTransaction(Transaction transaction)
        {
            ExecuteNonQuery("INSERT INTO BankTransaction (`Date`, `FromCardNumber`, `ToCardNumber`, Amount) VALUES " +
                            $"('{DateTime.Now.ToString("s")}','{transaction.FromCardNumber}', '{transaction.ToCardNumber}', {transaction.Amount})");
        }

        private void ExecuteNonQueryForList(List<string> sqlList)
        {
            var myCommand = new SqliteCommand("", _myConnection);
            foreach (string sql in sqlList)
            {
                myCommand.CommandText = sql;
                myCommand.ExecuteNonQuery();
            }
        }
        private void ExecuteNonQuery(string sqlList)
        {
            var myCommand = new SqliteCommand("", _myConnection);
            myCommand.CommandText = sqlList;
            myCommand.ExecuteNonQuery();
        }
        public void Dispose()
        {
            _myConnection.Close();
        }

        public List<Transaction> GetListOfLastTransaction(string cardNumber)
        {
            
            List<Transaction> transactions = new List<Transaction>();
            var results = ExecuteReader($"SELECT FromCardNumber = '{cardNumber}' FROM BankTransaction ORDER BY Date DESC LIMIT 5;");
            while (results.Read())
            {
                Console.WriteLine(results.GetString(0));
                // transactions.Add(new Transaction
                // {
                //     //Date = DateTime.Parse(results["Date"].ToString(), CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                //     FromCardNumber = results["FromCardNumber"].ToString(),
                //     ToCardNumber = results["ToCardNumber"].ToString(),
                //     Amount = Convert.ToInt32(results["Amount"])
                // });
            }
            return transactions;
        }

        private SqliteDataReader ExecuteReader(string sql)
        {
            var myCommand = new SqliteCommand("", _myConnection);
            myCommand.CommandText = sql;
            return myCommand.ExecuteReader();
        }
    }
}
