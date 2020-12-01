using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

// partial class должны быть в одном и томже namespace !!!!
// не получилось использовать partial в разных проектах ShopDbLib and WibShopApi
namespace ShopDbLib.Models
{
    public partial class AppDbContext : DbContext
    {     IConfiguration _config;

        

        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Katalog> Katalog { get; set; }
        public new virtual DbSet<Model> Model { get; set; }
        public virtual DbSet<Component> Nomenclature { get; set; }
        public virtual DbSet<NomenclaturaComponents> NomenclaturaComponents { get; set; }
        public virtual DbSet<Nomenclatura> Nomenclatura { get; set; }
        public DbSet<User> Users { get; set; }


        //----------------- 
        public AppDbContext(DbContextOptions<AppDbContext> options,IConfiguration config)
            : base(options)
        { 
            _config=config;
             Database.SetCommandTimeout(300);
         // Database.EnsureDeleted();  //23.11.20
            Database.EnsureCreated();
        }


        //------------------------------------------------

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql( _config["ConnectStringLocal"]);      //Startup.GetConnetionStringDB());
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

                entity.HasOne(d => d.Nomenclatura)
                    .WithMany(p => p.Image)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Image_Nomenclatura1");
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

            modelBuilder.Entity<Component>(entity =>
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

            modelBuilder.Entity<NomenclaturaComponents>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.ComponentId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.HasIndex(e => e.ComponentId)
                    .HasName("fk_Состав_Component1_idx");

                entity.HasIndex(e => e.ProductId)
                    .HasName("fk_Состав_Product1_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ComponentId).HasColumnName("Component_id");

                entity.Property(e => e.ProductId).HasColumnName("Product_id");

                entity.HasOne(d => d.Component)
                    .WithMany(p => p.NomenclaturaComponents)
                    .HasForeignKey(d => d.ComponentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Состав_Components1");

                entity.HasOne(d => d.Nomenclatura)
                    .WithMany(p => p.NomenclaturaComponents)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Состав_Product1");
            });

            modelBuilder.Entity<Nomenclatura>(entity =>
            {
                entity.HasIndex(e => e.ModelId)
                    .HasName("fk_Nomenclatura_Model1_idx");

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
                    .WithMany(p => p.Nomenclatura)
                    .HasForeignKey(d => d.ModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Nomenclatura_Model1");
            });

            OnModelCreatingPartial(modelBuilder);
        }


        //Здесь инициалицируем  БД (субд)  начальными данными  
          partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            OnModelKatalogCreating(modelBuilder);
            OnModelAuthCreating(modelBuilder);
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
     
        //  при создании бд  создается admin 
        private void OnModelAuthCreating(ModelBuilder modelBuilder)
        {

            var initObject = _config.GetSection("Users");
            var admin = initObject.GetSection("Admin");
            var user = initObject.GetSection("User");
            string adminEmail = admin["Email"];// ";
            string adminPassword = admin["Password"];
            string userEmail = user["Email"]; //  "user@mail.ru";
            string userPassword = user["Password"];// "";
            string userPhone = user["Phone"];// "+79181111111";
            string userName = "user";

            // добавляем роли

            User adminUser = new User
            {
                Id = 1,
                Email = adminEmail,
                Password = adminPassword,
                Role = Role.Admin,
                Name = admin["Name"]
            ,
                Address = "",
                Phone = admin["Phone"]
            };
            User user1 = new User
            {
                Id = 2,
                Email = userEmail,
                Password = userPassword,
                Name = userName,
                Role = Role.User,
                Address = "",
                Phone = userPhone
            };
            // modelBuilder.Entity<User>().Property(u=>u.Role).HasDefaultValue(Role.User);

            modelBuilder.Entity<User>().HasData(new User[] { adminUser, user1 });
            base.OnModelCreating(modelBuilder);


        }
    }
}
