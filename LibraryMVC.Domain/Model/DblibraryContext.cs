//using System;
//using System.Collections.Generic;
//using Microsoft.EntityFrameworkCore;

//namespace Domain.Model;

//public partial class DblibraryContext : DbContext
//{
//    public DblibraryContext()
//    {
//    }

//    public DblibraryContext(DbContextOptions<DblibraryContext> options)
//        : base(options)
//    {
//    }

//    public virtual DbSet<Author> Authors { get; set; }

//    public virtual DbSet<AuthorsBook> AuthorsBooks { get; set; }

//    public virtual DbSet<Book> Books { get; set; }

//    public virtual DbSet<BorrowedBook> BorrowedBooks { get; set; }

//    public virtual DbSet<Genre> Genres { get; set; }

//    public virtual DbSet<Publisher> Publishers { get; set; }

//    public virtual DbSet<User> Readers { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        => optionsBuilder.UseSqlServer("Server=DESKTOP-9V2UM5C\\SQLEXPRESS; Database=DBLibrary; Trusted_Connection=True; TrustServerCertificate=True; ");

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Author>(entity =>
//        {
//            entity.ToTable(tb => tb.HasComment("Table of authors and thier boigraphies"));

//            entity.Property(e => e.AuthorId).HasColumnName("author_ID");
//            entity.Property(e => e.Biography)
//                .HasMaxLength(360)
//                .HasColumnName("biography");
//            entity.Property(e => e.Name)
//                .HasMaxLength(60)
//                .HasColumnName("name");
//        });

//        modelBuilder.Entity<AuthorsBook>(entity =>
//        {
//            entity.HasNoKey();

//            entity.Property(e => e.AuthorId).HasColumnName("author_ID");
//            entity.Property(e => e.BookId).HasColumnName("book_ID");

//            entity.HasOne(d => d.Author).WithMany()
//                .HasForeignKey(d => d.AuthorId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK_AuthorsBooks_Authors");

//            entity.HasOne(d => d.Book).WithMany()
//                .HasForeignKey(d => d.BookId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK_AuthorsBooks_Books");
//        });

//        modelBuilder.Entity<Book>(entity =>
//        {
//            entity.ToTable(tb => tb.HasComment("Table of all books"));

//            entity.Property(e => e.BookId).HasColumnName("book_ID");
//            entity.Property(e => e.Author)
//                .HasMaxLength(60)
//                .HasColumnName("author");
//            entity.Property(e => e.AuthorId).HasColumnName("author_ID");
//            entity.Property(e => e.Genre)
//                .HasMaxLength(60)
//                .HasColumnName("genre");
//            entity.Property(e => e.GenreId).HasColumnName("genre_ID");
//            entity.Property(e => e.Publisher)
//                .HasMaxLength(90)
//                .HasColumnName("publisher");
//            entity.Property(e => e.PublisherId).HasColumnName("publisher_ID");
//            entity.Property(e => e.PublishingYear).HasColumnName("publishing_year");
//            entity.Property(e => e.Title)
//                .HasMaxLength(60)
//                .HasColumnName("title");

//            entity.HasOne(d => d.AuthorNavigation).WithMany(p => p.Books)
//                .HasForeignKey(d => d.AuthorId)
//                .HasConstraintName("FK_Books_Authors");

//            entity.HasOne(d => d.GenreNavigation).WithMany(p => p.Books)
//                .HasForeignKey(d => d.GenreId)
//                .HasConstraintName("FK_Books_Genres");

//            entity.HasOne(d => d.PublisherNavigation).WithMany(p => p.Books)
//                .HasForeignKey(d => d.PublisherId)
//                .HasConstraintName("FK_Books_Publishers");
//        });

//        modelBuilder.Entity<BorrowedBook>(entity =>
//        {
//            entity.HasKey(e => e.BorrowId);

//            entity.ToTable(tb => tb.HasComment("Books that are currently borrowed"));

//            entity.Property(e => e.BorrowId).HasColumnName("borrow_ID");
//            entity.Property(e => e.BookId).HasColumnName("book_ID");
//            entity.Property(e => e.BorrowStart).HasColumnName("borrow_start");
//            entity.Property(e => e.BorrowTime).HasColumnName("borrow_time");
//            entity.Property(e => e.ReaderId).HasColumnName("reader_ID");

//            entity.HasOne(d => d.Book).WithMany(p => p.BorrowedBooks)
//                .HasForeignKey(d => d.BookId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK_BorrowedBooks_Books");

//            entity.HasOne(d => d.User).WithMany(p => p.BorrowedBooks)
//                .HasForeignKey(d => d.ReaderId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK_BorrowedBooks_Readers");
//        });

//        modelBuilder.Entity<Genre>(entity =>
//        {
//            entity.ToTable(tb => tb.HasComment("Table of existing genres"));

//            entity.Property(e => e.GenreId).HasColumnName("genre_ID");
//            entity.Property(e => e.GenreDescription)
//                .HasMaxLength(360)
//                .HasColumnName("genre_description");
//            entity.Property(e => e.GenreName)
//                .HasMaxLength(60)
//                .HasColumnName("genre_name");
//        });

//        modelBuilder.Entity<Publisher>(entity =>
//        {
//            entity.ToTable(tb => tb.HasComment("Table of publishers"));

//            entity.Property(e => e.PublisherId).HasColumnName("publisher_ID");
//            entity.Property(e => e.ContactInfo)
//                .HasMaxLength(120)
//                .HasColumnName("contactInfo");
//            entity.Property(e => e.Name)
//                .HasMaxLength(90)
//                .HasColumnName("name");
//        });

//        modelBuilder.Entity<User>(entity =>
//        {
//            entity.ToTable(tb => tb.HasComment("Registered readers"));

//            entity.Property(e => e.ReaderId).HasColumnName("reader_ID");
//            entity.Property(e => e.ContactInfo)
//                .HasMaxLength(120)
//                .HasColumnName("contactInfo");
//            entity.Property(e => e.Name)
//                .HasMaxLength(60)
//                .HasColumnName("name");
//        });

//        OnModelCreatingPartial(modelBuilder);
//    }

//    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//}
