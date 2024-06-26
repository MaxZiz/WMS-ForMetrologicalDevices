using Microsoft.EntityFrameworkCore;
using WMS.Domain.Entities;
using WMS.Domain.Enums;
using WMS.Domain.Helpers;

namespace WMS.DAL
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        public DbSet<Device> Device { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Basket> Basket { get; set; }
        public DbSet<ToDoList> ToDoList { get; set; }
        public DbSet<NameList> NameLists { get; set; }
        public DbSet<Guide> Guides { get; set; }
        public DbSet<TypeOfDevice> TypeOfDevices { get; set; }
        public DbSet<Paragraph> Paragraphs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {       
            modelBuilder.Entity<User>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.HasData(new User()
                {
                    Id = 1,
                    Name = "Admin",
                    Password = HashPassword.GetHashPassword("123456"),
                    Role = Role.Administrator
                });

                builder.HasOne(x => x.Basket)
                .WithOne(x => x.User)
                .HasPrincipalKey<User>(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<User>(builder =>
            {
                builder.HasData(new User()
                {
                    Id = 1,
                    Name = "User",
                    Password = HashPassword.GetHashPassword("123456"),
                    Role = Role.User
                });
                builder.HasData(new User()
                {
                    Id = 1,
                    Name = "Moderator",
                    Password = HashPassword.GetHashPassword("123456"),
                    Role = Role.Moderator
                });
            });

            modelBuilder.Entity<Place>()
            .HasMany(b => b.Devices)
            .WithOne(c => c.Place);

            modelBuilder.Entity<Place>().HasData(new Place
            {
                Number = 535,
                Id = 535
            }
                );

            modelBuilder.Entity<Place>().HasData(new Place
            {
                Number = 534,
                Id = 536
            }
                );


            modelBuilder.Entity<Device>(b =>
            {
                b.HasData(new
                {
                    Id = 5,
                    Name = "Appa",
                    Description = "Мультиметр",
                    TypeDevice = TypeDevice.Device,
                    DateCreate = System.DateTime.Now,
                    PlaceId = 535
                }
              );
                b.OwnsMany(e => e.Histories).HasData(
                    new
                    {
                        _Guid = System.Guid.NewGuid(),
                        DateTime = System.DateTime.Now,
                        FromPlace = 1,
                        ToPlace = 535,
                        DeviceId = 1
                    }
                    );
            });
        
            modelBuilder.Entity<Profile>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.HasData(new Profile()
                {
                    Id = 1,
                    UserId = 1,

                });
            });

            modelBuilder.Entity<Basket>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.HasData(new Basket()
                {
                    Id = 1,
                    UserId = 1,
                });
            });

            modelBuilder.Entity<ToDoList>(builder =>
            {
                builder.HasData(new ToDoList()
                {
                    Id = 1,
                    DateCreation = System.DateTime.Now,
                    UserId = 1,
                    ToPlace = 535,

                });
            });

            modelBuilder.Entity<Guide>(builder =>
            {
                builder.HasKey(x => x.Id);
            });

            modelBuilder.Entity<NameList>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.HasData(new NameList()
                {
                    Id = 1,
                    Name = "Appa 91",
                });
            });
        }
    }
}