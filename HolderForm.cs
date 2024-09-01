using BankingSystem.Data;
using BankingSystem.Entites;
using BankingSystem.Enums;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem
{
    public partial class Program
    {
        public class HolderForm : IForm
        {
            public void LoadForm(Account account, ref bool logout)
            {
                var holderAccount  = account as HolderAccount;

                using var context = new AppDbContext();
                context.Attach(holderAccount);

                if (holderAccount.DeleteState == DeleteState.Requested)
                {
                    Console.WriteLine("Your Account Is Pending To Delete.. press any key to log out..");
                    Console.ReadKey();

                    logout = true;
                    activeAccount = null;
                    Console.Clear();

                    return;
                }

                Console.Clear();
                Console.WriteLine("\t\t\t\t\t  M47 Bank\n\t\t\t\t\t▀▀▀▀▀▀▀▀▀▀▀▀");
                Console.WriteLine($"user: {holderAccount.Username} | type: client \t\t Your Balance: {holderAccount.Balance:C}\n────────────");

                Console.WriteLine(
                   $"1) Deposit\n" +
                   $"2) Withdraw\n" +
                   $"3) Transfer Money\n" +
                   $"4) Manage Your Account\n" +
                   $"5) Logout");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 5)
                {
                    Console.Clear();
                    Console.WriteLine("\t\t\t\t\t  M47 Bank\n\t\t\t\t\t▀▀▀▀▀▀▀▀▀▀▀▀");
                    Console.WriteLine($"user: {holderAccount.Username} | type: client \t\t Your Balance: {holderAccount.Balance:C}\n────────────");

                    Console.WriteLine(
                    $"1) Deposit\n" +
                    $"2) Withdraw\n" +
                    $"3) Transfer Money\n" +
                    $"4) Manage Your Account\n" +
                    $"5) Logout");
                    Console.WriteLine("\nChoose Valid Option Please!");
                }

                switch (choice)
                {
                    //Deposit
                    case 1:
                        Console.WriteLine("Amount to Deposit (0 to cancel):");
                        while (true)
                        {
                            decimal amountToDeposit;
                            while (!decimal.TryParse(Console.ReadLine(), out amountToDeposit))
                            {
                                Console.WriteLine("Invalid Amount!, Try again..");
                                Console.WriteLine("Amount to Deposit (press 0 to cancel): ");
                            }
                            if (amountToDeposit == 0)
                                break;

                            holderAccount.Deposit(amountToDeposit, isTransfer: false);

                            if (context.SaveChanges() > 0)
                            {
                                Console.WriteLine($"\t ({amountToDeposit:C}) deposited successfully!!");
                                Thread.Sleep(3000);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Amount!, Try again..");
                                Console.WriteLine("Amount to Deposit (press 0 to cancel): ");
                            }
                        }
                        break;

                    //Withdraw
                    case 2:
                        Console.WriteLine("Amount to Withdraw (press 0 to cancel):");

                        while (true)
                        {
                            decimal amountToWithdraw;
                            while (!decimal.TryParse(Console.ReadLine(), out amountToWithdraw))
                            {
                                Console.WriteLine("Amount to Withdraw (press 0 to cancel): ");
                            }

                            if (amountToWithdraw == 0)
                                break;

                            holderAccount.Withdraw(amountToWithdraw, false);

                            if (context.SaveChanges() > 0)
                            {
                                Console.WriteLine($"\t ({amountToWithdraw:C}) withdrawn Successfully!!");
                                Thread.Sleep(3000);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Amount to Withdraw (press 0 to cancel): ");
                            }
                        }
                        break;

                    //Transfer Money
                    case 3:
                        Console.WriteLine("Enter the Username of Account that you will transfer to, (press 0 to cancel): ");

                        string username = Console.ReadLine();

                        if (username == "0")
                            break;

                        while (username != "0")
                        {
                            if (context.HolderAccounts.Any(ha => ha.Username.Equals(username) && !(username.Equals(holderAccount.Username)) && ha.DeleteState == DeleteState.NotDeleted))
                                break;

                            Console.WriteLine("User not Found!, Try Again (press 0 to cancel): ");
                            username = Console.ReadLine();
                        }
                        if (username == "0")
                            break;

                        var userToTransfer = context.HolderAccounts.First(ha => ha.Username.Equals(username));

                        Console.WriteLine("Amount to Transfer (press 0 to cancel):");
                        decimal amountToTransfer;

                        while (!decimal.TryParse(Console.ReadLine(), out amountToTransfer))
                        {
                            Console.WriteLine("Amount to Transfer (press 0 to cancel): ");
                        }
                        if (amountToTransfer == 0)
                            break;

                        holderAccount.Transfer(context, userToTransfer, amountToTransfer);

                        if (context.SaveChanges() > 0)
                        {
                            Console.WriteLine($"{amountToTransfer:C} Transfered to {userToTransfer.Username} Successfully!!");
                            Thread.Sleep(3000);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Error!, Try Again.. Amount to Transfer (press 0 to cancel): ");
                        }
                        break;

                    //ManageAccount
                    case 4:
                        Console.Clear();
                        Console.WriteLine("\t\t\t\t\t  M47 Bank\n\t\t\t\t\t▀▀▀▀▀▀▀▀▀▀▀▀");
                        Console.WriteLine($"user: {holderAccount.Username} | type: client \t\t Your Balance: {holderAccount.Balance:C}\n────────────");

                        Console.WriteLine(
                           $"1) Log History\n" +
                           $"2) Request Delete Account\n" +
                           $"3) Back");

                        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
                        {
                            Console.Clear();
                            Console.WriteLine("\t\t\t\t\t  M47 Bank\n\t\t\t\t\t▀▀▀▀▀▀▀▀▀▀▀▀");
                            Console.WriteLine($"user: {holderAccount.Username} | type: client \t\t Your Balance: {holderAccount.Balance:C}\n────────────");

                            Console.WriteLine(
                               $"1) Log History\n" +
                               $"2) Request Delete Account\n" +
                               $"3) Back");
                        }
                        switch (choice)
                        {
                            //Log History
                            case 1:
                                var logs = context.TransactionLogs.AsNoTracking()
                                    .Where(tl => tl.HolderAccountId == holderAccount.Id || tl.To == holderAccount.Username).ToList().OrderBy(tl => tl.CreatedAt);

                                Console.Clear();
                                Console.WriteLine("\t\t\t\t\t  M47 Bank\n\t\t\t\t\t▀▀▀▀▀▀▀▀▀▀▀▀");
                                Console.WriteLine($"user: {holderAccount.Username} | type: client \t\t Your Balance: {holderAccount.Balance:C}\n────────────\n");
                                Console.WriteLine("Log History: ");
                                Console.WriteLine(
                                    $"┌────┬───────────────┬───────────┬───────────────────────┬──────────────┬──────────────┐\n" +
                                    $"│ Id │    Amount     │   Type    │      Date & Time      │     From     │      To      │\n" +
                                    $"├────┼───────────────┼───────────┼───────────────────────┼──────────────┼──────────────┤");

                                foreach (var log in logs)
                                {
                                    if (log.TransactionType == Enums.TransactionType.Withdraw || (log.TransactionType == Enums.TransactionType.Transfer && log.HolderAccountId == holderAccount.Id))
                                    {
                                        log.Amount *= -1;
                                    }

                                    if ((log.TransactionType == Enums.TransactionType.Transfer && log.HolderAccountId == holderAccount.Id))
                                        log.From = null;
                                    if (log.TransactionType == Enums.TransactionType.Transfer && log.To == holderAccount.Username)
                                        log.To = null;

                                    Console.WriteLine(
                                        $"│ {log.Id,-3}│ {log.Amount,-14}│  {log.TransactionType,-9}│  {log.CreatedAt,-21}│ {log.From,-13}│ {log.To,-13}│");

                                    if (log == logs.Last())
                                        Console.WriteLine(
                                            $"└────┴───────────────┴───────────┴───────────────────────┴──────────────┴──────────────┘");
                                    else
                                        Console.WriteLine($"├────┼───────────────┼───────────┼───────────────────────┼──────────────┼──────────────┤");
                                }

                                Console.Write("\nPress Any Key To Back..");
                                Console.ReadKey();
                                break;

                            //Request Delete Account
                            case 2:
                                Console.WriteLine("do you want to request delete your account? (Y/N)");
                                var result1 = Console.ReadLine();

                                while (!(result1 == "Y" || result1 == "y" || result1 == "n" || result1 == "N"))
                                { Console.Write("Try Again: "); result1 = Console.ReadLine(); }

                                if (result1.Equals("Y") || result1.Equals("y"))
                                {
                                    holderAccount.DeleteState = DeleteState.Requested;
                                    context.SaveChanges();
                                    logout = true;
                                    activeAccount = null;
                                    Console.Clear();
                                    Console.WriteLine("\t Your Account Has been Requested To Delete, Wait for employee to accept. thank you :)");
                                    Console.Write("Press Any Key to Continue..");
                                    Console.ReadKey();
                                    Console.Clear();
                                }

                                else if (result1.Equals("N") || result1.Equals("N"))
                                    break;

                                break;

                            default:
                                break;
                        }
                        break;

                    //Logout
                    case 5:
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
