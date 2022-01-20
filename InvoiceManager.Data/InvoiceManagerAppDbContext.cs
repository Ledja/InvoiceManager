using InvoiceManager.Core.Entities;
using InvoiceManager.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using System;

namespace InvoiceManager.Data
{
    public class InvoiceManagerAppDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Invoice> Invoices { get; set; }


        public string DbPath { get; }

        public InvoiceManagerAppDbContext(DbContextOptions<InvoiceManagerAppDbContext> options)
            : base(options)
        {
            var folder = Environment.CurrentDirectory;
            DbPath = System.IO.Path.Join(folder, "invoice.db");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new ItemConfiguration());

            builder
                .ApplyConfiguration(new InvoiceConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}