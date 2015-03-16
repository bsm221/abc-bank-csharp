using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace abc_bank
{
    public class Customer
    {
        private String name;
        private List<Account> accounts;

        public List<Account> Accounts
        {
            get
            {
                return accounts;
            }
        }
        public Customer(String name)
        {
            this.name = name;
            this.accounts = new List<Account>();
        }

        public String GetName()
        {
            return name;
        }

        public Customer OpenAccount(Account account)
        {
            accounts.Add(account);
            return this;
        }

        public int GetNumberOfAccounts()
        {
            return accounts.Count;
        }

        public double TotalInterestEarned() 
        {
            double total = 0;
            foreach (Account a in accounts)
                total += a.InterestEarned();
            return total;
        }

        public String GetStatement() 
        {
            //String statement = null;
            StringBuilder statement = new StringBuilder();

            statement.Clear();
            statement.AppendLine("Statement for " + name);
            double total = 0.0;
            foreach (Account a in accounts) 
            {
                statement.AppendLine(String.Empty);
                statement.AppendLine(statementForAccount(a));
                total += a.sumTransactions();
            }
            statement.AppendLine(String.Empty);
            statement.Append("Total In All Accounts " + ToDollars(total));
            
            statement.Replace("\r", string.Empty);
            return statement.ToString();
        }

        private String statementForAccount(Account a) 
        {
            //String sb = "";
            StringBuilder sb = new StringBuilder();

           //Translate to pretty account type
            switch(a.GetAccountType()){
                case Account.CHECKING:
                    sb.AppendLine("Checking Account");
                    break;
                case Account.SAVINGS:
                    sb.AppendLine("Savings Account");
                    break;
                case Account.MAXI_SAVINGS:
                    sb.AppendLine("Maxi Savings Account");
                    break;
            }

            //Now total up all the transactions
            double total = 0.0;
            foreach (Transaction t in a.transactions) {
                sb.AppendLine( "  " + (t.amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(Math.Abs(t.amount)) );
                total += t.amount;
            }
            sb.Append("Total " + ToDollars(total));
            sb.Replace("\r", string.Empty);
            return sb.ToString();
        }

        private String ToDollars(double d)
        {
            return d.ToString("C", CultureInfo.CurrentCulture);
            //return String.Format("$%,.2f", Math.Abs(d));
        }

        public bool Transfer(Account from, Account to, double amount)
        {
            bool IsTransferred = true;

            if (from.sumTransactions() >= amount)
            {
                from.Withdraw(amount);
                to.Deposit(amount);
            }
            else
                IsTransferred = false;

            return IsTransferred;
        }

    }
}
