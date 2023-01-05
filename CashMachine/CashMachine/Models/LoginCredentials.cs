using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashMachine.Models
{
    public class LoginCredentials
    {
        public string? Password { get; set; }
        public string? User { get; set;}
        public LoginCredentials(string user, string password)
        {
            Password = password;
            User = user;
        }
    }
}
