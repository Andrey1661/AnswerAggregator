using AnswerAggregator.Domain.Entities;
using AutoMapper;
using BL.DTO;

namespace BL.Infrastructure
{
    public class AutoMapperBlModule : Profile
    {
        public AutoMapperBlModule()
        {
            CreateMappings();
        }

        private void CreateMappings()
        {
            CreateMap<UserProfile, UserLoginData>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Identity.Role));

            CreateMap<UserProfile, AuthorModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Fio, opt => opt.MapFrom(src => string.Concat(src.Surname, " ", src.Name, " ", src.Patronymic)))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Identity.Role))
                .ForMember(dest => dest.Avatar, opt => opt.Ignore());

            CreateMap<UserModel, UserProfile>()
                .ForMember(dest => dest.Identity, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UserProfile, UserModel>();

            CreateMap<Post, PostDto>();
            CreateMap<PostDto, Post>();

            CreateMap<SubjectIdentity, Subject>();
            CreateMap<Subject, SubjectIdentity>();

            CreateMap<UserProfile, ProfileDto>()
                .ForMember(dest => dest.UserName, t => t.MapFrom(src => src.Login))
                .ForMember(dest => dest.AccountVerified, t => t.MapFrom(src => src.Identity.AccountVerified))
                .ForMember(dest => dest.Group, t => t.MapFrom(src => src.Group.Name));
        }
    }
}
