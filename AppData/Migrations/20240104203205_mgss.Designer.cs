﻿// <auto-generated />
using System;
using AppData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AppData.Migrations
{
    [DbContext(typeof(BanMoHinhDBContext))]
    [Migration("20240104203205_mgss")]
    partial class mgss
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AppData.Models.Anh", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DuongDan")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<Guid?>("IDMauSac")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("IDSanPham")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("IDMauSac");

                    b.HasIndex("IDSanPham");

                    b.ToTable("Anh", (string)null);
                });

            modelBuilder.Entity("AppData.Models.BaiDang", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AnhDaiDien")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("IdNV")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("NgayCapNhat")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("NgayDang")
                        .HasColumnType("datetime");

                    b.Property<string>("NoiDung")
                        .IsRequired()
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<string>("TieuDe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("IdNV");

                    b.ToTable("BaiDang", (string)null);
                });

            modelBuilder.Entity("AppData.Models.ChatLieu", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("ChatLieu", (string)null);
                });

            modelBuilder.Entity("AppData.Models.ChiTietGioHang", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IDCTSP")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IDNguoiDung")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("IDCTSP");

                    b.HasIndex("IDNguoiDung");

                    b.ToTable("ChiTietGioHang", (string)null);
                });

            modelBuilder.Entity("AppData.Models.ChiTietHoaDon", b =>
                {
                    b.Property<Guid>("ID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DonGia")
                        .HasColumnType("int");

                    b.Property<Guid>("IDCTSP")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IDHoaDon")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("IDCTSP");

                    b.HasIndex("IDHoaDon");

                    b.ToTable("ChiTietHoaDon", (string)null);
                });

            modelBuilder.Entity("AppData.Models.ChiTietSanPham", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("GiaBan")
                        .HasColumnType("int");

                    b.Property<Guid?>("IDKhuyenMai")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IDKichCo")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IDMauSac")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IDSanPham")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ma")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("IDKhuyenMai");

                    b.HasIndex("IDKichCo");

                    b.HasIndex("IDMauSac");

                    b.HasIndex("IDSanPham");

                    b.ToTable("ChiTietSanPham", (string)null);
                });

            modelBuilder.Entity("AppData.Models.DanhGia", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BinhLuan")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime?>("NgayDanhGia")
                        .HasColumnType("datetime");

                    b.Property<int?>("Sao")
                        .HasColumnType("int");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("DanhGia", (string)null);
                });

            modelBuilder.Entity("AppData.Models.DiaChi", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DiaChiChiTiet")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Huyen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("IdKhachHang")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("IsDefault")
                        .HasColumnType("bit");

                    b.Property<string>("Sdt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThanhPho")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Xa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdKhachHang");

                    b.ToTable("DiaChi", (string)null);
                });

            modelBuilder.Entity("AppData.Models.GioHang", b =>
                {
                    b.Property<Guid>("IDKhachHang")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime");

                    b.HasKey("IDKhachHang");

                    b.ToTable("GioHang", (string)null);
                });

            modelBuilder.Entity("AppData.Models.HoaDon", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DiaChi")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("GhiChu")
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid?>("IDNhanVien")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("IDVoucher")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("LoaiHD")
                        .HasColumnType("int");

                    b.Property<string>("MaHD")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgayNhanHang")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("NgayThanhToan")
                        .HasColumnType("datetime");

                    b.Property<string>("PhuongThucThanhToan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SDT")
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("TenNguoiNhan")
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TienShip")
                        .HasColumnType("int");

                    b.Property<int?>("TongTien")
                        .HasColumnType("int");

                    b.Property<int>("TrangThaiGiaoHang")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("IDNhanVien");

                    b.HasIndex("IDVoucher");

                    b.ToTable("HoaDon", (string)null);
                });

            modelBuilder.Entity("AppData.Models.KhachHang", b =>
                {
                    b.Property<Guid>("IDKhachHang")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DiaChi")
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("DiemTich")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(250)");

                    b.Property<int?>("GioiTinh")
                        .HasColumnType("int");

                    b.Property<Guid>("IDRank")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("IDVoucher")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("NgaySinh")
                        .HasColumnType("datetime");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(MAX)");

                    b.Property<string>("SDT")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("IDKhachHang");

                    b.HasIndex("IDRank");

                    b.HasIndex("IDVoucher");

                    b.ToTable("KhachHang", (string)null);
                });

            modelBuilder.Entity("AppData.Models.KhuyenMai", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("GiaTri")
                        .HasColumnType("int");

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("NgayApDung")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("NgayKetThuc")
                        .HasColumnType("datetime");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("KhuyenMai", (string)null);
                });

            modelBuilder.Entity("AppData.Models.KichCo", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int?>("TrangThai")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("KichCo", (string)null);
                });

            modelBuilder.Entity("AppData.Models.LichSuTichDiem", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IDHoaDon")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("IDKhachHang")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("IDHoaDon");

                    b.HasIndex("IDKhachHang");

                    b.ToTable("LichSuTichDiem", (string)null);
                });

            modelBuilder.Entity("AppData.Models.LoaiSP", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("IDLoaiSPCha")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("IDLoaiSPCha");

                    b.ToTable("LoaiSP", (string)null);
                });

            modelBuilder.Entity("AppData.Models.MauSac", b =>
                {
                    b.Property<Guid?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ma")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("TrangThai")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("MauSac", (string)null);
                });

            modelBuilder.Entity("AppData.Models.NhanVien", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("IDVaiTro")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PassWord")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("IDVaiTro");

                    b.ToTable("NhanVien", (string)null);

                    b.HasData(
                        new
                        {
                            ID = new Guid("2ec27ab7-5f67-4ed5-ae67-52f9c9726ebf"),
                            DiaChi = "Ha Noi",
                            Email = "bena@gmail.com",
                            IDVaiTro = new Guid("b4996b2d-a343-434b-bfe9-09f8efbb3852"),
                            PassWord = "$2a$12$wQ8TmEzFP4Dom4.h50er/O7GH7j32ehD8UBSqhhaWY67YnaTBJVmC",
                            SDT = "0987654321",
                            Ten = "Admin",
                            TrangThai = 1
                        });
                });

            modelBuilder.Entity("AppData.Models.SanPham", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IDChatLieu")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IDLoaiSP")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ma")
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("IDChatLieu");

                    b.HasIndex("IDLoaiSP");

                    b.ToTable("SanPham", (string)null);
                });

            modelBuilder.Entity("AppData.Models.VaiTro", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("VaiTro", (string)null);

                    b.HasData(
                        new
                        {
                            ID = new Guid("b4996b2d-a343-434b-bfe9-09f8efbb3852"),
                            Ten = "Admin",
                            TrangThai = 1
                        },
                        new
                        {
                            ID = new Guid("952c1a5d-74ff-4daf-ba88-135c5440809c"),
                            Ten = "Nhân viên",
                            TrangThai = 1
                        });
                });

            modelBuilder.Entity("AppData.Models.Voucher", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("GiaTri")
                        .HasColumnType("int");

                    b.Property<int>("HinhThucGiamGia")
                        .HasColumnType("int");

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("NgayApDung")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("NgayKetThuc")
                        .HasColumnType("datetime");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<int>("SoTienCan")
                        .HasColumnType("int");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Voucher", (string)null);
                });

            modelBuilder.Entity("AppData.Models.XepHang", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Mota")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("TienMax")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("TienMin")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("XepHang", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("491abc2c-3bfa-47dd-a55c-ed065295374c"),
                            Mota = "Thành viên",
                            Ten = "Đồng",
                            TienMax = 0,
                            TienMin = 0
                        },
                        new
                        {
                            Id = new Guid("c6c70bab-e95b-4e78-aaf1-4077e9508332"),
                            Mota = "rank bạc",
                            Ten = "Bạc",
                            TienMax = 2000000,
                            TienMin = 1000000
                        },
                        new
                        {
                            Id = new Guid("02a277fd-e8b4-42ae-b305-9d598fce3c80"),
                            Mota = "rank vàng",
                            Ten = "Vàng",
                            TienMax = 4000000,
                            TienMin = 2000000
                        },
                        new
                        {
                            Id = new Guid("376e1049-0e36-4b89-a240-b5eb8409f503"),
                            Mota = "rank kim cương",
                            Ten = "Kim Cương",
                            TienMax = 9000000,
                            TienMin = 4000000
                        });
                });

            modelBuilder.Entity("AppData.Models.Anh", b =>
                {
                    b.HasOne("AppData.Models.MauSac", "MauSac")
                        .WithMany("Anhs")
                        .HasForeignKey("IDMauSac");

                    b.HasOne("AppData.Models.SanPham", "SanPham")
                        .WithMany("Anhs")
                        .HasForeignKey("IDSanPham");

                    b.Navigation("MauSac");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("AppData.Models.BaiDang", b =>
                {
                    b.HasOne("AppData.Models.NhanVien", "nhanVien")
                        .WithMany("BaiDangs")
                        .HasForeignKey("IdNV");

                    b.Navigation("nhanVien");
                });

            modelBuilder.Entity("AppData.Models.ChiTietGioHang", b =>
                {
                    b.HasOne("AppData.Models.ChiTietSanPham", "ChiTietSanPham")
                        .WithMany("ChiTietGioHangs")
                        .HasForeignKey("IDCTSP")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppData.Models.GioHang", "GioHang")
                        .WithMany("ChiTietGioHangs")
                        .HasForeignKey("IDNguoiDung")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChiTietSanPham");

                    b.Navigation("GioHang");
                });

            modelBuilder.Entity("AppData.Models.ChiTietHoaDon", b =>
                {
                    b.HasOne("AppData.Models.DanhGia", "DanhGia")
                        .WithOne("ChiTietHoaDon")
                        .HasForeignKey("AppData.Models.ChiTietHoaDon", "ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppData.Models.ChiTietSanPham", "ChiTietSanPham")
                        .WithMany("ChiTietHoaDons")
                        .HasForeignKey("IDCTSP")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppData.Models.HoaDon", "HoaDon")
                        .WithMany("ChiTietHoaDons")
                        .HasForeignKey("IDHoaDon")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChiTietSanPham");

                    b.Navigation("DanhGia");

                    b.Navigation("HoaDon");
                });

            modelBuilder.Entity("AppData.Models.ChiTietSanPham", b =>
                {
                    b.HasOne("AppData.Models.KhuyenMai", "KhuyenMai")
                        .WithMany("ChiTietSanPhams")
                        .HasForeignKey("IDKhuyenMai");

                    b.HasOne("AppData.Models.KichCo", "KichCo")
                        .WithMany("ChiTietSanPhams")
                        .HasForeignKey("IDKichCo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppData.Models.MauSac", "MauSac")
                        .WithMany("ChiTietSanPhams")
                        .HasForeignKey("IDMauSac")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppData.Models.SanPham", "SanPham")
                        .WithMany("ChiTietSanPhams")
                        .HasForeignKey("IDSanPham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KhuyenMai");

                    b.Navigation("KichCo");

                    b.Navigation("MauSac");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("AppData.Models.DiaChi", b =>
                {
                    b.HasOne("AppData.Models.KhachHang", "KhachHang")
                        .WithMany("DiaChis")
                        .HasForeignKey("IdKhachHang");

                    b.Navigation("KhachHang");
                });

            modelBuilder.Entity("AppData.Models.HoaDon", b =>
                {
                    b.HasOne("AppData.Models.NhanVien", "NhanVien")
                        .WithMany("HoaDons")
                        .HasForeignKey("IDNhanVien");

                    b.HasOne("AppData.Models.Voucher", "Voucher")
                        .WithMany("HoaDons")
                        .HasForeignKey("IDVoucher");

                    b.Navigation("NhanVien");

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("AppData.Models.KhachHang", b =>
                {
                    b.HasOne("AppData.Models.GioHang", "GioHang")
                        .WithOne("KhachHang")
                        .HasForeignKey("AppData.Models.KhachHang", "IDKhachHang")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppData.Models.XepHang", "XepHang")
                        .WithMany("KhachHangs")
                        .HasForeignKey("IDRank")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppData.Models.Voucher", "Voucher")
                        .WithMany("KhachHangs")
                        .HasForeignKey("IDVoucher");

                    b.Navigation("GioHang");

                    b.Navigation("Voucher");

                    b.Navigation("XepHang");
                });

            modelBuilder.Entity("AppData.Models.LichSuTichDiem", b =>
                {
                    b.HasOne("AppData.Models.HoaDon", "HoaDon")
                        .WithMany("LichSuTichDiems")
                        .HasForeignKey("IDHoaDon")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppData.Models.KhachHang", "KhachHang")
                        .WithMany("LichSuTichDiems")
                        .HasForeignKey("IDKhachHang");

                    b.Navigation("HoaDon");

                    b.Navigation("KhachHang");
                });

            modelBuilder.Entity("AppData.Models.LoaiSP", b =>
                {
                    b.HasOne("AppData.Models.LoaiSP", "LoaiSPCha")
                        .WithMany()
                        .HasForeignKey("IDLoaiSPCha");

                    b.Navigation("LoaiSPCha");
                });

            modelBuilder.Entity("AppData.Models.NhanVien", b =>
                {
                    b.HasOne("AppData.Models.VaiTro", "VaiTro")
                        .WithMany("NhanViens")
                        .HasForeignKey("IDVaiTro")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VaiTro");
                });

            modelBuilder.Entity("AppData.Models.SanPham", b =>
                {
                    b.HasOne("AppData.Models.ChatLieu", "ChatLieu")
                        .WithMany("SanPhams")
                        .HasForeignKey("IDChatLieu")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppData.Models.LoaiSP", "LoaiSP")
                        .WithMany("SanPhams")
                        .HasForeignKey("IDLoaiSP")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChatLieu");

                    b.Navigation("LoaiSP");
                });

            modelBuilder.Entity("AppData.Models.ChatLieu", b =>
                {
                    b.Navigation("SanPhams");
                });

            modelBuilder.Entity("AppData.Models.ChiTietSanPham", b =>
                {
                    b.Navigation("ChiTietGioHangs");

                    b.Navigation("ChiTietHoaDons");
                });

            modelBuilder.Entity("AppData.Models.DanhGia", b =>
                {
                    b.Navigation("ChiTietHoaDon")
                        .IsRequired();
                });

            modelBuilder.Entity("AppData.Models.GioHang", b =>
                {
                    b.Navigation("ChiTietGioHangs");

                    b.Navigation("KhachHang");
                });

            modelBuilder.Entity("AppData.Models.HoaDon", b =>
                {
                    b.Navigation("ChiTietHoaDons");

                    b.Navigation("LichSuTichDiems");
                });

            modelBuilder.Entity("AppData.Models.KhachHang", b =>
                {
                    b.Navigation("DiaChis");

                    b.Navigation("LichSuTichDiems");
                });

            modelBuilder.Entity("AppData.Models.KhuyenMai", b =>
                {
                    b.Navigation("ChiTietSanPhams");
                });

            modelBuilder.Entity("AppData.Models.KichCo", b =>
                {
                    b.Navigation("ChiTietSanPhams");
                });

            modelBuilder.Entity("AppData.Models.LoaiSP", b =>
                {
                    b.Navigation("SanPhams");
                });

            modelBuilder.Entity("AppData.Models.MauSac", b =>
                {
                    b.Navigation("Anhs");

                    b.Navigation("ChiTietSanPhams");
                });

            modelBuilder.Entity("AppData.Models.NhanVien", b =>
                {
                    b.Navigation("BaiDangs");

                    b.Navigation("HoaDons");
                });

            modelBuilder.Entity("AppData.Models.SanPham", b =>
                {
                    b.Navigation("Anhs");

                    b.Navigation("ChiTietSanPhams");
                });

            modelBuilder.Entity("AppData.Models.VaiTro", b =>
                {
                    b.Navigation("NhanViens");
                });

            modelBuilder.Entity("AppData.Models.Voucher", b =>
                {
                    b.Navigation("HoaDons");

                    b.Navigation("KhachHangs");
                });

            modelBuilder.Entity("AppData.Models.XepHang", b =>
                {
                    b.Navigation("KhachHangs");
                });
#pragma warning restore 612, 618
        }
    }
}
