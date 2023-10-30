﻿using BanMoHinh.API.Common;
using BanMoHinh.API.Data;
using BanMoHinh.API.IServices;
using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.Net.Http.Headers;
using System.Net.Mail;
using static System.Net.Mime.MediaTypeNames;

namespace BanMoHinh.API.Services
{
    public class ProductDetailService : IProductDetailService
    {
        private MyDbContext _dbContext;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "Images";
        public ProductDetailService(MyDbContext dbContext, IStorageService storageService)
        {
            _dbContext = dbContext;
            _storageService = storageService;
        }
        //luu anh vao thu muc wwwroot o api

        public async Task<bool> Create(ProductDetailVM item)
        {
            try
            {

                ProductDetail productDetail = new ProductDetail()
                {
                    Id = Guid.NewGuid(),
                    ProductId = item.ProductId,
                    SizeId = item.SizeId,
                    ColorId = item.ColorId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    PriceSale = item.PriceSale,
                    Create_At = item.Create_At,
                    Update_At = item.Update_At,
                    Description = item.Description,
                    Status = item.Status,
                };
                await _dbContext.ProductDetail.AddAsync(productDetail);
                await _dbContext.SaveChangesAsync();
                int passcount = 0;
                //save image
                if (item.filecollection != null)//không null 
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
                    foreach (var i in item.filecollection)
                    {

                        string imgPath = path + "\\" + i.FileName;
                        using (var stream = new FileStream(imgPath, FileMode.Create))
                        {
                            await i.CopyToAsync(stream);
                            passcount++;
                        }

                        var proi = new ProductImage()
                        {
                            Id = Guid.NewGuid(),
                            ProductDetailId = productDetail.Id,
                            ImageUrl = imgPath
                        };
                        await _dbContext.ProductImage.AddAsync(proi);
                    }

                    await _dbContext.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> CreateMany(List<ProductDetailVM> items)
        {
            try
            {
                var LstProductDetail = new List<ProductDetail>();
                foreach (var x in items)
                {
                    ProductDetail productDetail = new ProductDetail()
                    {
                        Id = Guid.NewGuid(),
                        ProductId = x.ProductId,
                        SizeId = x.SizeId,
                        ColorId = x.ColorId,
                        Quantity = x.Quantity,
                        Price = x.Price,
                        PriceSale = x.PriceSale,
                        Create_At = x.Create_At,
                        Update_At = x.Update_At,
                        Description = x.Description,
                        Status = x.Status,
                    };
                    LstProductDetail.Add(productDetail);
                }
                await _dbContext.ProductDetail.AddRangeAsync(LstProductDetail);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var productDt = await _dbContext.ProductDetail.FindAsync(id) ?? throw new Exception("Không tìm thấy sản phẩm");
                var images = _dbContext.ProductImage.Where(i => i.ProductDetailId == id);
                _dbContext.ProductImage.RemoveRange(images);
                await _dbContext.SaveChangesAsync();
                _dbContext.ProductDetail.Remove(productDt);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<ProductDetailVM>> GetAll()
        {
            var lst = from a in _dbContext.ProductDetail
                      join b in _dbContext.Size on a.SizeId equals b.Id
                      join c in _dbContext.Colors on a.ColorId equals c.ColorId
                      join d in _dbContext.Product on a.ProductId equals d.Id
                      select new ProductDetailVM
                      {
                          Id = a.Id,
                          ProductName = d.ProductName,
                          SizeName = b.SizeName,
                          ColorName = c.ColorName,
                          Quantity = a.Quantity,
                          Price = a.Price,
                          PriceSale = a.PriceSale,
                          Create_At = a.Create_At,
                          Update_At = a.Update_At,
                          Description = a.Description,
                          Status = a.Status
                      };
            return lst.ToList();
        }

        public async Task<ProductDetailVM> GetItem(Guid id)
        {
            var prD = await _dbContext.ProductDetail.FindAsync(id);
            var product = _dbContext.Product.FirstOrDefault(x => x.Id == prD.ProductId);
            var size = _dbContext.Size.FirstOrDefault(x => x.Id == prD.SizeId);
            var color = _dbContext.Colors.FirstOrDefault(x => x.ColorId == prD.ColorId);
            var img = (from a in _dbContext.ProductImage
                       join b in _dbContext.ProductDetail on a.ProductDetailId equals b.Id
                       where a.ProductDetailId == prD.Id
                       select a.ImageUrl).ToList();//khó hiểu, dùng asyn k tìm được, dùng 1 luồng lại được ???
            var lstPrd = new ProductDetailVM()
            {
                Id = id,
                Images = img,
                ProductId = prD.ProductId,
                ProductName = product.ProductName,
                SizeId = prD.SizeId,
                SizeName = size.SizeName,
                ColorId = prD.ColorId,
                ColorName = color.ColorName,
                Quantity = prD.Quantity,
                Price = prD.Price,
                PriceSale = prD.PriceSale,
                Create_At = prD.Create_At,
                Update_At = prD.Update_At,
                Description = prD.Description,
                Status = prD.Status
            };
            return lstPrd;
        }

        public async Task<bool> Update(ProductDetailVM item)
        {
            var idp = await _dbContext.ProductDetail.FindAsync(item.Id);

            if (idp == null)
            {
                throw new Exception($"không tìm thấy sản phẩm có id: {item.Id}");
            }
            else
            {
                idp.SizeId = item.SizeId;
                idp.ColorId = item.ColorId;
                idp.ProductId = item.ProductId;
                idp.Quantity = item.Quantity;
                idp.Price = item.Price;
                idp.PriceSale = item.PriceSale;
                idp.Create_At = item.Create_At;
                idp.Update_At = item.Update_At;
                idp.Description = item.Description;
                idp.Status = item.Status;
                //var images = _dbContext.ProductImage.Where(i => i.ProductDetailId == idp.Id);
                //_dbContext.ProductImage.RemoveRange(images);
                int passcount = 0;
                //save image
                if (item.filecollection != null)//không null 
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
                    foreach (var i in item.filecollection)
                    {

                        string imgPath = path + "\\" + i.FileName;
                        using (var stream = new FileStream(imgPath, FileMode.Create))
                        {
                            await i.CopyToAsync(stream);
                            passcount++;
                        }
                        var proi = new ProductImage()
                        {
                            ProductDetailId = idp.Id,
                            ImageUrl = i.FileName
                        };
                        _dbContext.ProductImage.Update(proi);
                    }
                    _dbContext.ProductDetail.Update(idp);
                }

                await _dbContext.SaveChangesAsync();
                return true;
            }
        }
    }
}
