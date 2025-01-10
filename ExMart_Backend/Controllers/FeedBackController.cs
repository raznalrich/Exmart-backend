using System.Text.Json.Serialization;
using ExMart_Backend.DTO;
using ExMart_Backend.Model;
using ExMart_Backend.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ExMart_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedBackController : ControllerBase
    {
        private readonly IFeedBackRepository _repository;

        public FeedBackController(IFeedBackRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedBackDTO>>> GetFeedBacksByUserId()
        {
            try
            {
                var feedbacks = await _repository.GetFeedbacksByUserIdAsync();
                if (feedbacks == null || !feedbacks.Any())
                {
                    return NotFound("No feedbacks found for user with ID");
                }
                return Ok(feedbacks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving feedbacks: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Feedback>> AddFeedback([FromBody] CreateFeedBackDTO createFeedBackDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var feedback = new Feedback
                {
                    UserId = createFeedBackDTO.UserId,
                    ProductName = createFeedBackDTO.ProductName,
                    FeedBack = createFeedBackDTO.FeedBack
                };

                var createdFeedback = await _repository.AddFeedbackAsync(feedback);

                return CreatedAtAction(nameof(AddFeedback), new { id = createdFeedback.FeedBackId }, createdFeedback);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding feedback: {ex.Message}");
            }
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<FeedBackDTO>>> GetAllFeedbacks()
        {
            try
            {
                var feedbacks = await _repository.GetAllFeedbacksAsync();
                if (feedbacks == null || !feedbacks.Any())
                {
                    return NotFound("No feedbacks found.");
                }
                return Ok(feedbacks);
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, $"An error occurred while retrieving all feedbacks: {ex.Message}");
            }
        }
    }
}
