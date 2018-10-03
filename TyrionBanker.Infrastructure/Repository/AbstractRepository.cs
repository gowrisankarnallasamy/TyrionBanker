using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TyrionBanker.Domain.Database;
using TyrionBanker.Infrastructure.Database;
using Unity.Attributes;

namespace TyrionBanker.Infrastructure.Repository
{
    internal class AbstractRepository
    {
        [Dependency("TyrionBankerDB")]
        public ITyrionBankerDbConnection SqlConnection { get; set; }
    }
}
