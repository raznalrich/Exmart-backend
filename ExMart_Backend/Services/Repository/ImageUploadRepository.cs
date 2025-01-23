using ExMart_Backend.Services.Interface;
using Microsoft.OpenApi.Models;
using Supabase;
using Swashbuckle.AspNetCore.SwaggerGen;
using YourNamespace.Repositories;

namespace ExMart_Backend.Services.Repository
{
    public class ImageUploadRepository : IImageUpload
    {
        private readonly IWebHostEnvironment _env;
        

        public ImageUploadRepository(IWebHostEnvironment env)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env), "Web host environment cannot be null.");
            var supabaseClient = new Client("your-supabase-url", "your-supabase-anon-key");

        }

        public Task<string> uploadImage()
        {
            throw new NotImplementedException();
        }

        public async Task<string> UploadImageAsync(IFormFile file, string requestScheme, string requestHost)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("No file uploaded.");
            }

            if (string.IsNullOrWhiteSpace(requestScheme) || string.IsNullOrWhiteSpace(requestHost))
            {
                throw new ArgumentException("Invalid request scheme or host.");
            }

            try
            {
                // Ensure that the "uploads" directory exists within wwwroot.
                var uploadsPath = Path.Combine(_env.WebRootPath ?? throw new InvalidOperationException("WebRootPath is not set."), "uploads");
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
            catch (IOException ex)
            {
                throw new InvalidOperationException("An error occurred while saving the file.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An unexpected error occurred while uploading the image.", ex);
            }
        }
        //public async Task<string> uploadImage()
        //{

        //    return "iamdsf";
        //}
    }
}
