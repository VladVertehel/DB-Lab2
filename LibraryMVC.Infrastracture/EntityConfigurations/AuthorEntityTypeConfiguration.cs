using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastracture.EntityConfigurations
{
    internal class AuthorEntityTypeConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(Author => Author.ID);
            builder.Property(Author => Author.Name).HasMaxLength(60);
            builder.Property(Author => Author.Biography).HasMaxLength(360);
        }
    }
}
