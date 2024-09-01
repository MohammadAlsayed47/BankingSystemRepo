using BankingSystem.Entites;
using BankingSystem.ExtentionMethods;

namespace BankingSystem
{
    public partial class Program
    {
        public static Account activeAccount = null;
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\t\t\t\t\t  M47 Bank\n\t\t\t\t\t▀▀▀▀▀▀▀▀▀▀▀▀");

                Console.WriteLine($"\n1) Login");
                Console.WriteLine($"2) Register New Account");
                Console.WriteLine($"3) Exit");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
                {
                    Console.Clear();
                    Console.WriteLine("\t\t\t\t\t  M47 Bank\n\t\t\t\t\t▀▀▀▀▀▀▀▀▀▀▀▀");

                    Console.WriteLine($"\n1) Login");
                    Console.WriteLine($"2) Register New Account");
                    Console.WriteLine($"3) Exit");
                    Console.WriteLine("Choose Valid Option");
                }

                switch (choice)
                {
                    //Login
                    case 1: 
                        while (activeAccount is null)
                        {
                            Console.Write("Username: ");
                            var username = Console.ReadLine();

                            Console.Write("Password: ");
                            var password = Console.ReadLine();
                            activeAccount = AccountProcessesHelper.Login(username, password);
                        }

                        bool loggedOut = false;
                        while (!loggedOut)
                        {
                            FormContext formContext = null;
                            if (activeAccount is HolderAccount)
                            {
                                formContext = new FormContext(new HolderForm());
                                formContext.LoadForm(activeAccount, ref loggedOut);
                            }
                            if (activeAccount is EmployeeAccount)
                            {
                                formContext = new FormContext(new EmployeeForm());
                                formContext.LoadForm(activeAccount, ref loggedOut);
                            }
                        }
                        break;

                    //Register Account
                    case 2:
                        string fullName = "";
                        string faName = "";
                        string nId = "";
                        string phone = "";
                        DateOnly birthdate;
                        decimal preDeposit;
                        string newPassword = null;

                        Console.Write("Enter Your Fullname: ");
                        fullName = Console.ReadLine();
                        while (string.IsNullOrEmpty(fullName) || fullName.ContainsNonChar())
                        {
                            Console.Write("error, try again.. Enter Your Fullname: ");
                            fullName = Console.ReadLine();
                        }

                        Console.Write("Enter Your Father Name: ");
                        faName = Console.ReadLine();
                        while (string.IsNullOrEmpty(faName) || fullName.ContainsNonChar())
                        {
                            Console.Write("error, try again.. Enter Your Father Name: ");
                            faName = Console.ReadLine();
                        }

                        Console.Write("Enter Your National Id (11 digits): ");
                        nId = Console.ReadLine();
                        while (string.IsNullOrEmpty(nId) || (nId.Length != 11))
                        {
                            Console.WriteLine("Invalid! Try Again..");
                            Console.Write("Enter Your National Id: ");
                            nId = Console.ReadLine();
                        }

                        Console.Write("Enter Your Phone Number: (10 digits, 09xxxxxxxx): ");
                        phone = Console.ReadLine();
                        while (string.IsNullOrEmpty(phone) || (phone.Length != 10))
                        {
                            Console.WriteLine("Invalid! Try Again..");
                            Console.Write("Enter Your Phone Number: ");
                            phone = Console.ReadLine();
                        }

                        Console.Write("Enter Your Birthdate: ");
                        while (!DateOnly.TryParse(Console.ReadLine(), out birthdate) || birthdate.Year > 2006)
                        {
                            Console.WriteLine("Invalid! Try Again..");
                            Console.Write("Enter Your Birthdate: ");
                        }

                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Account Created Successfully\n");
                        Console.ForegroundColor = ConsoleColor.White;


                        Console.Write("How Much Will You Pre-Deposit?, (min 100$): ");
                        while (!decimal.TryParse(Console.ReadLine(), out preDeposit) || preDeposit < 100m)
                        {
                            Console.WriteLine("Invalid! Try Again..");
                            Console.Write("Enter Valid Amount or Deposit 100$ at least: ");
                        }

                        Console.WriteLine("-----------");
                        Console.Write("Enter a Password (8 characters at least): ");
                        newPassword = Console.ReadLine();
                        while (string.IsNullOrEmpty(newPassword) || newPassword.Length < 8)
                        {
                            Console.WriteLine("Invalid Password!! or it's less than 8 characters, Try Again..");
                            Console.Write("Enter a Password: ");
                            newPassword = Console.ReadLine();
                        }

                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Done!!");
                        Console.ForegroundColor = ConsoleColor.White;
                        Task.Delay(2000);

                        AccountProcessesHelper.RegisterNewAccount(fullName, newPassword, nId, phone, birthdate, preDeposit, newPassword);
                        Console.Clear();
                        break;
                    
                    //Exit
                    case 3:
                        Environment.Exit(0);
                        break;

                    default:
                        break;
                }
            }
        }

        public class FormContext(IForm form)
        {
            private readonly IForm _form = form;

            public void LoadForm(Account account, ref bool logout)
            {
                _form.LoadForm(account, ref logout);
            }
        }

        /*
        private static void EmployeeForm(EmployeeAccount employeeAccount, ref bool logout)
        {
            using var context = new AppDbContext();
            context.Attach(employeeAccount);

            Console.Clear();
            Console.WriteLine("\t\t\t\t\t  M47 Bank\n\t\t\t\t\t▀▀▀▀▀▀▀▀▀▀▀▀");
            Console.WriteLine($"user: {employeeAccount.Username} | type: employee");

            Console.WriteLine(
                $"\n1) History Logs\n" +
                $"2) Accounts Requested To Delete\n" +
                $"3) Logout");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 5)
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\t\t  M47 Bank\n\t\t\t\t\t▀▀▀▀▀▀▀▀▀▀▀▀");
                Console.WriteLine($"user: {employeeAccount.Username} | type: employee");

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

        private static void HolderForm(HolderAccount holderAccount, ref bool logout)
        {
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
                    bool tryAgain = true;
                    Console.WriteLine("Amount to Deposit (0 to cancel):");
                    while (tryAgain)
                    {
                        decimal amountToDeposit;
                        while (!decimal.TryParse(Console.ReadLine(), out amountToDeposit))
                        {
                            Console.WriteLine("Invalid Amount!, Try again..");
                            Console.WriteLine("Amount to Deposit (press 0 to cancel): ");
                        }
                        if (amountToDeposit == 0)
                            break;

                        holderAccount.Deposit(amountToDeposit, false);

                        if (context.SaveChanges() > 0)
                        {
                            Console.WriteLine($"\t ({amountToDeposit:C}) deposited successfully!!");
                            Thread.Sleep(3000);
                            tryAgain = false;
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
                    tryAgain = true;
                    Console.WriteLine("Amount to Withdraw (press 0 to cancel):");

                    while (tryAgain)
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
                            tryAgain = false;
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

                    while (true)
                    {
                        if (context.HolderAccounts.Any(ha => ha.Username.Equals(username) && !(username.Equals(holderAccount.Username)) && ha.DeleteState == DeleteState.NotDeleted))
                            break;

                        Console.WriteLine("User not Found!, Try Again (press 0 to cancel): ");
                        username = Console.ReadLine();
                    }

                    if (username == "0")
                        break;

                    var userToTransfer = context.HolderAccounts.First(ha => ha.Username.Equals(username));

                    tryAgain = true;
                    Console.WriteLine("Amount to Transfer (press 0 to cancel):");
                    while (tryAgain)
                    {

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
                            tryAgain = false;
                        }
                        else
                        {
                            Console.WriteLine("Error!, Try Again.. Amount to Transfer (press 0 to cancel): ");
                        }
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
                                $"│ {log.Id,-3}│ {log.Amount, -14}│  {log.TransactionType,-9}│  {log.CreatedAt,-21}│ {log.From,-13}│ {log.To,-13}│");

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
        */
    }
}
