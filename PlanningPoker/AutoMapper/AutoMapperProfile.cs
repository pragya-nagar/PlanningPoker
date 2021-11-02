using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlanningPoker.Domain.Entities;
using PlanningPoker.DataContract.Request;
using PlanningPoker.DataContract.Response;

namespace PlanningPoker.WebApi.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<GameInsertRequest, Game>();
            CreateMap<UserInsertRequest, User>();
            CreateMap<User, RegUserResponse>();
        }
    }
}
