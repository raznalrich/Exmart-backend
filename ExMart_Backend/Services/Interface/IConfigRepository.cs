using ExMart_Backend.Model;

namespace ExMart_Backend.Services.Interface
{
    public interface IConfigRepository
    {
        Task<ColourMaster> GetColorById(int id);
        Task<SizeMaster> GetSizeById(int id);
    }
}
