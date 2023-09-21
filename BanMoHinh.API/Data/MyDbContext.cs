using BanMoHinh.Share.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BanMoHinh.API.Data
{
    public class MyDbContext : IdentityDbContext<User, Role, Guid>
    {
        public MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Rank> Rank { get; set; }
        public DbSet<Adress> Adresses { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Material> Material { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<WishList> WishList { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductImage> ProductImage { get; set; }
        public DbSet<ProductDetail> ProductDetail { get; set; }
        public DbSet<Size> Size { get; set; }
        public DbSet<Voucher> Voucher { get; set; }
        public DbSet<VoucherType> VoucherType { get; set; }
        public DbSet<VoucherDetails> VoucherDetails { get; set; }
        public DbSet<UserVoucher> VoucherUser { get; set; }
        public DbSet<Rate> Rate { get; set; }
        public DbSet<Post> Posts { get; set; }

    }
}
