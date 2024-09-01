using BankingSystem.Data;
using BankingSystem.Enums;
using System.Runtime.CompilerServices;

namespace BankingSystem.Entites
{
    public class HolderAccount : Account
    {

        public decimal Balance { get; set; }
        public string HolderNId { get; set; }
        public Holder Holder { get; set; }
        public List<TransactionLog>? TransactionLogs { get; set; } = new();
        public DeleteState DeleteState { get; set; } = DeleteState.NotDeleted;

        public void Deposit(decimal amount, bool isTransfer)
        {
            var transType = TransactionType.Deposit;
            if (amount > 0)
            {
                Balance += amount;
            }

            if (!isTransfer)
            {
                TransactionLogs.Add(new TransactionLog(amount, transType, DateTime.Now));
            }
        }

        public void Withdraw(decimal amount, bool isTransfer)
        {
            var transType = TransactionType.Withdraw;
            if (amount > 0 && amount <= (Balance - 100))
            {
                Balance -= amount;
            }

            else if (amount > (Balance - 100))
                Console.WriteLine("You must Keep 100$ At least! Try again..");
            else if (amount < 0)
                Console.WriteLine("Invalid Amount! Try again..");

            if (!isTransfer)
            {
                TransactionLogs.Add(new TransactionLog(amount, transType, DateTime.Now));
            }
        }

        public void Transfer(AppDbContext context, HolderAccount to, decimal amount)
        {
            var transType = TransactionType.Transfer;
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Withdraw(amount, true);
                    to.Deposit(amount, true);

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
                TransactionLogs.Add(new TransactionLog(amount, transType, DateTime.Now, Username, to.Username));
            }
        }
    }
}
