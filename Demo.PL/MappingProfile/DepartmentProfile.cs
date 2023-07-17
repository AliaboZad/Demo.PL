using AutoMapper;
using Demo.DAL.Models;
using Demo.PL.ViewsModels;

namespace Demo.PL.MappingProfile
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentViewmodel>().ReverseMap();
        }
    }

}
