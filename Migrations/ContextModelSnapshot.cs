﻿// <auto-generated />
using System;
using ApiBiblioteca.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiBiblioteca.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApiBiblioteca.Domain.Models.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Complement")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("ApiBiblioteca.Domain.Models.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("ApiBiblioteca.Domain.Models.BookLending", b =>
                {
                    b.Property<Guid>("LoanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CopyId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoanId", "CopyId");

                    b.HasIndex("CopyId");

                    b.ToTable("BookLendings");
                });

            modelBuilder.Entity("ApiBiblioteca.Domain.Models.Copy", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Available")
                        .HasColumnType("bit");

                    b.Property<Guid>("BookId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CopyCode")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("Copies");
                });

            modelBuilder.Entity("ApiBiblioteca.Domain.Models.Loan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Fines")
                        .HasColumnType("float");

                    b.Property<DateTime>("LoanDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("ApiBiblioteca.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ApiBiblioteca.Domain.Models.Address", b =>
                {
                    b.HasOne("ApiBiblioteca.Domain.Models.User", "User")
                        .WithOne("Address")
                        .HasForeignKey("ApiBiblioteca.Domain.Models.Address", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ApiBiblioteca.Domain.Models.BookLending", b =>
                {
                    b.HasOne("ApiBiblioteca.Domain.Models.Copy", "Copy")
                        .WithMany("BookLendings")
                        .HasForeignKey("CopyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiBiblioteca.Domain.Models.Loan", "Loan")
                        .WithMany("BookLendings")
                        .HasForeignKey("LoanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Copy");

                    b.Navigation("Loan");
                });

            modelBuilder.Entity("ApiBiblioteca.Domain.Models.Copy", b =>
                {
                    b.HasOne("ApiBiblioteca.Domain.Models.Book", "Book")
                        .WithMany("Copies")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("ApiBiblioteca.Domain.Models.Loan", b =>
                {
                    b.HasOne("ApiBiblioteca.Domain.Models.User", "User")
                        .WithMany("Loans")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ApiBiblioteca.Domain.Models.Book", b =>
                {
                    b.Navigation("Copies");
                });

            modelBuilder.Entity("ApiBiblioteca.Domain.Models.Copy", b =>
                {
                    b.Navigation("BookLendings");
                });

            modelBuilder.Entity("ApiBiblioteca.Domain.Models.Loan", b =>
                {
                    b.Navigation("BookLendings");
                });

            modelBuilder.Entity("ApiBiblioteca.Domain.Models.User", b =>
                {
                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("Loans");
                });
#pragma warning restore 612, 618
        }
    }
}
