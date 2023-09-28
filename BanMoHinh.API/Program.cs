using BanMoHinh.API.Data;
using BanMoHinh.API.IServices;
using BanMoHinh.API.Services;
using BanMoHinh.Share.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//conect sql
builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyCs"));
});
// add Dependency Injection
builder.Services.AddTransient<ISizeService, SizeService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IProductDetailService, ProductDetailService>();
builder.Services.AddTransient<IProductImageService, ProductImageService>();
// Add Identity
builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<MyDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
