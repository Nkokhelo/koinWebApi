using AutoMapper;
using koinfast.App_Start;
using koinfast.Models;
using koinfast.Models.Dto;

namespace koinfast
{
  public class MapperConfig
  {
    public static MapperConfiguration Config()
    {
      return new MapperConfiguration(cfg =>
      {
        cfg.AddProfile<MapperProfile>();
      });
    }
  }
}