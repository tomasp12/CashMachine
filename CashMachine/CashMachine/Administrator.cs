namespace CashMachine
{
    public class Administrator
    {
        public Administrator() 
        {
            Console.Clear();
            Console.WriteLine("Admin Console");
            Console.Write("Id");
            var id = Guid.NewGuid();
            Console.WriteLine(id);
            Console.Write("Enter your password: ");
            var userPassword = Security.GetHashString(Console.ReadLine()!);
            Console.WriteLine(userPassword);
            // Console.Write("Enter amount: ");
            // int amount = int.Parse(Console.ReadLine()!);
            // Console.Write("Admin user(y/n): ");
            // bool admin = Console.ReadLine()! == "y"? true :false;
            // Bank bank = new();
            
            Console.ReadKey();
        }
    }
}
