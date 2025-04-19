using Azure.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TheMealDb_Core.Dtos.Account;
using TheMealDb_Core.Interface;
using TheMealDb_Core.Model;
using TheMealDb_Infrastructure.Helper.EmailSettings;

namespace TheMealDb_Infrastructure.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;
        private readonly RoleManager<IdentityRole<int>> roleManager;

        public AuthRepository(UserManager<User> userManager,SignInManager<User> signInManager, IHttpContextAccessor httpContextAccessor,IConfiguration configuration, RoleManager<IdentityRole<int>> roleManager )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
            this.roleManager = roleManager;
        }
        public async Task<string> RegisterAsync(User user, string password)
        {
            var result = await userManager.CreateAsync(user,password);
            if (result.Succeeded)
            {
                if (await roleManager.RoleExistsAsync("User"))
                {
                    await userManager.AddToRoleAsync(user,"User");

                }
                // إنشاء رمز تأكيد البريد الإلكتروني
                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
               
                var scheme = httpContextAccessor.HttpContext.Request.Scheme;
                var host = httpContextAccessor.HttpContext.Request.Host;
               

                // إنشاء رابط تأكيد البريد الإلكتروني
                var confirmEmail = $"{scheme}://{host}/api/account/confirmemail?userId={user.Id}&token={token}";

                // إعداد بيانات البريد الإلكتروني
                var email = new Email
                {
                    Subject = "Confirm Email",
                    Recivers = user.Email,
                    Body = $"Please confirm your email by clicking this link: {confirmEmail}"
                };

                // إرسال البريد الإلكتروني
                EmailSettings.SendEmail(email);
                return "User is registered and confirmation email sent";

            }
            var errorMassage= result.Errors.Select(error=>error.Description).ToList();
            return string.Join(", ", errorMassage);

        }
        public async Task<string> ConfirmEmailAsync(string userId, string token)
        {
           
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return "User not found.";
            }

            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return "Email confirmed successfully, you can now login.";
            }

            return "Error confirming email.";
        }

        public async Task<string> LoginAsync(string UserName, string password)
        {
            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                return "UserName or Password is wrong";
            }

            var result = await signInManager.PasswordSignInAsync(user,password,false,false);
            if (!result.Succeeded)
            {

                return "UserName or Password is wrong";

            }

            return GenerateToken(user);
        }

        private string GenerateToken(User user)
        {
            var Claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:key"]));
            var cred = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                configuration["JWT:Issure"],
                configuration["JWT:Audience"],
                Claims,
                signingCredentials: cred,
                expires: DateTime.Now.AddMinutes(30)


            );
            return new JwtSecurityTokenHandler().WriteToken(token);
            
        }

        public async Task<string> ChangePassword(int UserId, string oldPassword, string newPassword)
        {
            var user = await userManager.FindByIdAsync(UserId.ToString());
            var currentPassword = await userManager.CheckPasswordAsync(user,oldPassword);
            if (!currentPassword)
            {
                return "the old password is wrong!!";
            }

            var result = await userManager.ChangePasswordAsync(user,oldPassword,newPassword);
            if (!result.Succeeded)
            {
                return "you have wrong!!";
            }
            return "Change password is done";
            
        }

        public async Task<string> CreateRole(string role)
        {
            if (await roleManager.RoleExistsAsync(role))
            {
                return "the rule is found";
            }

            IdentityRole<int> r = new IdentityRole<int>
            {
                Name = role
            };

            var rolecreated = await roleManager.CreateAsync(r);
            if (rolecreated.Succeeded)
            {
                return $"the role {r.Name} is Created";

            }
            return "role not Created";

           
        }

        public async Task<string> EditRole( EditRoleDto dto)
        {
            var user = await userManager.FindByIdAsync(dto.UserId.ToString());
            if (user == null)
            {
                return "user not found";
            }
            if (! await roleManager.RoleExistsAsync(dto.RoleName))
            {
                return "Role is not found";

            }
            var currentRole = await userManager.GetRolesAsync(user);
            if (currentRole[0] == dto.RoleName)
            {
                return "The role you entered for the user is the same as his previous role";
            }
            await userManager.RemoveFromRolesAsync(user,currentRole);
             await userManager.AddToRoleAsync(user,dto.RoleName);

            return "Successfully";
        }
    }
}
