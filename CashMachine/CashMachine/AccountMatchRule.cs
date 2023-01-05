namespace CashMachine
{
    public class AccountMatchRule : ILoginRule
    {
        public bool Check(string cardNumber, string passwordHash)
        {
            throw new NotImplementedException();
        }
    }
}
