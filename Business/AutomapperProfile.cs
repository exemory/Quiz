using AutoMapper;
using Business.DataTransferObjects;
using Data.Entities;

namespace Business;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<Answer, AnswerDto>();
        
        CreateMap<Question, QuestionDto>();

        CreateMap<Test, TestDto>();

        CreateMap<SignUpDto, User>(MemberList.Source)
            .ForSourceMember(d => d.Password, o => o.DoNotValidate());
        
        CreateMap<User, UserInfoDto>();
    }
}