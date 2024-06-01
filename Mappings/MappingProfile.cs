using AutoMapper;
using BusinessControlApp.Models.DB;
using BusinessControlApp.Models;

namespace BusinessControlApp.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeo para Rol
            CreateMap<BusinessControlApp.Models.DB.UserType, BusinessControlApp.Models.UserTypeViewModel>();

            // Mapeo para Usuario
            CreateMap<BusinessControlApp.Models.DB.User, BusinessControlApp.Models.UserViewModel>()
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => src.UserType));
        }
    }
}
