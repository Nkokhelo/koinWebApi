using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace koinfast.Models
{
  public class Investment
  {
    [ForeignKey("Deposit")]
    public int Id { get; set; }
    public string InvestmentNo { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public double Amount { get; set; }
    public double Payback { get; set; }
    public double Percentage { get; set; }
    public InvestmentState State { get; set; }
    public virtual Deposit Deposit { get; set; }

  }
}