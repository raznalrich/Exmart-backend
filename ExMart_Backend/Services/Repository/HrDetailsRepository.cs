using ExMart_Backend.Data;
using ExMart_Backend.Model;
using ExMart_Backend.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExMart_Backend.Services.Repository
{
    public class HrDetailsRepository : IHrDetails
    {
        private readonly ApplicationDBContext _db;

        public HrDetailsRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public Task<HrDetails> EditDetailsRepo(HrDetails hrDetails)
        {
            throw new NotImplementedException();
        }

        //public async Task<IEnumerable<HrDetails>> GetDetailsRepo()
        //{
        //    try
        //    {
        //        return await _db.Hrdetailing.ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new InvalidOperationException("An error occurred while retrieving all details.", ex);
        //    }
        //}
        public async Task<HrDetails> GetDetailsRepo(int HrDid)
        {
            try
            {
                return await _db.Hrdetailing.FirstOrDefaultAsync(u => u.Id == HrDid);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving all details.", ex);
            }
        }
    }
}
