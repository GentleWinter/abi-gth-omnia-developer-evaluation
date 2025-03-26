using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sale");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                  .HasColumnType("uuid")
                  .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(s => s.SaleNumber).IsRequired();
            builder.Property(s => s.CreatedAt).IsRequired();
            builder.Property(s => s.CustomerId).IsRequired().HasColumnType("uuid");
            builder.Property(s => s.BranchId).IsRequired().HasColumnType("uuid");
            builder.Property(s => s.IsCanceled).IsRequired();

            builder.HasMany(s => s.Items)
                  .WithOne()
                  .HasForeignKey("SaleId");
        }
    }
}
