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
            var file = $"{guid}.jpg";
            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot\\images\\Receipts",
                file);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await pdfFile.CopyToAsync(stream);
            }

            return $"~/images/Properties/{file}";
        }
    }
}
