﻿using ApiBiblioteca.Domain.Models;
using Azure;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblioteca.Infra.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BookLending> BookLendings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring the composite primary key for the BookLending join class
            modelBuilder.Entity<BookLending>()
                .HasKey(bl => new { bl.LoanId, bl.BookId });

            // Configuring Many-to-Many Relationships
            modelBuilder.Entity<BookLending>()
                .HasOne(bl => bl.Loan)
                .WithMany(l => l.BookLendings)
                .HasForeignKey(bl => bl.LoanId);

            modelBuilder.Entity<BookLending>()
                .HasOne(bl => bl.Book)
                .WithMany(b => b.BookLendings)
                .HasForeignKey(bl => bl.BookId);
        }
    }
}
