using ExMart_Backend.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
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
        _imageUpload = imageUpload;
    }

    [HttpPost("upload-image")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadImage([FromForm] ImageUploadRequest model)
    {
        try
        {
            var imageUrl = await _imageUpload.UploadImageAsync(
               model.File,
               Request.Scheme,
               Request.Host.Value
            );
            return Ok(new { imageUrl });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
