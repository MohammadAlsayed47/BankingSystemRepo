using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Entites
{
    public class Holder
    {
        public string NId { get; set; }
        public string Name { get; set; }
        public string FaName { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Phone { get; set; }
        public HolderAccount? Account { get; set; }
    }
}
