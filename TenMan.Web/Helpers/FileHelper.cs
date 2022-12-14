using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TenMan.Web.Helpers
{
    public class FileHelper : IIFileHelper
    {
        public async Task<string> UploadFileAsync(IFormFile pdfFile)
        {
            var guid = Guid.NewGuid().ToString();
            var file = $"{guid}.pdf";
            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot\\Receipts",
                file);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await pdfFile.CopyToAsync(stream);
            }

            return $"~/Receipts/{file}";
        }
    }
}
