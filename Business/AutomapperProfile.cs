using AutoMapper;
using Business.DataTransferObjects;
using Data.Entities;

namespace Business;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<User, UserInfoDto>();

        CreateMap<SignUpDto, User>(MemberList.Source)
            .ForSourceMember(d => d.Password, o => o.DoNotValidate());
    }
}