using ExMart_Backend.Services.Interface;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using YourNamespace.Repositories;

namespace ExMart_Backend.Services.Repository
{
    public class ImageUploadRepository : IImageUpload
    {
        private readonly IWebHostEnvironment _env;

        public ImageUploadRepository(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task<string> UploadImageAsync(IFormFile file, string requestScheme, string requestHost)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("No file uploaded.");

            // Ensure that the "uploads" directory exists within wwwroot.
            var uploadsPath = Path.Combine(_env.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadsPath);

            // Generate a unique filename to avoid collisions.
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsPath, fileName);

            // Save the file locally.
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Construct the publicly accessible URL.
            var imageUrl = $"{requestScheme}://{requestHost}/uploads/{fileName}";

            return imageUrl;
        }
    }

    public class FileUploadOperation : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Look for endpoints with IFormFile or IFormFileCollection
            var fileParams = context.MethodInfo.GetParameters()
                .Where(p => p.ParameterType == typeof(IFormFile)
                         || p.ParameterType == typeof(IFormFileCollection))
                .ToList();

            // If no file parameters are found, do nothing
            if (!fileParams.Any())
                return;

            // You may already have a RequestBody in some cases
            // We'll override/replace or create if it doesn't exist
            operation.RequestBody = new OpenApiRequestBody
            {
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["multipart/form-data"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "object",
                            Properties = fileParams.ToDictionary(
                                p => p.Name,
                                p => new OpenApiSchema
                                {
                                    Type = "string",
                                    Format = "binary"
                                }
                            ),
                            // Mark them as required
                            Required = fileParams.Select(p => p.Name).ToHashSet()
                        }
                    }
                }
            };
        }
    }
}