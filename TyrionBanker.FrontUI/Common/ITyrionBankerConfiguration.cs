using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TyrionBanker.Core;

namespace TyrionBanker.FrontUI.Common
{
    public interface ITyrionBankerConfiguration : ITyrionBankerBase
    {
        string SourceId { get; }
        string ApiTyrionBankerBaseUri { get; }
        string ApiTyrionBankerGetOwinToken { get; }
        string ApiTyrionBankerGetRoles { get; }
    }
}
