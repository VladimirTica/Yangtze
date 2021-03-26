using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Yangtze.DAL.Entities
{
    public partial class YangtzeDBContext : DbContext
    {
        public YangtzeDBContext()
        {
        }

        public YangtzeDBContext(DbContextOptions<YangtzeDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<CartItem> CartItem { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<ProductReview> ProductReview { get; set; }
        public virtual DbSet<Transaction> Transaction { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<WhishedProduct> WhishedProduct { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("Server=rds-yangtze-dev.clmdd8isw4nl.eu-central-1.rds.amazonaws.com;Port=3306;Database=yangtze;Uid=VladimirDev;Pwd=1Vladimir3Tica!1;");
            }
        }

    }
}
