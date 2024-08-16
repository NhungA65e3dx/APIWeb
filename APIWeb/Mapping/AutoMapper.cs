using APIWeb.Entities;
using APIWeb.Model;
using AutoMapper;
using System.Web.Providers.Entities;
using User = APIWeb.Entities.User;

namespace APIWeb.Mapping
{
    public class AutoMapper : Profile
    {
        public AutoMapper() 
        {
            CreateMap<Register, User>()
                    .ForMember(dest => dest.id, opt => opt.Ignore())
                    .ForMember(dest => dest.CreateAt, opt => opt.Ignore())
                    .ForMember(dest => dest.DueDate, opt => opt.Ignore());
            CreateMap<Update, User>()
                .ForMember(dest => dest.id, opt => opt.Ignore())
                 .ForMember(dest => dest.UpdateAt, opt => opt.Ignore());
            CreateMap<Login, User>()
                .ForMember(dest => dest.id, opt => opt.Ignore())
                 .ForMember(dest => dest.Role, opt => opt.Ignore());
        }
    }
}
