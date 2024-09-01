using BankingSystem.Data;

namespace BankingSystem.Entites
{
    public static class AccountProcessesHelper
    {
        public static void RegisterNewAccount(string fullName, string faName, string nId, string phone, DateOnly birthdate, decimal preDeposit, string password)
        {
            var rnd = new Random();
            var generatedUsername = $"{fullName[..4]}{faName[..3]}{rnd.Next(10000, 100000)}";

            Console.WriteLine("-----------\n");
            Console.WriteLine($"Your Username Is: {generatedUsername}");
            Console.WriteLine("-----------\n");

            Console.Write("Press Any Key To Continue..");
            Console.ReadKey();

            Holder holder = new() { Name = fullName, FaName = faName, BirthDate = birthdate, NId = nId, Phone = phone };
            HolderAccount holderAccount = new() { Username = generatedUsername, HolderNId = nId, Password = password, CreatedAt = DateTime.Now, Balance = preDeposit };

            using (var context = new AppDbContext())
            {
                context.Holders.Add(holder);
                context.HolderAccounts.Add(holderAccount);
                context.SaveChanges();
            }
        }
        public static Account Login(string username, string password)
        {
            using (var context = new AppDbContext())
            {

                if (context.EmployeeAccounts.Where(e => e.Username.Equals(username) && e.Password.Equals(password)).Any())
                    return context.EmployeeAccounts.Single(e => e.Username == username);

                else if (context.HolderAccounts.Where(e => e.Username.Equals(username) && e.DeleteState != Enums.DeleteState.Deleted && e.Password.Equals(password)).Any())
                    return context.HolderAccounts.Single(e => e.Username == username);

                else
                {
                    Console.WriteLine("Invalid! Try again..");
                    return null;
                };
            }
        }
    }
}
