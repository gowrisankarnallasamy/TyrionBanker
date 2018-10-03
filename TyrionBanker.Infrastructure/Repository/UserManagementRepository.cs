using AutoMapper;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var mapperConfig = new MapperConfiguration(cfg => {
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
            var roles = SqlConnection.Connection.Query<string>("SELECT R.Name FROM UserRole UR, Roles R WHERE @userId = UR.UserId AND UR.RoleId = R.RoleId", new { userId });
            return (IList<string>)roles;
        }

        public UserInfoDomain GetUserInfo(string username)
        {
            var userInfo = SqlConnection.Connection.QuerySingleOrDefault<UserInfo>("SELECT * FROM UserInfo UI WHERE UI.Name = @username", new { username });
            var mapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<UserInfo, UserInfoDomain>();
            });
            var mapper = mapperConfig.CreateMapper();
            var userInfoDomain = mapper.Map<UserInfoDomain>(userInfo);

            return userInfoDomain;
        }
    }
}
