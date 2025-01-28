using ExMart_Backend.Model;

namespace ExMart_Backend.Services.Interface
{
    public interface IHrDetails
    {
        //Task<IEnumerable<HrDetails>>GetDetailsRepo(int HrDid);
        Task<HrDetails> GetDetailsRepo(int HrDid);
        Task<HrDetails> EditDetailsRepo(int HRid, HrDetails hrDetails);
    }
}
