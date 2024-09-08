using System;
using System.Collections.Generic;
using Lesson6LoadingsTask.Models;
using Microsoft.EntityFrameworkCore;

namespace Lesson6LoadingsTask.Data;

public partial class LibraryContext : DbContext
{
    public LibraryContext()
    {
    }

    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Lib> Libs { get; set; }

    public virtual DbSet<SCard> SCards { get; set; }

    public virtual DbSet<TCard> TCards { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-7F74UDB;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FirstName).HasMaxLength(15);
            entity.Property(e => e.LastName).HasMaxLength(25);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("MoveTheInformation"));

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Comment).HasMaxLength(50);
            entity.Property(e => e.IdAuthor).HasColumnName("Id_Author");
            entity.Property(e => e.IdCategory).HasColumnName("Id_Category");
            entity.Property(e => e.IdPress).HasColumnName("Id_Press");
            entity.Property(e => e.IdThemes).HasColumnName("Id_Themes");
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.IdAuthor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Books_Author");
        });

        modelBuilder.Entity<Lib>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FirstName).HasMaxLength(15);
            entity.Property(e => e.LastName).HasMaxLength(25);
        });

        modelBuilder.Entity<SCard>(entity =>
        {
            entity.ToTable("S_Cards", tb =>
                {
                    tb.HasTrigger("CheckMonth");
                    tb.HasTrigger("CheckQuantity");
                    tb.HasTrigger("CheckQuantityy");
                    tb.HasTrigger("CheckTakenBookCount");
                    tb.HasTrigger("DecreaseQuantityForStudents");
                    tb.HasTrigger("IncreaseQunatity");
                    tb.HasTrigger("Trigger1");
                });

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DateIn).HasColumnType("datetime");
            entity.Property(e => e.DateOut).HasColumnType("datetime");
            entity.Property(e => e.IdBook).HasColumnName("Id_Book");
            entity.Property(e => e.IdLib).HasColumnName("Id_Lib");
            entity.Property(e => e.IdStudent).HasColumnName("Id_Student");

            entity.HasOne(d => d.Book).WithMany(p => p.SCards)
                .HasForeignKey(d => d.IdBook)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_S_Cards_Book");

            entity.HasOne(d => d.Lib).WithMany(p => p.SCards)
                .HasForeignKey(d => d.IdLib)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_S_Cards_Lib");
        });

        modelBuilder.Entity<TCard>(entity =>
        {
            entity.ToTable("T_Cards", tb =>
                {
                    tb.HasTrigger("CheckQuantityForTeachers");
                    tb.HasTrigger("DecreaseQuantityForTeachers");
                    tb.HasTrigger("IncreaseQunatityForTeachers");
                });

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DateIn).HasColumnType("datetime");
            entity.Property(e => e.DateOut).HasColumnType("datetime");
            entity.Property(e => e.IdBook).HasColumnName("Id_Book");
            entity.Property(e => e.IdLib).HasColumnName("Id_Lib");
            entity.Property(e => e.IdTeacher).HasColumnName("Id_Teacher");

            entity.HasOne(d => d.Book).WithMany(p => p.TCards)
                .HasForeignKey(d => d.IdBook)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_Cards_Book");

            entity.HasOne(d => d.Lib).WithMany(p => p.TCards)
                .HasForeignKey(d => d.IdLib)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_T_Cards_Lib");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
