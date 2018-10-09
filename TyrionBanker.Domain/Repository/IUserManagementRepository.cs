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
        IEnumerable<UserInfoDomain> GetUserInfo();
        UserInfoDomain GetUserInfo(string username);
        IList<FunctionServiceModel> GetRoleFunctions(int userId);
        IList<string> GetRoles(int userId);
        IList<string> GetRoles(string userName);
        void SaveUserInfo(UserInfoDomain userInfoDomain);
        void DeleteUserInfo(string username);
        IEnumerable<RoleDomain> GetRoles();
        RoleDomain GetRole(string name);
        RoleDomain GetRole(int id);
        void SaveRole(RoleDomain roleDomain);
        void DeleteRole(int id);
        void DeleteRole(string name);
        void DeleteRole(RoleDomain roleDomain);
    }
}
