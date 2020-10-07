using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace koinfast.Models
{
  public class Deposit
  {
    [Key]
    public int Id { get; set; }
    public string RefNo { get; set; }
    public string ProofUrl { get; set; }
    public double Amount { get; set; }
    public DateTime DepositDate { get; set; }
    public DateTime ApprovalDate { get; set; }
    public int InvestorId { get; set; }
    public virtual Investor Investor { get; set; }
    public DepositState State { get; set; }
    public int PackageId { get; set; }
    public virtual Package Package { get; set; }
    public virtual Investment Investment { get; set; }
  }
}