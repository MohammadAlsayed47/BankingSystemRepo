using BankingSystem.Entites;
using BankingSystem.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Data.Config
{
    internal class TransactionLogConfiguration : IEntityTypeConfiguration<TransactionLog>
    {
        public void Configure(EntityTypeBuilder<TransactionLog> builder)
        {
            builder.ToTable("GLTransactions");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.TransactionType)
                .HasConversion(x => x.ToString(),x => (TransactionType) Enum.Parse(typeof(TransactionType), x))
                .HasColumnType("VARCHAR")
                .HasMaxLength(8);

            builder.Property(a => a.Amount)
                .HasColumnType("MONEY");


            builder.Property(a => a.From)
                .HasColumnType("VARCHAR")
                .HasMaxLength(50)
                .IsRequired(false);
            
            builder.Property(a => a.To)
                .HasColumnType("VARCHAR")
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(a => a.CreatedAt)
                .HasMaxLength(50);
        }
    }
}
