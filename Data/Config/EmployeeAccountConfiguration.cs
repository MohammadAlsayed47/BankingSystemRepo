using BankingSystem.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Data.Config
{
    internal class EmployeeAccountConfiguration : IEntityTypeConfiguration<EmployeeAccount>
    {
        public void Configure(EntityTypeBuilder<EmployeeAccount> builder)
        {
            builder.ToTable("EmployeeAccounts");

            builder.Property(ea => ea.IsOnwer)
                .HasMaxLength(4)
                .IsRequired();

            builder.HasOne(ea => ea.Employee)
                .WithOne(e => e.Account)
                .HasForeignKey<EmployeeAccount>(ea => ea.EmployeeId)
                .IsRequired();

            builder.HasData(LoadEmpAccounts());
        }

        private List<EmployeeAccount> LoadEmpAccounts()
        {
            return new()
            {
                new() {Id = 1, EmployeeId = 1, CreatedAt = new DateTime(2023,1,1), IsOnwer = true, Username = "messi10", Password = "messi1010" },
                new() {Id = 2, EmployeeId = 2, CreatedAt = new DateTime(2023,1,1), IsOnwer = false, Username = "ronaldinho10", Password = "ronaldinho1010" },
                new() {Id = 3, EmployeeId = 3, CreatedAt = new DateTime(2023,1,1), IsOnwer = false, Username = "neymar10", Password = "neymar1010" }
            };
        }
    }
}
