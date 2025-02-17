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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            try
            {
                var categories = await _categoryRepository.GetCategoriesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving categories: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Category>> AddCategory([FromBody] Category category)
        {
            try
            {
                if (category == null || string.IsNullOrEmpty(category.CategoryName) || string.IsNullOrEmpty(category.IconPath))
                {
                    return BadRequest("Category name and icon path are required.");
                }

                var addedCategory = await _categoryRepository.AddCategoryAsync(category);
                return CreatedAtAction(nameof(GetCategories), new { id = addedCategory.Id }, addedCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding the category: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCategory(int id)
        {
            try
            {
                var category = await _categoryRepository.RemoveCategoryAsync(id);
                if (category == null)
                {
                    return NotFound("Category not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while removing the category: {ex.Message}");
            }
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category category)
        {
            try
            {
                if (id != category.Id)
                {
                    return BadRequest("Category ID in route and request body do not match.");
                }

                var updatedCategory = await _categoryRepository.UpdateCategoryAsync(category);

                if (updatedCategory == null)
                {
                    return NotFound("Category not found.");
                }

                // Return the updated category
                return Ok(updatedCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the category: {ex.Message}");
            }
        }

    }
}
