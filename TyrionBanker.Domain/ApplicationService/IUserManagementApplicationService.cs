using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TyrionBanker.Core.Unity.Attributes;
using TyrionBanker.Domain.Attributes;
using TyrionBanker.Domain.Models;

namespace TyrionBanker.Domain.ApplicationService
{
    [Log]
    [ApplicationServiceTransaction]
    public interface IUserManagementApplicationService
    {
        UserInfoDomain GetUserInfo(string userName);
        IList<string> GetRoles(int userId);
        IList<FunctionServiceModel> GetRoleFunctions(int userId);
    }
}
