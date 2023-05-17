using AspNetCore.Totp;
using GameShop.Application.Services;
using GameShop.ViewModels.System.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Buffers.Text;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GameShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TOTPController : ControllerBase
    { readonly ITOTPService _totpService;
        public TOTPController(ITOTPService totpService)
        {
            _totpService= totpService;  
        }

        [HttpGet("code")]
        public async Task<IActionResult> GetCode(string userName, string passWord)
        {
            var response = await _totpService.GetCode(userName, passWord);
            return Ok(response);
        }

        [HttpGet("qr-code-image")]
        public async Task<IActionResult> GetQrImage(string userName, string passWord)
        {
            var response = await _totpService.GenerateQrCodeImage(userName, passWord);
            //var newBase64string = response.QrCodeImage.Replace("data:image/png;base64,", "");

            System.IO.MemoryStream ms = new MemoryStream();
            response.Save(ms, ImageFormat.Jpeg);
            byte[] byteImage = ms.ToArray();
            var SigBase64 = Convert.ToBase64String(byteImage);
            byte[] bytes = Convert.FromBase64String(SigBase64);
            // Convert Base64 String to byte[]
            return new FileContentResult(bytes, "image/jpeg");

        }
        [HttpGet("qr-code")]
        public async Task<IActionResult> GetQrUri(string userName, string passWord)
        {
            var response = await _totpService.GenerateQrCodeUri(userName, passWord);
            return Ok(response);

        }

        [HttpPost("validate")]
        public async Task<IActionResult> Validate([FromBody]ValidateOTPDTO req)
        {   if(!ModelState.IsValid)
            {
                 return BadRequest(ModelState);
            }
            var response = await _totpService.Validate(req);
            return Ok(response);
        }
    }
}
