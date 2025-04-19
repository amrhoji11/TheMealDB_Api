using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMealDb_Core.Dtos.Account
{
    public class ChangePasswordDto
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}
