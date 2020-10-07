using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using koinfast.Models;

namespace koinfast.Controllers
{
  [RoutePrefix("investors")]

  public class InvestorsController : BaseApiController
  {

    [HttpGet]
    [Authorize]
    public IEnumerable<Investor> Get()
    {
      return _db.Investors.ToList();
    }
    [HttpGet]
    [Route("deactivated")]
    public IEnumerable<Investor> Disbaled()
    {
      return _db.Investors.Where(i => i.Disabled).ToList();
    }

    [HttpGet]
    [Route("active")]
    public IEnumerable<Investor> Active()
    {
      return _db.Investors.Where(i => !i.Disabled).ToList();
    }

    [HttpGet]
    [Route("{id}/sponsees")]
    public IHttpActionResult Sponsees(int id)
    {
      var investor = _db.Investors.Find(id);
      if (investor != null)
      {
        return Ok(investor.Sponsees.ToList());
      }
      return NotFound();
    }

    [HttpGet]
    [Route("sponsors")]
    public IEnumerable<Investor> Sponsors()
    {
      return _db.Investors.Where(i => i.TotalSponsees > 0).ToList();
    }

    [HttpGet]
    [Route("sponsor/{refNo}")]
    public IHttpActionResult Sponsor(string refNo)
    {
      Investor i = _db.Investors.ToList().Find(s => s.InvestorNo == refNo);
      if (i == null)
      {
        return NotFound();
      }
      return Ok<Investor>(i);
    }

    [HttpGet]
    // GET: api/Investors/5
    public async Task<IHttpActionResult> Get(int id)
    {
      var investor = await _db.Investors.FindAsync(id);
      if (investor != null)
      {
        return Ok(investor);
      }
      return NotFound();
    }

    public async Task<IHttpActionResult> Disable(int id)
    {
      var investor = await _db.Investors.FindAsync(id);
      if (investor == null)
      {
        return NotFound();
      }
      investor.Disabled = true;
      _db.SaveChanges();
      return Ok("Investor is disabled");
    }

  }
}
