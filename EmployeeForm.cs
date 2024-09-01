using BankingSystem.Data;
using BankingSystem.Entites;
using BankingSystem.Enums;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem
{
    public partial class Program
    {
        public class EmployeeForm : IForm
        {
            public void LoadForm(Account account, ref bool logout)
            {
                EmployeeAccount? empAccount = account as EmployeeAccount;

                using AppDbContext context = new AppDbContext();
                context.Attach(empAccount);

                Console.Clear();
                Console.WriteLine("\t\t\t\t\t  M47 Bank\n\t\t\t\t\t▀▀▀▀▀▀▀▀▀▀▀▀");
                Console.WriteLine($"user: {empAccount?.Username} | type: employee");

                Console.WriteLine(
                    $"\n1) History Logs\n" +
                    $"2) Accounts Requested To Delete\n" +
                    $"3) Logout");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 5)
                {
                    Console.Clear();
                    Console.WriteLine("\t\t\t\t\t  M47 Bank\n\t\t\t\t\t▀▀▀▀▀▀▀▀▀▀▀▀");
                    Console.WriteLine($"user: {empAccount?.Username} | type: employee");

                    Console.WriteLine(
                    $"\n1) History Logs\n" +
                    $"2) Accounts Requested To Delete\n" +
                    $"3) Logout");
                    Console.WriteLine("\nChoose Valid Option Please!");
                }

                using var context2 = new AppDbContext();

                switch (choice)
                {
                    //All Transaction Logs
                    case 1:
                        Displaying.DisplayHistoryLog(context.TransactionLogs.AsNoTracking().ToList().OrderBy(tl => tl.CreatedAt));
                        break;

                    //Requested Accounts
                    case 2:
                        var accounts = context2.HolderAccounts.Where(ha => ha.DeleteState == DeleteState.Requested).ToList();
                        foreach (var item in accounts)
                        {
                            Console.WriteLine($"{item.Id}| {item.Username}, {item.Balance}, {item.CreatedAt}");
                        }

                        Console.WriteLine("select account Id to confirm delete: ");

                        int haId;

                        while (!int.TryParse(Console.ReadLine(), out haId) || haId < 0 || context2.HolderAccounts.FirstOrDefault(ha => ha.Id == haId).DeleteState != DeleteState.Requested)
                        {
                            Console.WriteLine("Invalid! Try Again..");
                            Console.Write("Enter Valid Id: ");
                        }


                        Console.WriteLine("are you sure to delete the account? (Y/N)");
                        var result = Console.ReadLine();

                        while (!(result == "Y" || result == "y" || result == "n" || result == "N"))
                        { Console.Write("Try Again: "); result = Console.ReadLine(); }

                        if (result.Equals("Y") || result.Equals("y"))
                        {
                            var selectedAccount = context2.HolderAccounts.FirstOrDefault(ha => ha.Id == haId);
                            selectedAccount.DeleteState = DeleteState.Deleted;

                            context2.SaveChanges();

                            Console.WriteLine($"\t [{selectedAccount.Username}] Account Has Deleted Successfully!!");
                            Console.Write("Press Any Key to Continue..");
                            Console.ReadKey();
                            Console.Clear();
                        }

                        else if (result.Equals("N") || result.Equals("N"))
                            break;

                        break;

                    //Logout
                    case 3:
                        Console.WriteLine("Are you sure to logout? (Y/N)");
                        var result2 = Console.ReadLine();

                        while (!(result2 == "Y" || result2 == "y" || result2 == "n" || result2 == "N"))
                        { Console.Write("Try Again: "); result2 = Console.ReadLine(); }

                        if (result2.Equals("Y") || result2.Equals("y"))
                        {
                            logout = true;
                            activeAccount = null;
                            Console.Clear();
                        }

                        else if (result2.Equals("N") || result2.Equals("N"))
                            logout = false;
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
