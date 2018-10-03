using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TyrionBanker.Domain.Domain;
using TyrionBanker.Domain.Models;
using Unity.Attributes;

namespace TyrionBanker.Domain.ApplicationService
{
    internal class UserManagementApplicationService : AbstractApplicationService, IUserManagementApplicationService
    {
        [Dependency]
        internal IUserDomain UserDomain { get; set; }

        public IList<FunctionServiceModel> GetRoleFunctions(int userId)
        {
            return UserDomain.GetRoleFunctions(userId);
        }

        public IList<string> GetRoles(int userId)
        {
            return UserDomain.GetRoles(userId);
        }

        public UserInfoDomain GetUserInfo(string userName)
        {
            return UserDomain.GetUserInfo(userName);
        }
    }
}
