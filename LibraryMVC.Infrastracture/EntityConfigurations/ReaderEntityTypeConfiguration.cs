using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastracture.EntityConfigurations
{
    internal class ReaderEntityTypeConfiguration : IEntityTypeConfiguration<Reader>
    {
        public void Configure(EntityTypeBuilder<Reader> builder)
        {
            builder.HasKey(Reader => Reader.ID);
            builder.Property(Reader => Reader.Name).HasMaxLength(60).IsRequired();
            builder.Property(Reader => Reader.ContactInfo).HasMaxLength(120).IsRequired();
        }
    }
}
