using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastracture.EntityConfigurations
{
    internal class PublisherEntityTypeConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.HasKey(Publisher => Publisher.ID);
            builder.Property(Publisher => Publisher.Name).HasMaxLength(90).IsRequired();
            builder.Property(Publisher => Publisher.ContactInfo).HasMaxLength(120).IsRequired();
        }
    }
}
