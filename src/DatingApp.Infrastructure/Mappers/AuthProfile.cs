namespace DatingApp.Infrastructure.Profiles
{
    using DatingApp.Infrastructure.DTOs.AuthDTOs;

    public class AuthProfile : AutoMapper.Profile
    {
        public AuthProfile()
        {
            CreateMap<string, LoginUserDTO>()
                .ForMember(dest => dest.Token, opts => opts.MapFrom(src => src));
        }
    }
}
