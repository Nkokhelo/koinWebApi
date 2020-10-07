using AutoMapper;
using koinfast.Interfaces;
using koinfast.Models;
using koinfast.Models.Dto;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace koinfast.Controllers
{
  [RoutePrefix("account")]

  public class AccountController : BaseApiController
  {
    private readonly ITokenService _tokenService;
    private readonly ILogicService _logicService;

    public AccountController(ITokenService tokenService, ILogicService logicService)
    {
      _logicService = logicService;
      _tokenService = tokenService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IHttpActionResult> Register(RegisterDto model)
    {
      if (!ModelState.IsValid) return BadRequest(ModelState);

      using (ApplicationUserManager _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_db)))
      {
        var user = new ApplicationUser() { Email = model.Email, UserName = model.Email, };
        var userResult = await _userManager.CreateAsync(user, model.Password);
        if (!userResult.Succeeded)
        {
          AddErrors(userResult);
          return customBadRequest(ModelState);
        }

        var roleResult = await _userManager.AddToRoleAsync(user.Id, userRoles.Investor);
        if (!roleResult.Succeeded)
        {
          AddErrors(roleResult);
          return customBadRequest(ModelState);
        }

        var investor = _mapper.Map<Investor>(model);
        investor.TotalSponsees = 0;

        bool numberExist = false;
        do
        {
          investor.InvestorNo = _logicService.getRandomNo();
          numberExist = _db.Investors.ToList().Any(i => i.InvestorNo == investor.InvestorNo);
        } while (numberExist);

        var sponsor = await _db.Investors.FindAsync(model.SponsorId);
        if (sponsor != null)
        {
          sponsor.TotalSponsees += 1;
          _db.Investors.Add(investor);
          await _db.SaveChangesAsync();
        }

        //remove this code once done
        investor.SponsorId = null;
        _db.Investors.Add(investor);
        await _db.SaveChangesAsync();


        var userDto = new UserDto()
        {
          userName = user.UserName,
          token = _tokenService.CreateToken(user.UserName, userRoles.Investor)
        };
        return Ok(userDto);
      }
    }

    [HttpPost]
    [Route("login")]
    public async Task<IHttpActionResult> Login(LoginDto model)
    {
      if (ModelState.IsValid)
      {
        using (var _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_db)))
        {
          var user = await _db.Users.SingleOrDefaultAsync(u => u.Email == model.UserName);
          if (user == null) return BadRequest("Incorrect username or password");

          var result = await _userManager.CheckPasswordAsync(user, model.Password);
          if (result == false) return BadRequest("Incorrect username or password");

          var userDto = new UserDto()
          {
            userName = model.UserName,
            token = _tokenService.CreateToken(model.UserName, userRoles.Investor)
          };
          return Ok(userDto);

        }
      }
      return BadRequest(ModelState);
    }

    private void AddErrors(IdentityResult result)
    {
      foreach (var error in result.Errors)
      {
        ModelState.AddModelError("", error);
      }
    }

    private IHttpActionResult customBadRequest(ModelStateDictionary modelState)
    {
      if (modelState == null) return BadRequest();
      return BadRequest(modelState);
    }
  }
}
