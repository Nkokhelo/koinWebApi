using FluentDateTime;
using koinfast.Interfaces;
using koinfast.Models;
using System;
using System.Linq;

namespace koinfast
{
  public class LogicService : ILogicService
  {
    public string getRandomNo()
    {
      Random r = new Random();
      int myNum = r.Next(10000000, 99999999);
      return myNum.ToString();
    }

    public T stringToEnum<T>(string value)
    {
      return (T)Enum.Parse(typeof(T), value, true);
    }
    public T intToEnum<T>(int value)
    {
      return (T)Enum.ToObject(typeof(T), value);
    }

    public Investment makeInvestment(Deposit deposit)
    {
      var i = new Investment();
      if (deposit != null)
      {
        var is15DaysPackage = (deposit.Package.PackageType == PackageType.Investment && deposit.Package.Duration == 15);
        var is30DaysPackage = (deposit.Package.PackageType == PackageType.Investment && deposit.Package.Duration == 30);
        var endDate = (is15DaysPackage) ? DateTime.Now.AddBusinessDays(15) : (is30DaysPackage) ? DateTime.Now.AddDays(30) : DateTime.Now.AddYears(1);
        var payback = (is15DaysPackage) ? (deposit.Amount + (deposit.Amount * 15 / 100)) : (is30DaysPackage) ? (deposit.Amount + deposit.Amount) : deposit.Package.Return;

        i = new Investment
        {
          Id = deposit.Id,
          Amount = deposit.Amount,
          State = InvestmentState.Invested,
          StartDate = DateTime.Now,
          EndDate = endDate,
          Payback = payback,
          Percentage = (is15DaysPackage) ? 15 : (is30DaysPackage) ? 100 : 0,
        };
      }
      return i;
    }

  }
}