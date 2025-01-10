using ExMart_Backend.Model;
using ExMart_Backend.Services.Interface;
using ExMart_Backend.Services.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ExMart_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly Ipolicy _policyRepo;

        public PolicyController(Ipolicy policyRepo)
        {
            _policyRepo = policyRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Policy>>> GetPolicy()
        {
            try
            {
                var policies = await _policyRepo.GetPolicies();
                return Ok(policies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPolicy(int id)
        {
            try
            {
                var policy = await _policyRepo.GetPoliciesById(id);
                if (policy == null)
                {
                    return NotFound();
                }
                return Ok(policy);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Policy>> EditPolicy(int id, Policy updatedPolicy)
        {
            try
            {
                var updated = await _policyRepo.EditPolicies(id, updatedPolicy);
                if (updated == null)
                {
                    return NotFound();
                }
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

    