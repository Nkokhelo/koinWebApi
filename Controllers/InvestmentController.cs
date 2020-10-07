using koinfast.Interfaces;
using koinfast.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace koinfast.Controllers
{
  [RoutePrefix("investments")]

  public class InvestmentController : BaseApiController
  {
    private readonly ILogicService _logicService;
    public InvestmentController(ILogicService logicService)
    {
      _logicService = logicService;
    }

    [HttpGet]
    [Authorize]
    public IEnumerable<Investment> Get()
    {
      var investments = _db.Investments.ToList();
      var hasToBeUpdated = investments.Any(i => i.State != InvestmentState.Invested && i.EndDate > DateTime.Now);
      if (hasToBeUpdated)
      {
        investments.Where(i => i.State != InvestmentState.Invested && i.EndDate > DateTime.Now).ForEach(i =>
        {
          var inv = _db.Investments.Find(i.Id);
          inv.State = InvestmentState.Matured;
          _db.SaveChanges();
        });
      }
      return investments;
    }

    [HttpGet]
    [Route("status/{state}")]
    public IHttpActionResult Status(string state)
    {
      try
      {
        var investmentState = _logicService.stringToEnum<InvestmentState>(state);
        return Ok(_db.Investments.Where(i => i.State == investmentState).ToList());
      }
      catch (Exception e)
      {
        return InternalServerError(e);
      }
    }


    [HttpGet]
    [Route("{id}")]
    public IHttpActionResult Get(int id)
    {
      var investment = _db.Investments.Find(id);
      if (investment != null)
      {
        return Ok(investment);
      }
      return NotFound();
    }


    [HttpGet]
    [Route("widthdraw/{id}")]
    public IHttpActionResult Widthdraw(int id)
    {
      try
      {
        var investment = _db.Investments.Find(id);
        if (investment != null)
        {
          investment.State = InvestmentState.Requested;
          _db.SaveChanges();
        }
        return NotFound();
      }
      catch (Exception e)
      {
        return InternalServerError(e);
      }
    }

    [HttpGet]
    [Route("approve/{id}")]
    public IHttpActionResult Approve(int id)
    {
      try
      {
        var investment = _db.Investments.Find(id);
        if (investment != null)
        {
          var transaction = new Transaction
          {
            Id = investment.Deposit.Investor.Id,
            Amount = investment.Payback,
            TransactionDate = DateTime.Now,
            TransactionType = ProcessType.Widthdraw,
            RefNo = getRefNo()
          };
          investment.State = InvestmentState.Widthdrawn;
          _db.SaveChanges();
        }
        return NotFound();
      }
      catch (Exception e)
      {
        return InternalServerError(e);
      }
    }

    public string getRefNo()
    {
      string refNo = "";
      bool numberExist = false;
      do
      {
        refNo = _logicService.getRandomNo();
        numberExist = _db.Transactions.ToList().Any(i => i.RefNo == refNo);
      } while (numberExist);
      return refNo;
    }
  }
}
