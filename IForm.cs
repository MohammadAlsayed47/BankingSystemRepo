using BankingSystem.Entites;

namespace BankingSystem
{
    public partial class Program
    {
        public interface IForm
        {
            void LoadForm(Account account, ref bool logout);
        }

    }
}
