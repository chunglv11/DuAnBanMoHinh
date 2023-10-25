using BanMoHinh.API.Data;
using BanMoHinh.API.IServices;
using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BanMoHinh.API.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly MyDbContext _dbContext;
        public MaterialService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Create(MaterialVM item)
        {
            try
            {
                var mater = new Material()
                {
                  MaterialName = item.MaterialName,
                };
                await _dbContext.Material.AddAsync(mater);
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
                var item = await _dbContext.Brand.FirstOrDefaultAsync(c => c.Id == id);
                _dbContext.Remove(item);
                await _dbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<List<Material>> GetAll()
        {
            return await _dbContext.Material.ToListAsync();
        }


            public async Task<Material> GetItem(Guid id)
        {
            return await _dbContext.Material.FindAsync(id);

        }

        public async Task<bool> Update(Guid id, MaterialVM item)
        {
            try
            {
                var mater = await _dbContext.Material.FirstOrDefaultAsync(c => c.Id == id);
                
                mater.MaterialName = item.MaterialName;
                _dbContext.Material.Update(mater);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
