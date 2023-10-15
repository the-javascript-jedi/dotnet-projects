using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        // we need to tell automapper what we want to go from and what we want to go to
        public AutoMapperProfiles()
        {
            // we need to map the photoUrl object key with the first photo from list of photos
            CreateMap<AppUser, MemberDto>()
            .ForMember(dest => dest.PhotoUrl,
            opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url));
            CreateMap<Photo, PhotoDto>();
            // this will map variables and also functions also
            /*
            public int Age { get; set; } will be mapped to GetAge() funcion automatically
            Age should map to the GetAge method
            The Get method name must be same as the variable declared
            */
            CreateMap<MemberUpdateDto, AppUser>();
        }
    }
}