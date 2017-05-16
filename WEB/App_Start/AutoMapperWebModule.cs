using System.Web;
using AutoMapper;
using BL.DTO;
using WEB.Models.Account;
using WEB.Models.Proflie;

namespace WEB.App_Start
{
    public class AutoMapperWebModule : Profile
    {
        public AutoMapperWebModule()
        {
            CreateMappings();
        }

        private void CreateMappings()
        {
            CreateMap<RegistrationModel, UserModel>();
            CreateMap<ProfileDto, ProfileModel>()
                .ForMember(dest => dest.AvatarLink,
                    opt => opt.MapFrom(src => src.Avatar));
        }
    }
}