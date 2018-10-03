using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TyrionBanker.Core.Unity.Attributes;
using TyrionBanker.Domain.Models;

namespace TyrionBanker.Domain.Repository
{
    [Log]
    interface IUserManagementRepository
    {
        UserInfoDomain GetUserInfo(string username);
        IList<FunctionServiceModel> GetRoleFunctions(int userId);
        IList<string> GetRoles(int userId);
    }
}
