using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using koinfast.Models;

namespace koinfast.Interfaces
{
  public interface ILogicService
  {
    string getRandomNo();
    T stringToEnum<T>(string value);

    T intToEnum<T>(int value);

    Investment makeInvestment(Deposit deposit);

  }
}
