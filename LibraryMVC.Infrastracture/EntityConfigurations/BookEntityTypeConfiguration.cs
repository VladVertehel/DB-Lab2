using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastracture.EntityConfigurations
{
    internal class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(Book => Book.ID);
            builder.Property(Book => Book.Title).HasMaxLength(60);
            builder.Property(Book => Book.Author).HasMaxLength(60);
            builder.Property(Book => Book.Publisher).HasMaxLength(90);
            builder.Property(Book => Book.Genre).HasMaxLength(60);
        }
    }
}
