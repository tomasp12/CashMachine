namespace CashMachine.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? FromCardNumber { get; set; }
        public string? ToCardNumber { get; set; }
        public double Amount { get; set; }

    }
}
