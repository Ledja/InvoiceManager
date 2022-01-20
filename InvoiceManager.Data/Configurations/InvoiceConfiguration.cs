using InvoiceManager.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceManager.Data.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder
                .HasKey(i => i.Id);

            builder
                .Property(i => i.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(i => i.Description)
                .IsRequired()
                .HasMaxLength(250);
            builder
                .Property(i => i.IsPaid)
                .IsRequired();

            builder
                .HasMany(i => i.Items)
                .WithOne();

            builder
                .ToTable("Invoices");
        }
    }
}
