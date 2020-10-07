using koinfast.Interfaces;
using koinfast.Services;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Services.Description;

namespace koinfast
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      // Web API configuration and services
      EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");
      config.EnableCors(cors);

      config.SuppressDefaultHostAuthentication();
      config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

      // Web API routes
      config.MapHttpAttributeRoutes();

      config.Routes.MapHttpRoute(
          name: "DefaultApi",
          routeTemplate: "{controller}/{id}",
          defaults: new { id = RouteParameter.Optional }
      );
      config.Formatters.Remove(config.Formatters.XmlFormatter);
    }
  }
}
