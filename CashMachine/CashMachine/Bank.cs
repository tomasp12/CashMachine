﻿using CashMachine.Models;

namespace CashMachine
{
    public class Bank
    {
        private readonly Dictionary<string, BankAccount> _accounts = new();
        private readonly List<ILoginRule> _loginRules = new();
        public void LoadAccounts()
        {
            using StreamReader sr = new("../../../accounts.txt");
            while (!sr.EndOfStream)
            {
                var parts = sr.ReadLine()!.Split(',');
                var cardNumber = parts[0];
                var pwHash = parts[1];
                var balance = int.Parse(parts[2]);
                var admin = bool.Parse(parts[3]);
                _accounts[cardNumber] = new BankAccount(cardNumber, pwHash, balance, admin);
            }
            _loginRules.Add(new CardNumberRule());
            _loginRules.Add(new AccountMatchRule(_accounts));
        }

        public void SaveAccounts()
        {
            using StreamWriter sw = new("../../../accounts.txt");
            foreach (var pair in _accounts)
            {
                BankAccount account = pair.Value;
                sw.WriteLine($"{account.CardNumber},{account.PasswordHash},{account.Balance},{account.Admin}");
            }
        }
        public BankAccount GetAccount(string cardNumber)
        {
            return _accounts[cardNumber];
        }

        public bool Login(LoginCredentials login)
        {
            return _loginRules.All(rule => rule.Check(login.User!, login.Password!));
        }
    }
}
