using ExMart_Backend.Data;
using ExMart_Backend.Model;
using ExMart_Backend.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace ExMart_Backend.Services.Repository
{
    public class ConfigRepository : IConfigRepository
    {
        private readonly ApplicationDBContext _db;

        public ConfigRepository(ApplicationDBContext db)
        {
            _db = db;
        }
        public async Task<ColourMaster> GetColorById(int id)
        {
            return await _db.ColourMaster.FindAsync(id);
        }  
        public async Task<SizeMaster> GetSizeById(int id)
        {
            return await _db.SizeMaster.FindAsync(id);
        }
        // Corrected: Changed return type to match the entity and marked the method as async
        public async Task<IEnumerable<ColourMaster>> GetAllColors()
        {
            return await _db.ColourMaster.ToListAsync();
        }

        // Implemented: GetAllSizes method
        public async Task<IEnumerable<SizeMaster>> GetAllSizes()
        {
            return await _db.SizeMaster.ToListAsync();
        }
    }
}
