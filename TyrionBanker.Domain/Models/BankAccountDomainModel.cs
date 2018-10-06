using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TyrionBanker.Domain.Models
{
    public class BankAccountDomainModel
    {
        public string AccountNo { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
    }
}
