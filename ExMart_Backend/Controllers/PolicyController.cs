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
            var policies = await _policyRepo.GetPolicies();
            return Ok(policies);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPolicy(int id)
        {
            var policies = await _policyRepo.GetPoliciesById(id);
            return Ok(policies);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Policy>> EditPolicy(int id, Policy updatedPolicy)
        {
            var updated = await _policyRepo.EditPolicies(id, updatedPolicy);

            if (updated == null)
            {
                return NotFound(); 
            }

            return Ok(updated); 
        }
    }
}
    