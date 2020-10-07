using System.Web;

namespace koinfast.Models.Dto
{
  public class DepositDto
  {
    public string RefNo { get; set; }
    public HttpPostedFileBase DepositProf { get; set; }
    public double Amount { get; set; }
    public int PackageId { get; set; }
    public int InvestorId { get; set; }

  }
}