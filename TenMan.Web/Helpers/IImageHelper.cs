using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace TenMan.Web.Helpers
{
    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile imageFile);
    }
}
