using ExMart_Backend.Interface;
using ExMart_Backend.Model;
using Microsoft.AspNetCore.Mvc;

namespace ExMart_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();
            return Ok(categories);
        }

        // POST: api/categories
        [HttpPost]
        public async Task<ActionResult<Category>> AddCategory([FromBody] Category category)
        {
            if (category == null || string.IsNullOrEmpty(category.CategoryName) || string.IsNullOrEmpty(category.IconPath))
            {
                return BadRequest("Category name and icon path are required.");
            }

            var addedCategory = await _categoryRepository.AddCategoryAsync(category);
            return CreatedAtAction(nameof(GetCategories), new { id = addedCategory.Id }, addedCategory);
        }

        // DELETE: api/categories/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCategory(int id)
        {
            var category = await _categoryRepository.RemoveCategoryAsync(id);
            if (category == null)
            {
                return NotFound("Category not found.");
            }

            return NoContent();

        }
    }
}
