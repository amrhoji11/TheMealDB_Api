using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMealDb_Core.Dtos.Account;
using TheMealDb_Core.Model;

namespace TheMealDb_Core.Interface
{
    public interface IAuthRepository
    {
        Task<string> RegisterAsync(User user, string password);
        Task<string> ConfirmEmailAsync(string userId, string Emailtoken);
        Task<string> LoginAsync(string UserName , string password);
        Task<string> ChangePassword(int UserId, string oldPassword , string newPassword);
        Task<string> CreateRole(string role);
        Task<string> EditRole(EditRoleDto dto);

    }
}
