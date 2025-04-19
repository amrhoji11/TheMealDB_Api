using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMealDb_Core.Dtos.Account
{
    public class EditRoleDto
    {
        public int UserId { get; set; }
        public string RoleName { get; set; }
    }
}
