using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameShop.ViewModels.Catalog.UserImages
{
    public class UserImageCreateRequest
    {
        public IFormFile ImageFile { get; set; }
    }
}