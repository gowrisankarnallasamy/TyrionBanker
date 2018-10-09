using AutoMapper;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TyrionBanker.Core;
using TyrionBanker.Domain.Models;
using TyrionBanker.Domain.Repository;
using TyrionBanker.Infrastructure.Models;

namespace TyrionBanker.Infrastructure.Repository
{
    internal class UserManagementRepository : AbstractRepository, IUserManagementRepository
    {
        public IList<FunctionServiceModel> GetRoleFunctions(int userId)
        {
            var roles = SqlConnection.Connection.Query<Privilage>("SELECT P.* FROM UserRole UR, Roles R, RolePrivilage RP, Privilages P WHERE @userId = UR.UserId AND UR.RoleId = R.RoleId AND R.RoleId = RP.RoleId AND RP.PrivilageId = p.PrivilageId", new { userId });
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Privilage, FunctionServiceModel>()
                .ForMember(src => src.FunctionName, opt => opt.MapFrom(srcm => srcm.Name))
                .ForMember(src => src.Permission, opt => opt.MapFrom(srcm => srcm.Name));
            });
            var mapper = mapperConfig.CreateMapper();
            var functionServiceModels = mapper.Map<IEnumerable<FunctionServiceModel>>(roles);
            return (IList<FunctionServiceModel>)functionServiceModels;
        }

        public IList<string> GetRoles(int userId)
        {
            return GetRolesByUser(userId: userId);
        }

        public IList<string> GetRoles(string userName)
        {
            return GetRolesByUser(userName : userName);
        }

        private IList<string> GetRolesByUser(int userId = default(int), string userName = default(string))
        {
            var roles = SqlConnection.Connection.Query<string>("SELECT R.[Name] FROM UserInfo UI, UserRole UR, Roles R WHERE (UI.UserId = @userid or UI.name = @userName) AND UI.UserId = UR.UserId AND UR.RoleId = R.RoleId", new { userId, userName });
            return (IList<string>)roles;
        }

        public IEnumerable<RoleDomain> GetRoles()
        {
            var roles = SqlConnection.Connection.Query<Role>("SELECT * FROM Roles");
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Role, RoleDomain>());
            var mapper = mapperConfig.CreateMapper();
            var domainRoles = mapper.Map<IEnumerable<RoleDomain>>(roles);
            return domainRoles;
        }

        public RoleDomain GetRole(string name)
        {
            Role role = GetRoleDbModel(name: name);
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Role, RoleDomain>());
            var mapper = mapperConfig.CreateMapper();
            var domainRole = mapper.Map<RoleDomain>(role);
            return domainRole;
        }

        private Role GetRoleDbModel(int id = default(int), string name = default(string))
        {
            return SqlConnection.Connection.QuerySingleOrDefault<Role>("SELECT * FROM Roles WHERE  roleid = @id or name = @name", new { id, name });
        }

        public RoleDomain GetRole(int id)
        {
            var role = GetRoleDbModel(id: id);
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Role, RoleDomain>());
            var mapper = mapperConfig.CreateMapper();
            var domainRole = mapper.Map<RoleDomain>(role);
            return domainRole;
        }

        public void SaveRole(RoleDomain roleDomain)
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<RoleDomain, Role>());
            var mapper = mapperConfig.CreateMapper();
            var roledb = mapper.Map<Role>(roleDomain);
            if (roledb.RoleId == 0)
            {
                SqlConnection.Connection.Insert(roledb);
            }
            else
            {
                SqlConnection.Connection.Update(roledb);
            }
        }

        public void DeleteRole(string name)
        {
            var roledb = GetRoleDbModel(name: name);
            DeleteRoleDb(roledb);
        }

        public void DeleteRole(int id)
        {
            var roledb = GetRoleDbModel(id: id);
            DeleteRoleDb(roledb);
        }

        public void DeleteRole(RoleDomain roleDomain)
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<RoleDomain, Role>());
            var mapper = mapperConfig.CreateMapper();
            var roledb = mapper.Map<Role>(roleDomain);
            DeleteRoleDb(roledb);
        }

        private void DeleteRoleDb(Role roledb)
        {
            var deletedRows = SqlConnection.Connection.Delete(roledb);
            if (deletedRows == 0)
            {
                throw new UnabletoDeleteExceptions($"Unable to delete role {roledb.Name}");
            }
        }

        public IEnumerable<UserInfoDomain> GetUserInfo()
        {
            var userInfo = SqlConnection.Connection.Query<UserInfo>("SELECT UI.* FROM UserInfo UI");
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserInfo, UserInfoDomain>();
            });
            var mapper = mapperConfig.CreateMapper();
            var userInfoDomain = mapper.Map<IEnumerable<UserInfoDomain>>(userInfo);

            return userInfoDomain;
        }

        public UserInfoDomain GetUserInfo(string username)
        {
            UserInfo userInfo = GetUserInfoDb(username);
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserInfo, UserInfoDomain>();
            });
            var mapper = mapperConfig.CreateMapper();
            var userInfoDomain = mapper.Map<UserInfoDomain>(userInfo);

            return userInfoDomain;
        }

        private UserInfo GetUserInfoDb(string username)
        {
            return SqlConnection.Connection.QuerySingleOrDefault<UserInfo>("SELECT * FROM UserInfo UI WHERE UI.Name = @username", new { username });
        }

        public void SaveUserInfo(UserInfoDomain userInfoDomain)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserInfoDomain, UserInfo>();
            });
            var mapper = mapperConfig.CreateMapper();
            var userInfo = mapper.Map<UserInfo>(userInfoDomain);

            if (userInfo.UserId == 0)
            {
                SqlConnection.Connection.Insert(userInfo);
            }
            else
            {
                SqlConnection.Connection.Update(userInfo);
            }
        }

        public void DeleteUserInfo(string username)
        {
            var userInfo = GetUserInfoDb(username);
            var updatedRows = SqlConnection.Connection.Delete(userInfo);
            if (updatedRows == 0)
            {
                throw new UnabletoDeleteExceptions($"Unable to delete user {username}");
            }
        }


    }
}
