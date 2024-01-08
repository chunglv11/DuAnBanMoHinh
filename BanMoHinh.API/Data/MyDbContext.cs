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

            Create(builder);

        }
        private void Create(ModelBuilder builder)
        {
            builder.Entity<Role>().HasData(
                    new Role() { Id = Guid.NewGuid(), Name = "Admin", NormalizedName = "ADMIN" },
                    new Role() { Id = Guid.NewGuid(), Name = "User", NormalizedName = "USER" },
                    new Role() { Id = Guid.NewGuid(), Name = "Guest", NormalizedName = "GUEST" }
                ); 
        
            builder.Entity<Material>().HasData(
                new Material() { Id = Guid.NewGuid(), MaterialName = "Sắt"},
                new Material() { Id = Guid.NewGuid(), MaterialName = "Nhựa pvc"},
                new Material() { Id = Guid.NewGuid(), MaterialName = "Gỗ"}
            );
            builder.Entity<Rank>().HasData(
                new Rank() { Id = Guid.NewGuid(), Name = "Bạc", PointsMin = 0,PoinsMax = 1000000},
                new Rank() { Id = Guid.NewGuid(), Name = "Vàng", PointsMin = 1000001, PoinsMax =  3000000},
                new Rank() { Id = Guid.NewGuid(), Name = "Kim Cương", PointsMin = 3000001, PoinsMax = 10000000 }
            );

            builder.Entity<OrderStatus>().HasData(
                    new OrderStatus() { Id = Guid.NewGuid(), OrderStatusName = "Đang được xử lý" },
                    new OrderStatus() { Id = Guid.NewGuid(), OrderStatusName = "Chờ lấy hàng" },
                    new OrderStatus() { Id = Guid.NewGuid(), OrderStatusName = "Đang giao hàng" },
                    new OrderStatus() { Id = Guid.NewGuid(), OrderStatusName = "Giao hàng thành công" },
                    new OrderStatus() { Id = Guid.NewGuid(), OrderStatusName = "Giao hàng không thành công" },
                    new OrderStatus() { Id = Guid.NewGuid(), OrderStatusName = "Hủy đơn" },
                    new OrderStatus() { Id = Guid.NewGuid(), OrderStatusName = "Yêu cầu trả hàng" },
                    new OrderStatus() { Id = Guid.NewGuid(), OrderStatusName = "Chấp nhận trả hàng" }
                );
            builder.Entity<Brand>().HasData(
                new Brand() { Id = Guid.NewGuid(), BrandName = "Brand 1" },
                new Brand() { Id = Guid.NewGuid(), BrandName = "Brand 2" },
                new Brand() { Id = Guid.NewGuid(), BrandName = "Brand 3" },
                new Brand() { Id = Guid.NewGuid(), BrandName = "Brand 4" },
                new Brand() { Id = Guid.NewGuid(), BrandName = "Brand 5" }
            );
            builder.Entity<Size>().HasData(
                new Size() { Id = Guid.NewGuid(), SizeName = "Size 1",Height=30,Width = 30 },
                new Size() { Id = Guid.NewGuid(), SizeName = "Size 2",Height=30,Width = 30 },
                new Size() { Id = Guid.NewGuid(), SizeName = "Size 3",Height=30,Width = 30 },
                new Size() { Id = Guid.NewGuid(), SizeName = "Size 4",Height=30,Width = 30 },
                new Size() { Id = Guid.NewGuid(), SizeName = "Size 5",Height=30,Width = 30 }

            );

            builder.Entity<Category>().HasData(
                new Category() { Id = Guid.NewGuid(), CategoryName = "Category 1" },
                new Category() { Id = Guid.NewGuid(), CategoryName = "Category 2" },
                new Category() { Id = Guid.NewGuid(), CategoryName = "Category 3" },
                new Category() { Id = Guid.NewGuid(), CategoryName = "Category 4" },
                new Category() { Id = Guid.NewGuid(), CategoryName = "Category 5" }
            );
            builder.Entity<Payment>().HasData(
                    new Payment() { Id = Guid.NewGuid(), PaymentName = "Thanh toán khi nhận hàng" },
                    new Payment() { Id = Guid.NewGuid(), PaymentName = "Thanh toán Online" }
                );
            builder.Entity<Colors>().HasData(
               new Colors() { ColorId = Guid.NewGuid(), ColorName = "Đen", ColorCode = "#000000" },
               new Colors() { ColorId = Guid.NewGuid(), ColorName = "Trắng", ColorCode = "#FFFFFF" },
               new Colors() { ColorId = Guid.NewGuid(), ColorName = "Đỏ", ColorCode = "#FF0000" },
               new Colors() { ColorId = Guid.NewGuid(), ColorName = "Xanh lá cây", ColorCode = "#00FF00" },
               new Colors() { ColorId = Guid.NewGuid(), ColorName = "Xanh dương", ColorCode = "#0000FF" },
               new Colors() { ColorId = Guid.NewGuid(), ColorName = "Vàng", ColorCode = "#FFFF00" },
               new Colors() { ColorId = Guid.NewGuid(), ColorName = "Cam", ColorCode = "#FFA500" },
               new Colors() { ColorId = Guid.NewGuid(), ColorName = "Tím", ColorCode = "#800080" },
               new Colors() { ColorId = Guid.NewGuid(), ColorName = "Hồng", ColorCode = "#FFC0CB" },
               new Colors() { ColorId = Guid.NewGuid(), ColorName = "Xám", ColorCode = "#808080" },
               new Colors() { ColorId = Guid.NewGuid(), ColorName = "Nâu", ColorCode = "#A52A2A" },
               new Colors() { ColorId = Guid.NewGuid(), ColorName = "Xanh lam", ColorCode = "#000080" },
               new Colors() { ColorId = Guid.NewGuid(), ColorName = "Xanh da trời", ColorCode = "#00BFFF" },
               new Colors() { ColorId = Guid.NewGuid(), ColorName = "Hồng phấn", ColorCode = "#FFDAB9" },
               new Colors() { ColorId = Guid.NewGuid(), ColorName = "Xám tro", ColorCode = "#C0C0C0" },
               new Colors() { ColorId = Guid.NewGuid(), ColorName = "Bạc", ColorCode = "#C0C0C0" }
                );
            
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
        public DbSet<Colors> Colors { get; set; }
        public DbSet<Voucher> Voucher { get; set; }
        public DbSet<UserVoucher> VoucherUser { get; set; }
        public DbSet<Rate> Rate { get; set; }
        public DbSet<Post> Posts { get; set; }

    }
}
