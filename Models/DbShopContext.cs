using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace DbClassLib.Models
{
    public partial class MyShopContext : DbContext
    {
          IConfiguration _config;

        public MyShopContext(DbContextOptions<MyShopContext> options,IConfiguration config)
            : base(options)
        {
            _config=config;
            Database.SetCommandTimeout(300);
           Database.EnsureDeleted();  //8.11.20
            Database.EnsureCreated();
        }

        public virtual DbSet<Image> Image { get; set; }
        public new virtual DbSet<Model> Model { get; set; }
        public virtual DbSet<Nomenclature> Nomenclature { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Состав> Состав { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL(this._config["ConnectStringLocal"]);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("name_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Path)
                    .HasName("path_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.ProductId)
                    .HasName("fk_Image_Product1_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(10) unsigned zerofill");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(45);

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasColumnName("path")
                    .HasMaxLength(100);

                entity.Property(e => e.ProductId).HasColumnName("Product_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Image)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Image_Product1");
            });

            modelBuilder.Entity<Model>(entity =>
            {
                entity.HasIndex(e => e.KatalogId)
                    .HasName("fk_Model_Katalog_idx");

                entity.HasIndex(e => e.Name)
                    .HasName("name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(200);

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .HasMaxLength(200);

                entity.Property(e => e.KatalogId).HasColumnName("Katalog_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(45);

                entity.Property(e => e.Price).HasColumnName("price");
            });

            modelBuilder.Entity<Nomenclature>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.ModelId)
                    .HasName("fk_Product_Model1_idx");

                entity.HasIndex(e => e.Name)
                    .HasName("name_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.СоставId)
                    .HasName("fk_Product_Состав1_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ModelId).HasColumnName("Model_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(45);

                entity.Property(e => e.СоставId).HasColumnName("Состав_id");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Product_Model1");

                entity.HasOne(d => d.Состав)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.СоставId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Product_Состав1");
            });

            modelBuilder.Entity<Состав>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("name_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.NomenclatureId)
                    .HasName("fk_Состав_Nomenclature1_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(45);

                entity.Property(e => e.NomenclatureId).HasColumnName("Nomenclature_id");

                entity.HasOne(d => d.Nomenclature)
                    .WithMany(p => p.Состав)
                    .HasForeignKey(d => d.NomenclatureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Состав_Nomenclature1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
