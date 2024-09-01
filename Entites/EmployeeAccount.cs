namespace BankingSystem.Entites
{
    public class EmployeeAccount : Account
    {
        public bool IsOnwer { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
