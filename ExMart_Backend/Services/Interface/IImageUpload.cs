using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace YourNamespace.Repositories
{
    public interface IImageUpload
    {
        
        Task<string> UploadImageAsync(IFormFile file, string requestScheme, string requestHost);
    }
}
