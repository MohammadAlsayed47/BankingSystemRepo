using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Entites
{
    public abstract class Account
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
