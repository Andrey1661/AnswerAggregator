using AnswerAggregator.Domain.Entities;
using AutoMapper;
using BL.DTO;

namespace BL.Infrastructure
{
    public class AutoMapperInitializationModule : Profile
    {
        public AutoMapperInitializationModule()
        {
            CreateMappings();
        }

        private void CreateMappings()
        {
            CreateMap<UserProfile, UserLoginData>();

            CreateMap<UserDTO, UserProfile>()
                .ForMember(dest => dest.Identity, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UserProfile, UserDTO>();

            CreateMap<Post, PostDTO>();
            CreateMap<PostDTO, Post>();

            CreateMap<SubjectDTO, Subject>();
            CreateMap<Subject, SubjectDTO>();

            CreateMap<UserProfile, ProfileDTO>()
                .ForMember(dest => dest.UserName, t => t.MapFrom(src => src.Login))
                .ForMember(dest => dest.AccountVerified, t => t.MapFrom(src => src.Identity.AccountVerified));
        }
    }
}
