using ExMart_Backend.Services.Interface;
using Imagekit;
using Imagekit.Models;
using Imagekit.Sdk;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using YourNamespace.Repositories;

namespace ExMart_Backend.Services.Repository
{
    public class ImageUploadRepository : IImageUpload
    {
        private readonly ImagekitClient _imagekit;

        public ImageUploadRepository()
        {
            _imagekit = new ImagekitClient(
                "public_pVMv3+nW/kdzxYm9xvIU8fDI7D8=",
                "private_bomY0ZQi4OBjK6ZW2PZwzbfF9FQ=",
                "https://ik.imagekit.io/qxpwttqf26/"
            );
        }

        public async Task<string> UploadImageAsync(IFormFile file, string requestScheme, string requestHost)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("No file uploaded.");
            }

            try
            {
                // Convert file to byte array
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();

                // Create upload request for ImageKit
                var uploadRequest = new FileCreateRequest
                {
                    file = fileBytes,
                    fileName = file.FileName,
                    folder = "/uploads",  // Specify the folder in ImageKit
                    useUniqueFileName = true,  // Ensures unique filenames
                    isPrivateFile = false  // Set false to allow public access
                };

                // Upload file to ImageKit
                var uploadResponse = _imagekit.Upload(uploadRequest);

                if (uploadResponse == null || string.IsNullOrEmpty(uploadResponse.url))
                {
                    throw new Exception("Failed to upload image to ImageKit.");
                }

                return uploadResponse.url;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while uploading the image to ImageKit.", ex);
            }
        }
    }
}
