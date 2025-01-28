using ExMart_Backend.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using YourNamespace.Repositories;

[Route("api/[controller]")]
[ApiController]
public class ImageUploadController : ControllerBase
{
    public class ImageUploadRequest
    {
        public IFormFile File { get; set; }
    }

    private readonly IImageUpload _imageUpload;

    public ImageUploadController(IImageUpload imageUpload)
    {
        _imageUpload = imageUpload ?? throw new ArgumentNullException(nameof(imageUpload), "Image upload service cannot be null.");
    }

    [HttpPost("upload-image")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadImage([FromForm] ImageUploadRequest model)
    {
        if (model?.File == null)
        {
            return BadRequest("No file was provided for upload.");
        }

        try
        {
            var imageUrl = await _imageUpload.UploadImageAsync(model.File, Request.Scheme, Request.Host.Value);
            return Ok(new { message = "Image uploaded successfully to ImageKit", imageUrl });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error occurred while uploading to ImageKit.", details = ex.Message });
        }
    }

}
