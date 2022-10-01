using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace TenMan.Web.Helpers
{
    public interface IIFileHelper
    {
        Task<string> UploadFileAsync(IFormFile pdfFile);
    }
}
