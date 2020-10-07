using AutoMapper;
using koinfast.Models;
using koinfast.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace koinfast.App_Start
{
  public class MapperProfile : Profile
  {
    public MapperProfile()
    {
      CreateMap<Investor, RegisterDto>();
      CreateMap<RegisterDto, Investor>();
    }
  }
}