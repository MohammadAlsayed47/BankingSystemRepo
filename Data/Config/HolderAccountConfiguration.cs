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
    internal class HolderAccountConfiguration : IEntityTypeConfiguration<HolderAccount>
    {
        public void Configure(EntityTypeBuilder<HolderAccount> builder)
        {
            builder.ToTable("HolderAccounts");
            builder.Property(ha => ha.Id).ValueGeneratedOnAdd();

            builder.Property(ha => ha.Balance)
                .HasColumnType("MONEY");

            builder.Property(ha => ha.DeleteState)
                .HasConversion(ds => ds.ToString(),
                ds => (DeleteState)Enum.Parse(typeof(DeleteState), ds))
                .HasColumnType("VARCHAR")
                .HasDefaultValue(DeleteState.NotDeleted)
                .HasMaxLength(10);

            builder.HasOne(ha => ha.Holder)
                .WithOne(ha => ha.Account)
                .HasForeignKey<HolderAccount>(ha => ha.HolderNId)
                .IsRequired();

            builder.HasMany(ha => ha.TransactionLogs)
                .WithOne(tl => tl.HolderAccount)
               .HasForeignKey(tl => tl.HolderAccountId)
               .IsRequired();
        }
    }
}
