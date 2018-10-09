using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TyrionBanker.Domain.Repository;
using Unity.Attributes;
using Unity;
using TyrionBanker.Domain;
using TyrionBanker.Domain.Models;
using TyrionBanker.Core;
using System.Linq;

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
        public void TestGetUserInos()
        {
            var userInfos = UserRepo.GetUserInfo();
            Assert.AreNotEqual(userInfos, null);
            Assert.IsTrue(userInfos.Any());
        }

        [TestMethod]
        public void TestGetRoles()
        {
            var roles = UserRepo.GetRoles();
            Assert.AreNotEqual(roles, null);
            Assert.IsTrue(roles.Any());
        }

        [TestMethod]
        public void TestGetRole()
        {
            var roles = UserRepo.GetRoles(1);
            Assert.AreNotEqual(roles, null);
            Assert.IsTrue(roles.Count > 0);

            roles = UserRepo.GetRoles("accholder1");
            Assert.AreNotEqual(roles, null);
            Assert.IsTrue(roles.Count > 0);
        }

        [TestMethod]
        public void TestSaveRole()
        {
            var roleDomain = new RoleDomain { Name = "testrole1" };
            UserRepo.SaveRole(roleDomain);
            var insertedRoleDomain = UserRepo.GetRole(roleDomain.Name);
            var insertedRoleDomain2 = UserRepo.GetRole(insertedRoleDomain.RoleId);
            UserRepo.DeleteRole(insertedRoleDomain);

            Assert.IsNotNull(insertedRoleDomain);
            Assert.IsNotNull(insertedRoleDomain2);

            roleDomain = new RoleDomain { Name = "testrole1" };
            UserRepo.SaveRole(roleDomain);
            insertedRoleDomain = UserRepo.GetRole(roleDomain.Name);
            UserRepo.DeleteRole(insertedRoleDomain.RoleId);

            roleDomain = new RoleDomain { Name = "testrole1" };
            UserRepo.SaveRole(roleDomain);
            insertedRoleDomain = UserRepo.GetRole(roleDomain.Name);
            UserRepo.DeleteRole(insertedRoleDomain.Name);
        }

        [TestMethod]
        public void TestGetRoleFunctions()
        {
            var roles = UserRepo.GetRoleFunctions(1);
            Assert.AreNotEqual(roles, null);
            Assert.IsTrue(roles.Count > 0);
        }

        [TestMethod]
        public void TestSaveUserInfo()
        {
            var userInfoDomain = new UserInfoDomain { UserId = 0, Name = "testuser", Password = PassworHelper.HashPassword("testuser"), UserRoles = null };
            UserRepo.SaveUserInfo(userInfoDomain);
            var newUserInfo = UserRepo.GetUserInfo(userInfoDomain.Name);
            UserRepo.DeleteUserInfo(newUserInfo.Name);
        }
    }
}
