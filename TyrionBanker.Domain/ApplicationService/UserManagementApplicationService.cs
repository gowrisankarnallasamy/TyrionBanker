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

        public IList<FunctionServiceModel> GetRoleFunctions(int userId) => UserDomain.GetRoleFunctions(userId);
        public IList<string> GetRoles(int userId) => UserDomain.GetRoles(userId);
        public IList<string> GetRoles(string userName) => UserDomain.GetRoles(userName);
        public UserInfoDomain GetUserInfo(string userName) => UserDomain.GetUserInfo(userName);
    }
}
