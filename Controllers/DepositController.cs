using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using koinfast.Models;
using koinfast.Models.Dto;
using koinfast;
using koinfast.Interfaces;

namespace koinfast.Controllers
{
  [RoutePrefix("deposits")]

  public class DepositController : BaseApiController
  {
    private readonly ILogicService _logicService;
    public DepositController(ILogicService logicService)
    {
      _logicService = logicService;
    }

    [HttpGet]
    [Authorize]
    public IEnumerable<Deposit> Get()
    {
      return _db.Deposits.ToList();
    }

    [HttpGet]
    [Route("status/{state}")]
    public IHttpActionResult Status(string state)
    {
      try
      {
        var depositState = _logicService.stringToEnum<DepositState>(state);
        return Ok(_db.Deposits.Where(i => i.State == depositState).ToList());
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
      var deposit = _db.Deposits.Find(id);
      if (deposit != null)
      {
        return Ok(deposit);
      }
      return NotFound();
    }

    public IHttpActionResult Deposit(DepositDto model)
    {
      if (!ModelState.IsValid) return BadRequest(ModelState);
      try
      {
        var type = model.DepositProf.ContentType.Split('/')[1];
        var guid = Guid.NewGuid().ToString().Replace("-", "");
        var path = $"/ProofOfPayment/{guid}.{type}";
        var investor = _db.Investors.ToList().Find(i => i.Email == User.Identity.Name);
        var deposit = new Deposit
        {
          RefNo = getRefNo(),
          State = DepositState.Pending,
          ProofUrl = path,
          Amount = model.Amount,
          DepositDate = DateTime.Now,
          ApprovalDate = DateTime.Now,
          PackageId = model.PackageId,
          InvestorId = investor.Id
        };

        _db.Deposits.Add(deposit);
        if (!SaveProofToFolder(model.DepositProf, path))
        {
          return BadRequest(ModelState);
        }
        var transaction = new Transaction
        {
          Id = investor.Id,
          Amount = deposit.Amount,
          TransactionDate = DateTime.Now,
          TransactionType = ProcessType.Deposit,
          RefNo = transGetRefNo()
        };
        _db.Transactions.Add(transaction);
        _db.SaveChanges();
        return Ok();
      }
      catch (Exception e)
      {
        return InternalServerError(e);
      }
    }


    [HttpGet]
    [Route("respond/{id}/{state}")]
    public IHttpActionResult Respond(int id, string state)
    {
      try
      {
        var deposit = _db.Deposits.Find(id);
        if (deposit != null)
        {
          var depositState = _logicService.stringToEnum<DepositState>(state);
          var investment = _logicService.makeInvestment(deposit);
          investment.InvestmentNo = getRefNo();
          deposit.State = depositState;
          _db.Investments.Add(investment);
          _db.SaveChanges();

          return Ok(deposit);
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
        numberExist = _db.Deposits.ToList().Any(d => d.RefNo == refNo);
      } while (numberExist);

      return refNo;
    }
    public string transGetRefNo()
    {
      string refNo = "";
      bool numberExist = false;
      do
      {
        refNo = _logicService.getRandomNo();
        numberExist = _db.Transactions.ToList().Any(d => d.RefNo == refNo);
      } while (numberExist);
      return refNo;
    }

  }
}
