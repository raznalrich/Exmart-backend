using ExMart_Backend.Data;
using ExMart_Backend.DTO;
using ExMart_Backend.Model;
using ExMart_Backend.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExMart_Backend.Services.Repository
{
    public class FeedbackRepository : IFeedBackRepository
    {
        private readonly ApplicationDBContext _context;

        public FeedbackRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FeedBackDTO>> GetFeedbacksByUserIdAsync(int userId)
        {
            return await _context.Feedbacks
                .Include(f => f.User)
                .Where(f => f.UserId == userId)
                .Select(f => new FeedBackDTO
                {
                    FeedBackId = f.FeedBackId,
                    UserId = f.UserId,
                    UserName = f.User.Name,
                    ProductName = f.ProductName,
                    FeedBack = f.FeedBack
                })
                .ToListAsync();
        }


        public async Task<Feedback> AddFeedbackAsync(Feedback feedback)
        {
            // Add the feedback to the database
            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            return feedback;
        }
    }
}
