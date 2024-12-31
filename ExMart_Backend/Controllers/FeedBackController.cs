using System.Text.Json.Serialization;
using ExMart_Backend.DTO;
using ExMart_Backend.Model;
using ExMart_Backend.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ExMart_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedBackController:ControllerBase
    {
        private readonly IFeedBackRepository _repository;

        public FeedBackController(IFeedBackRepository repository)
        {
            _repository = repository;
        }

        //Get feedbacks by id
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<FeedBackDTO>>> GetFeedBacksByUserId(int userId)
        {
            var feedbacks = await _repository.GetFeedbacksByUserIdAsync(userId);
            if (feedbacks == null || !feedbacks.Any())
            {
                return NotFound($"No feedbacks found for user with ID {userId}");
            }
            return Ok(feedbacks);
        }


        //add feedbacks
        [HttpPost]

        public async Task<ActionResult<Feedback>> AddFeedback([FromBody] CreateFeedBackDTO createFeedBackDTO)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map DTO to Model
            var feedback = new Feedback
            {
                UserId = createFeedBackDTO.UserId,
                ProductName = createFeedBackDTO.ProductName,
                FeedBack = createFeedBackDTO.FeedBack
            };

            // Add feedback to repository (database)
            var createdFeedback = await _repository.AddFeedbackAsync(feedback);

            // Return response
            return CreatedAtAction(nameof(AddFeedback), new { id = createdFeedback.FeedBackId }, createdFeedback);
        }
    }
}

