using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TyrionBanker.Domain.Repository;
using Unity.Attributes;
using Unity;
using TyrionBanker.Domain;

namespace TyrionBanker.Infrastructure.Test
{
    [TestClass]
    public class UserManagementTest
    {
        internal static IUserManagementRepository UserRepo { get; set; }

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
            UserRepo = container.Resolve<IUserManagementRepository>();
        }

        [TestMethod]
        public void TestGetUser()
        {
            var userRepo = UserRepo.GetUserInfo("accholder1");
            Assert.AreNotEqual(userRepo, null);
            Assert.AreEqual(userRepo.Name, "accholder1");
        }

        [TestMethod]
        public void TestGetRoles()
        {
            var roles = UserRepo.GetRoles(1);
            Assert.AreNotEqual(roles, null);
            Assert.IsTrue(roles.Count > 0);
        }

        [TestMethod]
        public void TestGetRoleFunctions()
        {
            var roles = UserRepo.GetRoleFunctions(1);
            Assert.AreNotEqual(roles, null);
            Assert.IsTrue(roles.Count > 0);
        }
    }
}
