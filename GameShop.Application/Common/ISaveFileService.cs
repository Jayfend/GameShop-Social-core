using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Application.Common
{
    public interface ISaveFileService
    {

        Task<string> SaveFileAsync(IFormFile file);
    }
}
