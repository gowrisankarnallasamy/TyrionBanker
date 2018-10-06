using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TyrionBanker.Domain.Models;

namespace TyrionBanker.Domain.Repository
{
    interface IBankerRepository
    {
        IEnumerable<BankAccountDomainModel> GetBankAccountDetails();
        IEnumerable<BankAccountDomainModel> GetBankAccountDetailsByUserName(string userName);
    }
}
