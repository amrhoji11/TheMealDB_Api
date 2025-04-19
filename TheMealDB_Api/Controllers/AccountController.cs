using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;
using TheMealDB_Api.Helper;
using TheMealDb_Core.Dtos.Account;
using TheMealDb_Core.Interface;
using TheMealDb_Core.Model;

namespace TheMealDB_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthRepository authRepository;
        private readonly UserManager<User> userManager;

        public AccountController(IAuthRepository authRepository , UserManager<User> userManager)
        {
            this.authRepository = authRepository;
            this.userManager = userManager;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto dto )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var user = new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Address= dto.Address

            };
            var result = await authRepository.RegisterAsync(user,dto.Password);
            return Ok(result);
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string UserId, [FromQuery] string token)
        {
            
           
            
            var res = await authRepository.ConfirmEmailAsync(UserId, token);
            if (res.Contains("successfully"))
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var result = await authRepository.LoginAsync(dto.UserName,dto.password);

            return Ok(result);

        }
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ","");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("token is missing ");

            }
            var userId = ExtractClaims.ExtractUserId(token);
            if (userId is null)
            {
                return Unauthorized("invalid user token");
            }

            var result = await authRepository.ChangePassword(userId.Value,dto.oldPassword,dto.newPassword);
            return Ok(result);


        }
        [Authorize(Roles ="Admin")]
        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(string role)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token)) 
            {
                return Unauthorized("user Unauthorized");
            
            }

            var result = await authRepository.CreateRole(role);
            return Ok(result);

        }
        [Authorize(Roles ="Admin")]
        [HttpPost("EditRole")]
        public async Task<IActionResult> EditRole(EditRoleDto dto)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("user Unauthorized");

            }
            var userId =  ExtractClaims.ExtractUserId(token);
            var result = await authRepository.EditRole(dto);
            return Ok(result);

           

        }
    }
}
