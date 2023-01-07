using System.Globalization;
using CashMachine.Config;
using CashMachine.Models;
using Microsoft.Data.Sqlite;

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
             ExecuteNonQuery($"INSERT INTO BankTransaction (`Date`, `FromCardNumber`, `ToCardNumber`, Amount) VALUES " +
                             $"('{DateTime.Now:s}','{transaction.FromCardNumber}', '{transaction.ToCardNumber}', {transaction.Amount})"
                             );
        }

        public List<Transaction> GetListOfLastTransaction(string cardNumber)
        {

            List<Transaction> transactions = new();
            var results = ExecuteReader($"SELECT * FROM BankTransaction WHERE FromCardNumber = '{cardNumber}' ORDER BY Date DESC LIMIT 5;");
            while (results.Read())
            {
                transactions.Add(new Transaction
                {
                    Date = DateTime.Parse(results["Date"].ToString()!, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                    FromCardNumber = results["FromCardNumber"].ToString(),
                    ToCardNumber = results["ToCardNumber"].ToString(),
                    Amount = Convert.ToInt32(results["Amount"])
                });
            }
            return transactions;
        }

        public int GetCountOfTransactionToday(string cardNumber)
        {
            var startDate = DateTime.Today;
            var endDate = DateTime.Today.AddDays(1).AddTicks(-1);

            var results = ExecuteReader($"SELECT * FROM BankTransaction " +
                                                      $"WHERE FromCardNumber = '{cardNumber}' " +
                                                      $"AND Date BETWEEN '{startDate:s}' AND '{endDate:s}';"
                                                      );
            return results.Cast<object>().Count();
        }

        private void ExecuteNonQuery(string sqlList)
        {
            var myCommand = new SqliteCommand("", _myConnection)
            {
                CommandText = sqlList
            };
            myCommand.ExecuteNonQuery();
        }

        private SqliteDataReader ExecuteReader(string sql)
        {
            var myCommand = new SqliteCommand("", _myConnection)
            {
                CommandText = sql
            };
            return myCommand.ExecuteReader();
        }

        public void Dispose()
        {
            _myConnection.Close();
        }
    }
}
