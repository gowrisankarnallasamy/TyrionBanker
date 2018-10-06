using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using TyrionBanker.Domain.Repository;
using TyrionBanker.Domain;
using System.Linq;

namespace TyrionBanker.Infrastructure.Test
{
    [TestClass]
    public class BankerRepoTest
    {
        internal static IBankerRepository BankerRepo { get; set; }

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            typeof(InfrastructureUnityContainer)
                .GetField("UnityContainer", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                .SetValue(null, null);
            typeof(DomainUnityContainer)
                .GetField("UnityContainer", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                .SetValue(null, null);

            var container = new UnityContainer();
            DomainUnityContainer.BuildUp(container);
            InfrastructureUnityContainer.BuildUp(container);
            BankerRepo = container.Resolve<IBankerRepository>();
        }

        [TestMethod]
        public void TestGetBankAccountDetails()
        {
            var lstDomainBankAccounts = BankerRepo.GetBankAccountDetails();
            Assert.AreNotEqual(lstDomainBankAccounts, null);
            Assert.IsTrue(lstDomainBankAccounts.Any());
        }

        [TestMethod]
        public void TestGetBankAccountDetailsByUserName()
        {
            var lstDomainBankAccounts = BankerRepo.GetBankAccountDetailsByUserName("accholder1");
            Assert.AreNotEqual(lstDomainBankAccounts, null);
            Assert.IsTrue(lstDomainBankAccounts.Any());

            lstDomainBankAccounts = BankerRepo.GetBankAccountDetailsByUserName("banker1");
            Assert.AreNotEqual(lstDomainBankAccounts, null);
            Assert.IsTrue(lstDomainBankAccounts.Any());
        }
    }
}
