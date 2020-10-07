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
  [RoutePrefix("transactions")]

  public class TransactionController : BaseApiController
  {
    private readonly ILogicService _logicService;
    public TransactionController(ILogicService logicService)
    {
      _logicService = logicService;
    }

    [HttpGet]
    [Authorize]
    public IEnumerable<Transaction> Get()
    {
      return _db.Transactions.ToList();
    }

    [HttpGet]
    [Route("status/{state}")]
    public IHttpActionResult Status(string state)
    {
      try
      {
        var transactionState = _logicService.stringToEnum<ProcessType>(state);
        return Ok(_db.Transactions.Where(i => i.TransactionType == transactionState).ToList());
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
      var transaction = _db.Transactions.Find(id);
      if (transaction != null)
      {
        return Ok(transaction);
      }
      return NotFound();
    }
  }
}
