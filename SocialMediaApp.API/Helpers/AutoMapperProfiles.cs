using AutoMapper;
using SocialMediaApp.API.Dtos;
using SocialMediaApp.API.Models;

namespace SocialMediaApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>();
            CreateMap<User, UserForDetailedDto>();
        }
    }
}