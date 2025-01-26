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

        public async Task<IEnumerable<FeedBackDTO>> GetFeedbacksByUserIdAsync()
        {
            try
            {
                return await _context.Feedbacks
                    .Include(f => f.User)
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
            catch (Exception ex)
            {
               
                Console.WriteLine($"Error in GetFeedbacksByUserIdAsync: {ex.Message}");
                throw; 
            }
        }

        public async Task<Feedback> AddFeedbackAsync(Feedback feedback)
        {
            try
            {
                _context.Feedbacks.Add(feedback);
                await _context.SaveChangesAsync();
                return feedback;
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error in AddFeedbackAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<FeedBackDTO>> GetAllFeedbacksAsync()
        {
            try
            {
                var feedbacks = await _context.Feedbacks
                    .Select(f => new FeedBackDTO
                    {
                        UserId = f.UserId,
                        ProductName = f.ProductName,
                        FeedBack = f.FeedBack
                    })
                    .ToListAsync();

                return feedbacks;
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error in GetAllFeedbacksAsync: {ex.Message}");
                throw; 
            }
        }
    }
}
