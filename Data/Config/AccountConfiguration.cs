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
    internal class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        { 
            builder.UseTpcMappingStrategy();

            builder.HasKey(a => a.Id);

            builder.Property(a => a.CreatedAt)
                .HasMaxLength(50);

            builder.Property(a => a.Password)
                .HasMaxLength(50);

            builder.Property(a => a.Username)
                .HasColumnType("VARCHAR")
                .HasMaxLength(50);
        }
    }
}
