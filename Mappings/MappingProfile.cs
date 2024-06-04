using AutoMapper;
using BusinessControlApp.Models.DB;
using BusinessControlApp.Models;

namespace BusinessControlApp.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeo para Usuario
            CreateMap<BusinessControlApp.Models.DB.User, BusinessControlApp.Models.UserViewModel>()
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => src.UserType));

            // Mapeo para Tipo
            CreateMap<BusinessControlApp.Models.DB.UserType, BusinessControlApp.Models.UserTypeViewModel>();

            // Mapeo para Negocio
            CreateMap<BusinessControlApp.Models.DB.Business, BusinessControlApp.Models.BusinessViewModel>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

            // Mapeo para Categoria
            CreateMap<BusinessControlApp.Models.DB.Category, BusinessControlApp.Models.CategoryViewModel>();

            // Mapero para Menu
            CreateMap<BusinessControlApp.Models.DB.MenuItem, BusinessControlApp.Models.MenuItemViewModel>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Business, opt => opt.MapFrom(src => src.Business));

        }
    }
}
