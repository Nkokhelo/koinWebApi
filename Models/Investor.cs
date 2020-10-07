using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace koinfast.Models
{
  public class Investor
  {
    [Key]
    public int Id { get; set; }
    public Title Title { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Email { get; set; }
    public string AccNo { get; set; }
    public string Bank { get; set; }
    public string InvestorNo { get; set; }
    public int TotalSponsees { get; set; }
    public bool Disabled { get; set; }

    [ForeignKey("Sponsor")]
    public int? SponsorId { get; set; }
    public virtual Investor Sponsor { get; set; }
    public virtual ICollection<Investor> Sponsees { get; set; }
    public virtual ICollection<Deposit> Deposits { get; set; }
    public virtual ICollection<Transaction> Transactions { get; set; }
  }
}