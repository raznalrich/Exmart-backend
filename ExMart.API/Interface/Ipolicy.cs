using ExMart_Backend.Model;

namespace ExMart_Backend.Services.Interface
{
    public interface Ipolicy
    {   
        Task<IEnumerable<Policy>> GetPolicies();
        Task<Policy> GetPoliciesById(int policyId);
        Task<Policy> EditPolicies(int policyId, Policy updatedPolicy);
    }
}
