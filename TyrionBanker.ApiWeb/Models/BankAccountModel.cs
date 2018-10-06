using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TyrionBanker.ApiWeb.Models
{
    public class BankAccountModel
    {
        public string AccountNo { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
    }
}