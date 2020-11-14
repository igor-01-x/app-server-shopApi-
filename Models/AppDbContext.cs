using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;


namespace ShopDbLib.Models
{
    public partial class AppDbContext : DbContext
    {     IConfiguration _config;

        

        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Katalog> Katalog { get; set; }
        public new virtual DbSet<Model> Model { get; set; }
        public virtual DbSet<Nomenclature> Nomenclature { get; set; }
        public virtual DbSet<ProdNomenclatures> ProdNomenclatures { get; set; }
        public virtual DbSet<Product> Product { get; set; }


        //----------------- 
        public AppDbContext(DbContextOptions<AppDbContext> options,IConfiguration config)
            : base(options)
        { 
            _config=config;
             Database.SetCommandTimeout(300);
          Database.EnsureDeleted();  //24.10.20
            Database.EnsureCreated();
        }


        //------------------------------------------------

       

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
                    .HasColumnType("int(10) unsigned zerofill")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasColumnName("path")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ProductId).HasColumnName("Product_id");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Image)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Image_Product1");
            });

            modelBuilder.Entity<Katalog>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
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
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.KatalogId).HasColumnName("Katalog_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.HasOne(d => d.Katalog)
                    .WithMany(p => p.Model)
                    .HasForeignKey(d => d.KatalogId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Model_Katalog");
            });

            modelBuilder.Entity<Nomenclature>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Image)
                    .HasColumnName("image")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Price).HasColumnName("price");
            });

            modelBuilder.Entity<ProdNomenclatures>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.NomenclatureId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.HasIndex(e => e.NomenclatureId)
                    .HasName("fk_Состав_Nomenclature1_idx");

                entity.HasIndex(e => e.ProductId)
                    .HasName("fk_Состав_Product1_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.NomenclatureId).HasColumnName("Nomenclature_id");

                entity.Property(e => e.ProductId).HasColumnName("Product_id");

                entity.HasOne(d => d.Nomenclature)
                    .WithMany(p => p.ProdNomenclatures)
                    .HasForeignKey(d => d.NomenclatureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Состав_Nomenclature1");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProdNomenclatures)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Состав_Product1");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.ModelId)
                    .HasName("fk_Product_Model1_idx");

                entity.HasIndex(e => e.Name)
                    .HasName("name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ModelId).HasColumnName("Model_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Product_Model1");
            });

            OnModelCreatingPartial(modelBuilder);
        }


        //Здесь инициалицируем  БД (субд)  начальными данными
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            OnModelKatalogCreating(modelBuilder);
        }


               private void OnModelKatalogCreating(ModelBuilder modelBuilder){
         
          
  
        var  kagalog=new Katalog[]{
        new Katalog {Id=1,Name="Камод"},
         new Katalog{Id=2,Name="Кровать"},
          new Katalog{Id=3,Name="Шкаф"},
          new Katalog{Id=4,Name="Кухонный Уголок"},
          new Katalog{Id=5,Name="Стол Обеденный"} ,
          new Katalog{Id=6,Name="Стол Писменный"},
          new Katalog{Id=7,Name="Стол Журнальный"},
          new Katalog{Id=8,Name="Стол Маникюрный"},
          new Katalog{Id=9,Name="Стол Тумба"},
          new Katalog{Id=10,Name="Гномик"},
          new Katalog{Id=11,Name="Комплектующие"}

        };

        Console.WriteLine("Create -----------      ProductType()---------- Start->");

         modelBuilder.Entity<Katalog>().HasData( kagalog);
            base.OnModelCreating(modelBuilder);
  

              
        
     }
    }
}
