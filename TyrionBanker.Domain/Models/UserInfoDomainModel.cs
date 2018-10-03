using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TyrionBanker.Domain.Models
{
    public partial class UserInfoDomain
    {
        public virtual int UserId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Password { get; set; }
        public virtual IEnumerable<string> UserRoles { get; set; }
    }

}
