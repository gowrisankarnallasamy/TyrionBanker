using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TyrionBanker.Core;
using TyrionBanker.FrontUI.Common;
using TyrionBanker.FrontUI.WebAPIManager;
using Unity.Attributes;

namespace TyrionBanker.FrontUI.Services
{
    internal abstract class AbstractService : ITyrionBankerBase
    {
        [Dependency]
        public IWebAPIManager webAPIManager { get; set; }
        public AbstractService()
        {

        }
    }
}
