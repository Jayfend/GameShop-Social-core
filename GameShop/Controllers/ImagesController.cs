using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.IO;

namespace GameShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImagesController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("Name")]
        public ActionResult GetImage(string Name)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            var path = Path.Combine(webRootPath + @"\user-content\", Name);
            if (System.IO.File.Exists(path))
            {
                var content = System.IO.File.ReadAllBytes(path);
                return new FileContentResult(content, "image/jpeg");
            }
            else
            {
                var newpath = Path.Combine(webRootPath + @"\user-content\", "imgnotfound.jpg");
                var content = System.IO.File.ReadAllBytes(newpath);
                return new FileContentResult(content, "image/jpeg");
            }
        }
    }
}