using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace koinfast.Models
{
  public class Transaction
  {
      
    [Key]
    public int Id { get; set; }
    public string RefNo { get; set; }
    public ProcessType TransactionType { get; set; }
    public double Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public virtual Investor Investor { get; set; }
  }
}
