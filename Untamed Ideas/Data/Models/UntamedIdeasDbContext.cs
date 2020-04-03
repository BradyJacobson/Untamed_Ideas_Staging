using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data.Models
{
    public partial class UntamedIdeasDbContext : DbContext
    {
        public UntamedIdeasDbContext()
        {
        }

        public UntamedIdeasDbContext(DbContextOptions<UntamedIdeasDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Descriptions> Descriptions { get; set; }
        public virtual DbSet<Ideas> Ideas { get; set; }
        public virtual DbSet<Images> Images { get; set; }
        public virtual DbSet<Supplies> Supplies { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server = .\\SQLExpress; Database = UntamedIdeasDb; Trusted_Connection = true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Descriptions>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Content)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdeaNavigation)
                    .WithMany(p => p.Descriptions)
                    .HasForeignKey(d => d.Idea)
                    .HasConstraintName("FK__Descriptio__Idea__4F7CD00D");
            });

            modelBuilder.Entity<Ideas>(entity =>
            {
                entity.HasIndex(e => e.Ideaname)
                    .HasName("UQ__Ideas__85B8B7B206BF0364")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Ideaname)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Ideas)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("FK__Ideas__Username__4CA06362");
            });

            modelBuilder.Entity<Images>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Representation).HasColumnName("representation");

                entity.HasOne(d => d.IdeaNavigation)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.Idea)
                    .HasConstraintName("FK__Images__Idea__5535A963");
            });

            modelBuilder.Entity<Supplies>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Supplies1)
                    .HasColumnName("Supplies")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdeaNavigation)
                    .WithMany(p => p.Supplies)
                    .HasForeignKey(d => d.Idea)
                    .HasConstraintName("FK__Supplies__Idea__52593CB8");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__Users__536C85E517B4B0D1");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
