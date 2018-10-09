using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TyrionBanker.Domain.ApplicationService;
using Unity;
using TyrionBanker.Infrastructure;

namespace TyrionBanker.Domain.Test
{
    [TestClass]
    public class UserManagementApplicationServiceTest
    {
        internal static IUserManagementApplicationService UserAppService { get; set; }

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            var container = new UnityContainer();
            DomainUnityContainer.BuildUp(container);
            InfrastructureUnityContainer.BuildUp(container);
            UserAppService = container.Resolve<IUserManagementApplicationService>();
        }

        [TestMethod]
        public void TestGetUser()
        {
            var userRepo = UserAppService.GetUserInfo("accholder1");
            Assert.AreNotEqual(userRepo, null);
            Assert.AreEqual(userRepo.Name, "accholder1");
        }

        [TestMethod]
        public void TestGetRoles()
        {
            var roles = UserAppService.GetRoles(1);
            Assert.AreNotEqual(roles, null);
            Assert.IsTrue(roles.Count > 0);

            roles = UserAppService.GetRoles("accholder1");
            Assert.AreNotEqual(roles, null);
            Assert.IsTrue(roles.Count > 0);
        }

        [TestMethod]
        public void TestGetRoleFunctions()
        {
            var roles = UserAppService.GetRoleFunctions(1);
            Assert.AreNotEqual(roles, null);
            Assert.IsTrue(roles.Count > 0);
        }
    }
}
