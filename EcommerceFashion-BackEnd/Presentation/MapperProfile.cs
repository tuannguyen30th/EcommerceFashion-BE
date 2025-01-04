using Application.AccountService.Models.RequestModels;
using Application.AccountService.Models.ResponseModels;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Role = Domain.Enums.Role;

namespace Infrastructure.Common
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Account
            CreateMap<AccountSignUpModel, Account>();
            CreateMap<Account, AccountModel>()
                .ForMember(dest => dest.Roles,
                    opt => opt.MapFrom(src =>
                        src.AccountRoles.Select(accountRole => accountRole.Role.Name).Select(Enum.Parse<Role>)))
                .ForMember(dest => dest.RoleNames,
                    opt => opt.MapFrom(src => src.AccountRoles.Select(accountRole => accountRole.Role.Name)));
            CreateMap<Account, AccountLiteModel>();
            CreateMap<AccountUpdateModel, Account>();

            
        }
    }
}
