using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Owin;

namespace koinfast.App_Start
{
  public partial class Startup
  {

    public void Configuration(IAppBuilder app)
    {
      app.CreatePerOwinContext(ApplicationDbContext.Create);
      app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
      app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);


      app.UseJwtBearerAuthentication(
       new JwtBearerAuthenticationOptions
       {
         AuthenticationMode = AuthenticationMode.Active,
         TokenValidationParameters = new TokenValidationParameters()
         {
           ValidateIssuerSigningKey = true,
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["TokenKey"])),
           ValidateIssuer = false,
           ValidateAudience = false,
         }
       }
      );
    }
  }
}