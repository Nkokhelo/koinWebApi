using System.Web.Http;
using koinfast.Interfaces;
using koinfast.Services;
using Unity;
using Unity.WebApi;

namespace koinfast
{
  public static class UnityConfig
  {
    public static void RegisterComponents()
    {
      var container = new UnityContainer();

      // register all your components with the container here
      // it is NOT necessary to register your controllers

      container.RegisterType<ITokenService, TokenService>();
      container.RegisterType<ILogicService, LogicService>();
      GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
    }
  }
}