﻿// <auto-generated />
using System;
using BankingSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BankingSystem.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240816135907_Add-DeleteState-Enum-For-HolderAccount")]
    partial class AddDeleteStateEnumForHolderAccount
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.HasSequence("AccountSequence");

            modelBuilder.Entity("BankingSystem.Entites.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR [AccountSequence]");

                    SqlServerPropertyBuilderExtensions.UseSequence(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasMaxLength(50)
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR");

                    b.HasKey("Id");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("BankingSystem.Entites.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR");

                    b.HasKey("Id");

                    b.ToTable("Employees", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "messi@gmail.com",
                            Name = "Messi"
                        },
                        new
                        {
                            Id = 2,
                            Email = "ronaldinho@gmail.com",
                            Name = "Ronaldinho"
                        },
                        new
                        {
                            Id = 3,
                            Email = "neymar@gmail.com",
                            Name = "Neymar"
                        });
                });

            modelBuilder.Entity("BankingSystem.Entites.Holder", b =>
                {
                    b.Property<string>("NId")
                        .HasMaxLength(11)
                        .HasColumnType("VARCHAR");

                    b.Property<DateOnly>("BirthDate")
                        .HasMaxLength(10)
                        .HasColumnType("DATE");

                    b.Property<string>("FaName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR");

                    b.HasKey("NId");

                    b.ToTable("Holders", (string)null);
                });

            modelBuilder.Entity("BankingSystem.Entites.TransactionLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("MONEY");

                    b.Property<DateTime>("CreatedAt")
                        .HasMaxLength(50)
                        .HasColumnType("datetime2");

                    b.Property<string>("From")
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR");

                    b.Property<int>("HolderAccountId")
                        .HasColumnType("int");

                    b.Property<string>("To")
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("TransactionType")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("VARCHAR");

                    b.HasKey("Id");

                    b.HasIndex("HolderAccountId");

                    b.ToTable("GLTransactions", (string)null);
                });

            modelBuilder.Entity("BankingSystem.Entites.EmployeeAccount", b =>
                {
                    b.HasBaseType("BankingSystem.Entites.Account");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<bool>("IsOnwer")
                        .HasMaxLength(4)
                        .HasColumnType("bit");

                    b.HasIndex("EmployeeId")
                        .IsUnique()
                        .HasFilter("[EmployeeId] IS NOT NULL");

                    b.ToTable("EmployeeAccounts", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Password = "messi1010",
                            Username = "messi10",
                            EmployeeId = 1,
                            IsOnwer = true
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Password = "ronaldinho1010",
                            Username = "ronaldinho10",
                            EmployeeId = 2,
                            IsOnwer = false
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Password = "neymar1010",
                            Username = "neymar10",
                            EmployeeId = 3,
                            IsOnwer = false
                        });
                });

            modelBuilder.Entity("BankingSystem.Entites.HolderAccount", b =>
                {
                    b.HasBaseType("BankingSystem.Entites.Account");

                    b.Property<decimal>("Balance")
                        .HasColumnType("MONEY");

                    b.Property<string>("DeleteState")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10)
                        .HasColumnType("VARCHAR")
                        .HasDefaultValue("NotDeleted");

                    b.Property<string>("HolderNId")
                        .IsRequired()
                        .HasColumnType("VARCHAR(11)");

                    b.HasIndex("HolderNId")
                        .IsUnique()
                        .HasFilter("[HolderNId] IS NOT NULL");

                    b.ToTable("HolderAccounts", (string)null);
                });

            modelBuilder.Entity("BankingSystem.Entites.TransactionLog", b =>
                {
                    b.HasOne("BankingSystem.Entites.HolderAccount", "HolderAccount")
                        .WithMany("TransactionLogs")
                        .HasForeignKey("HolderAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HolderAccount");
                });

            modelBuilder.Entity("BankingSystem.Entites.EmployeeAccount", b =>
                {
                    b.HasOne("BankingSystem.Entites.Employee", "Employee")
                        .WithOne("Account")
                        .HasForeignKey("BankingSystem.Entites.EmployeeAccount", "EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("BankingSystem.Entites.HolderAccount", b =>
                {
                    b.HasOne("BankingSystem.Entites.Holder", "Holder")
                        .WithOne("Account")
                        .HasForeignKey("BankingSystem.Entites.HolderAccount", "HolderNId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Holder");
                });

            modelBuilder.Entity("BankingSystem.Entites.Employee", b =>
                {
                    b.Navigation("Account");
                });

            modelBuilder.Entity("BankingSystem.Entites.Holder", b =>
                {
                    b.Navigation("Account");
                });

            modelBuilder.Entity("BankingSystem.Entites.HolderAccount", b =>
                {
                    b.Navigation("TransactionLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
