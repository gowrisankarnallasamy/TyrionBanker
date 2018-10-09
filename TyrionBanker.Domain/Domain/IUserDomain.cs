using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TyrionBanker.Domain.Models;

namespace TyrionBanker.Domain.Domain
{
    internal interface IUserDomain
    {
        UserInfoDomain GetUserInfo(string userName);
        IList<string> GetRoles(int userId);
        IList<string> GetRoles(string userName);
        IList<FunctionServiceModel> GetRoleFunctions(int userId);
    }
}
