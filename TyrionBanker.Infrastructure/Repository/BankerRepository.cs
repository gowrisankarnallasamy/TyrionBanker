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
    class BankerRepository : AbstractRepository, IBankerRepository
    {
        public IEnumerable<BankAccountDomainModel> GetBankAccountDetails()
        {
            var bankAccounts = SqlConnection.Connection.Query<BankAccount>("SELECT * FROM BankAccount");
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BankAccount, BankAccountDomainModel>()
                .ForMember(dest => dest.AccountNo, opt => opt.MapFrom(src => src.BankAccountNo))
                .ForMember(dest => dest.AccountType, opt => opt.MapFrom(src => src.BankAccountType.Name))
                ;
            });
            var domainBankAccounts = mapperConfig.CreateMapper().Map<IEnumerable<BankAccountDomainModel>>(bankAccounts);
            return domainBankAccounts;
        }

        public IEnumerable<BankAccountDomainModel> GetBankAccountDetailsByUserName(string userName)
        {
            var bankAccounts = SqlConnection.Connection.Query<BankAccount>(@"
                SELECT * FROM BankAccount BA
                INNER JOIN UserAccount UA ON BA.BankAccountId = UA.BankAccountId
                INNER JOIN UserInfo UI ON UA.UserId = UI.UserId
                WHERE UI.Name = @userName", new { userName });
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BankAccount, BankAccountDomainModel>()
                .ForMember(dest => dest.AccountNo, opt => opt.MapFrom(src => src.BankAccountNo))
                .ForMember(dest => dest.AccountType, opt => opt.MapFrom(src => src.BankAccountType.Name))
                ;
            });
            var domainBankAccounts = mapperConfig.CreateMapper().Map<IEnumerable<BankAccountDomainModel>>(bankAccounts);
            return domainBankAccounts;
        }
    }
}
