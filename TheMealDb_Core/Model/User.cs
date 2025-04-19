using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMealDb_Core.Model
{
    public class User : IdentityUser<int>
    {
        public string Address { get; set; }
    }
}
