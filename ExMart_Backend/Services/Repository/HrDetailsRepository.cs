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

        public async Task<HrDetails> EditDetailsRepo(int HRid, HrDetails hrDetails)
        {
            var existingDetail = await _db.Hrdetailing.FirstOrDefaultAsync(u => u.Id == HRid);
            if (existingDetail != null)
            {
                existingDetail.Id = HRid;
                existingDetail.HrPhoneNumber = hrDetails.HrPhoneNumber;
                existingDetail.HrEmail = hrDetails.HrEmail;
                existingDetail.HrAddress = hrDetails.HrAddress;
                existingDetail.ProTagLine = hrDetails.ProTagLine;
                existingDetail.HrChatEmail = hrDetails.HrChatEmail;

                await _db.SaveChangesAsync();
                return existingDetail;
            }
            return null;
        }
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
