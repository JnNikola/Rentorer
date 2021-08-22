using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Rentor.Models;
using Rentorer.Dto;

namespace Rentorer.App_Start
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<CustomerDto, Customer>();
            Mapper.CreateMap<Customer, CustomerDto>();
        }
    }
}