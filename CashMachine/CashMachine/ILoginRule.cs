namespace CashMachine
{
    public interface ILoginRule
    {
        bool Check(string cardNumber, string passwordHash);
    }
}
