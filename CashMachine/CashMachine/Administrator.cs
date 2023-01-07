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
            Console.ReadKey();
        }
    }
}
