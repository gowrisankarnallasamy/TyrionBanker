using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TyrionBanker.ApiWeb.Models;
using TyrionBanker.ApiWeb.Results;
using Unity;

namespace TyrionBanker.ApiWeb.Controllers
{
    public class BankAccountController : AbstractController
    {
        public BankAccountController(IUnityContainer unityContainer) : base(unityContainer)
        {
        }

        public Result<IEnumerable<BankAccountModel>> GetBankAccountDetails()
        {
            return new Result<IEnumerable<BankAccountModel>>();
        }
    }
}