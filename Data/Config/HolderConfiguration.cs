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
    internal class HolderConfiguration : IEntityTypeConfiguration<Holder>
    {
        public void Configure(EntityTypeBuilder<Holder> builder)
        {
            builder.ToTable("Holders");

            builder.HasKey(h => h.NId);
            builder.Property(h => h.NId)
                .HasColumnType("VARCHAR")
                .HasMaxLength(11)
                .ValueGeneratedNever();

            builder.Property(h => h.Name)
                .HasColumnType("VARCHAR")
                .HasMaxLength(50);

            builder.Property(h => h.FaName)
                .HasColumnType("VARCHAR")
                .HasMaxLength(50);

            builder.Property(h => h.BirthDate)
                .HasColumnType("DATE")
                .HasMaxLength(10);
            
            builder.Property(h => h.Phone)
                .HasColumnType("VARCHAR")
                .HasMaxLength(50);
        }
    }
}
