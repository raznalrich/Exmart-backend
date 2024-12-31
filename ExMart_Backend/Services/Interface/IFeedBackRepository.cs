using ExMart_Backend.DTO;
using ExMart_Backend.Model;

namespace ExMart_Backend.Services.Interface
{
    public interface IFeedBackRepository
    {
        Task<IEnumerable<FeedBackDTO>> GetFeedbacksByUserIdAsync(int userId);
        Task<Feedback> AddFeedbackAsync(Feedback feedback);
    }
}
