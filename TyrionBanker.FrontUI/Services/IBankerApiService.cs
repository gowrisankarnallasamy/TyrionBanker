using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TyrionBanker.Core;
using TyrionBanker.FrontUI.Common;
using TyrionBanker.FrontUI.Models;

namespace TyrionBanker.FrontUI.Services
{
    public interface IBankerApiService : ITyrionBankerBase
    {
        Task<OwinToken> GetBankerTokenAsync(string userName, string password);
        Task<IEnumerable<string>> GetRolesAsync();
    }
}
