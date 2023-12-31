﻿using AutoMapper;
using Demo.DAL.Models;
using Demo.PL.ViewsModels;

namespace Demo.PL.MappingProfile
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
        }
    }
}
