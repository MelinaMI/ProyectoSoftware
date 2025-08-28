using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //Declaro las entidades
        public DbSet<Category> Category { get; set; }
        public DbSet<DeliveryType> DeliveryType { get; set; }
        public DbSet<Dish> Dish { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Status> Status { get; set; }


        //modelado de las relaciones
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // CATEGORY
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id); //PK
                entity.Property(c => c.Id).ValueGeneratedOnAdd(); //autogenerada
                entity.Property(c => c.Name).IsRequired().HasColumnType("varchar(25)");
                entity.Property(c => c.Description).IsRequired().HasColumnType("varchar(255)");
                entity.Property(c => c.Order).HasColumnType("int");
            });

            // DELIVERYTYPE
            modelBuilder.Entity<DeliveryType>(entity =>
            {
                entity.HasKey(dt => dt.Id); //PK
                entity.Property(dt => dt.Id).ValueGeneratedOnAdd(); //autogenerada
                entity.Property(dt => dt.Name).IsRequired().HasColumnType("nvarchar(25)").HasMaxLength(25);
            });

            // DISH
            modelBuilder.Entity<Dish>(entity =>
            {
                entity.HasKey(d => d.DishId); //PK
                entity.Property(d => d.DishId).ValueGeneratedOnAdd(); //autogenerada
                entity.Property(d => d.Name).IsRequired().HasColumnType("varchar(255)").HasMaxLength(255);
                entity.Property(d => d.Description).HasColumnType("varchar(MAX)");
                entity.Property(d => d.Price).HasColumnType("decimal(18,2)");
                entity.Property(d => d.Available).HasColumnType("bool");
                entity.Property(d => d.ImageUrl).HasColumnType("varchar(MAX)");
                entity.Property(d => d.CreateDate).HasColumnType("datetime");
                entity.Property(d => d.UpdateDate).HasColumnType("datetime");

                //FK hacia Category
                entity.HasOne(d => d.CategoryNavigation)
                .WithMany(c => c.Dishes)
                .HasForeignKey(d => d.Category)
                .IsRequired();
            });

            // ORDER
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.OrderId); //PK
                entity.Property(o => o.OrderId).ValueGeneratedOnAdd(); //autogenerada
                entity.Property(o => o.DeliveryTo).IsRequired().HasColumnType("varchar(255)").HasMaxLength(255);
                entity.Property(o => o.Notes).HasColumnType("varchar(MAX)");
                entity.Property(o => o.Price).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(o => o.CreateDate).HasColumnType("datetime");
                entity.Property(o => o.UpdateDate).HasColumnType("datetime");

                // FK hacia Status
                entity.HasOne(o => o.OverallStatusNavigation)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.OverallStatus)
                .IsRequired();

                // FK hacia DeliveryType
                entity.HasOne(o => o.DeliveryTypeNavigation)
                .WithMany(dt => dt.Orders)
                .HasForeignKey(o => o.DeliveryType)
                .IsRequired();
            });

            // ORDERITEM
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(oi => oi.OrderItemId); //PK
                entity.Property(oi => oi.OrderItemId).ValueGeneratedOnAdd(); //autogenerada
                entity.Property(oi => oi.Quantity).IsRequired().HasColumnType("int");
                entity.Property(oi => oi.Notes).HasColumnType("varchar(MAX)");
                entity.Property(oi => oi.CreateDate).HasColumnType("datetime");

                // FK hacia Order
                entity.HasOne(oi => oi.OrderNavigation)
                .WithMany(s => s.OrderItems) //apunta a la coleccion de OrderItems en Order
                .HasForeignKey(o => o.Order)
                .IsRequired();

                // FK hacia Dish
                entity.HasOne(oi => oi.DishNavigation)
                .WithMany(d => d.OrderItems) //apunta a la coleccion de OrderItems en Dish
                .HasForeignKey(oi => oi.Dish)
                .IsRequired();

                // FK hacia Status
                entity.HasOne(oi => oi.StatusNavigation)
                .WithMany(s => s.OrderItems) //apunta a la coleccion de OrderItems en Status
                .HasForeignKey(oi => oi.Status)
                .IsRequired();
            });

            // STATUS
            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(s => s.Id); //PK
                entity.Property(s => s.Id).ValueGeneratedOnAdd(); //autogenerada
                entity.Property(s => s.Name).IsRequired().HasColumnType("varchar(25)").HasMaxLength(25);
            });
        }
    }
}
