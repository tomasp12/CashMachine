using CashMachine.Models;

namespace CashMachine
{
    public class CardNumberRule : ILoginRule
    {
       
        public bool Check(string cardNumber, string passwordHash)
        {
            return Guid.TryParse(cardNumber, out var x) ? true : false;
        }
    }
}
