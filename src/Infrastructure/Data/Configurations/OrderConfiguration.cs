using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data.Configurations;

public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        // Key with value converter for OrderId
        var orderIdConverter = new ValueConverter<OrderId, Guid>(
            id => id.Value,
            value => OrderId.From(value)
        );

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id)
            .HasConversion(orderIdConverter)
            .ValueGeneratedNever();

        builder.Property(o => o.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(o => o.CancellationReason)
            .HasMaxLength(1024);

        // Computed/derived, not stored
        builder.Ignore(o => o.TotalPrice);

        // Use backing field for items
        builder.Navigation(nameof(Order.Items))
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.OwnsMany(o => o.Items, itemsBuilder =>
        {
            itemsBuilder.ToTable("OrderItems");

            // Shadow key for the owned entity
            itemsBuilder.Property<int>("Id");
            itemsBuilder.HasKey("Id");

            itemsBuilder.WithOwner().HasForeignKey("OrderId");

            itemsBuilder.Property(i => i.ProductId)
                .IsRequired();

            itemsBuilder.Property(i => i.Quantity)
                .IsRequired();

            itemsBuilder.Property(i => i.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            // Derived value
            itemsBuilder.Ignore(i => i.LineTotal);
        });
    }
}
