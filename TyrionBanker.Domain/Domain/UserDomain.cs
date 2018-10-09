using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TyrionBanker.Domain.Models;
using TyrionBanker.Domain.Repository;
using Unity.Attributes;

namespace TyrionBanker.Domain.Domain
{
    internal class UserDomain : AbstractDomainService, IUserDomain
    {
        [Dependency]
        public IUserManagementRepository repository { get; set; }

        public IList<FunctionServiceModel> GetRoleFunctions(int userId) => repository.GetRoleFunctions(userId);

        public IList<string> GetRoles(int userId) => repository.GetRoles(userId);
        public IList<string> GetRoles(string userName) => repository.GetRoles(userName);

        public UserInfoDomain GetUserInfo(string userName) => repository.GetUserInfo(userName);
    }
}
