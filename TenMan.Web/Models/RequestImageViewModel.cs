using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TenMan.Web.Data.Entities;

namespace TenMan.Web.Models
{
        public class RequestImageViewModel : RequestImage
        {
            [Display(Name = "Image")]
            public IFormFile ImageFile { get; set; }
        }
}
