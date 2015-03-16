using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Account
    {

        public const int CHECKING = 0;
        public const int SAVINGS = 1;
        public const int MAXI_SAVINGS = 2;

        private readonly int accountType;
        public List<Transaction> transactions;

        public Account(int accountType) 
        {
            this.accountType = accountType;
            this.transactions = new List<Transaction>();
        }

        public void Deposit(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(amount));
            }
        }

        public void Withdraw(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(-amount));
            }
        }

        public double InterestEarned() 
        {
            //BM:  Changing the interest calculation to annual would entail changing sumTransactions() to return a collection of transactions
            //     looping through the collection of transaction and calculating the daily balance and
            //     then calculating the interest on a daily basis by looping through the criteria below. 
            double amount = sumTransactions();
            
            //Calculating the Interest Earned . BM
            double interest = 0.00;


            switch(accountType){
                case SAVINGS:
                    if (amount <= 1000)
                        interest =  amount * 0.001;
                    else
                        interest =  1 + (amount-1000) * 0.002;
    //            case SUPER_SAVINGS:
    //                if (amount <= 4000)
    //                    return 20;
                    break;
                case MAXI_SAVINGS:
                                           
                    if (amount <= 1000)
                        interest =  amount * 0.02;
                    if (amount <= 2000)
                        if (transactions.FindAll(FindTrans).Find(x => x.amount < 0) == null)
                            interest = 20 + (amount - 1000) * 0.05;
                        else
                            interest = 20 + (amount - 1000) * 0.01;
                    else
                        interest = 70 + (amount - 2000) * 0.1;
                    break;
                default:
                    interest = amount * 0.001;
                    break;
            }
            return interest;
        }

        private bool FindTrans(Transaction tran)
        {
            return tran.transactionDate.CompareTo(DateTime.Now.AddDays(-10)) > 1;
        }


        public double sumTransactions() {
           return CheckIfTransactionsExist(true);
        }

        private double CheckIfTransactionsExist(bool checkAll) 
        {
            double amount = 0.0;
            foreach (Transaction t in transactions)
                amount += t.amount;
            return amount;
        }

        public int GetAccountType() 
        {
            return accountType;
        }

    }
}
