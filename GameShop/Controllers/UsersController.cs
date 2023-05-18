using GameShop.Application;
using GameShop.Application.System.Users;
using GameShop.ViewModels.Catalog.UserImages;
using GameShop.ViewModels.System.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Transactions;

namespace GameShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        
        public UsersController(IUserService userService)
        {
            _userService = userService;
            
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.Authenticate(request);
            if (result.ResultObj == null)
            {
                return Unauthorized(result);
            }

            return Ok(result);
        }
        [HttpPost("admin-authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> AdminAuthenticate([FromBody] AdminLoginRequest request)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.AdminAuthenticate(request);
            if (result.ResultObj == null)
            {
                return Unauthorized(result);
            }

            return Ok(result);
        }

        [HttpPost("changepassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] PasswordUpdateRequest request)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.ChangePassword(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("forgotpassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.ForgotPassword(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.Register(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("adminregister")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AdminRegister([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.AdminRegister(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
                var result = await _userService.UpdateUser(request);
                if (!result.IsSuccess)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            
        }

        [HttpPut("{id}/roles")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> RoleAssign(Guid Id, [FromBody] RoleAssignRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
                var result = await _userService.RoleAssign(Id, request);
                if (!result.IsSuccess)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetUserPagingRequest request)
        {
            var games = await _userService.GetUsersPaging(request);
            return Ok(games);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var games = await _userService.GetById(id);
            return Ok(games);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
           
                var result = await _userService.Delete(id);
                return Ok(result);
            
        }

        [Authorize]
        [HttpPost("Avatar/{UserID}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddAvatar(string UserID, [FromForm] UserImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
                var result = await _userService.AddAvatar(UserID, request);
                if (!result.IsSuccess)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            
        }

        [Authorize]
        [HttpPost("Thumbnail/{UserID}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddThumbnail(string UserID, [FromForm] UserImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
                var result = await _userService.AddThumbnail(UserID, request);
                if (!result.IsSuccess)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            
        }

        [AllowAnonymous]
        [HttpPost("Confirm")]
        public async Task<IActionResult> ConfirmAccount(ConfirmAccountRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
                var result = await _userService.ConfirmAccount(request);
                if (!result.IsSuccess)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            
        }

        [AllowAnonymous]
        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail(SendEmailRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.SendEmail(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}