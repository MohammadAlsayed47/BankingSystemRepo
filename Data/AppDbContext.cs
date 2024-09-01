using BankingSystem.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Data
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Holder> Holders { get; set; }
        public virtual DbSet<EmployeeAccount> EmployeeAccounts { get; set; }
        public virtual DbSet<HolderAccount> HolderAccounts { get; set; }
        public virtual DbSet<TransactionLog> TransactionLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var constr = config.GetSection("constr").Value;
            optionsBuilder.UseSqlServer(constr);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
