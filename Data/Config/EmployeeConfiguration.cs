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
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(h => h.Id);

            builder.Property(h => h.Name)
                .HasColumnType("VARCHAR")
                .HasMaxLength(50);

            builder.Property(h => h.Email);

            builder.HasData(LoadEmps());
        }

        private List<Employee> LoadEmps()
        {
            return new()
            {
                new Employee {Id = 1, Name = "Messi", Email = "messi@gmail.com" },
                new Employee {Id = 2, Name = "Ronaldinho", Email = "ronaldinho@gmail.com" },
                new Employee {Id = 3, Name = "Neymar", Email = "neymar@gmail.com" }
            };
        }
    }
}
