using AutoMapper;
using CoreMentoringApp.Core.Models;
using CoreMentoringApp.WebSite.Areas.Api.Models;
using CoreMentoringApp.WebSite.ViewModels;

namespace CoreMentoringApp.WebSite.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();

            CreateMap<Product, ProductDTO>().ReverseMap();

            CreateMap<Category, CategoryDTO>();
        }
    }
}
