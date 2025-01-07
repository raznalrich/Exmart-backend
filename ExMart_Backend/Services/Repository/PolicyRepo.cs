using ExMart_Backend.Data;
using ExMart_Backend.Model;
using ExMart_Backend.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExMart_Backend.Services.Repository
{
    public class PolicyRepo : Ipolicy
    {

        private readonly ApplicationDBContext _db;

        public PolicyRepo(ApplicationDBContext db)
        {
            _db = db;
        }
        public async Task<Policy> EditPolicies(int id, Policy updatedPolicy)
        {
            var existingPolicy = await _db.TermsAndConditions.FindAsync(id);

            if (existingPolicy == null)
            {
                return null; 
            }

            existingPolicy.TndCcontent = updatedPolicy.TndCcontent;
            
            _db.TermsAndConditions.Update(existingPolicy);
            await _db.SaveChangesAsync();

            return existingPolicy;
        }

        public async Task<Policy> GetPoliciesById(int id)
        {
            return await _db.TermsAndConditions.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Policy>> GetPolicies()
        {
            return await _db.TermsAndConditions.ToListAsync();
        }
    }
}
