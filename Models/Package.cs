using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace koinfast.Models
{
  public class Package
  {
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public PackageType PackageType { get; set; }
    public double Return { get; set; }
    public int Duration { get; set; }
    public virtual ICollection<Deposit> Deposits { get; set; }
  }
}