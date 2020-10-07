namespace koinfast
{
  public enum DepositState { Pending, Approved, Declined, Withdrawn }
  public enum Title { Mr, Mss, Miss, Mrs }
  public enum PackageType { Shares, Investment }

  //Pending -If a send a request to invest
  //Requested -If a send a request to widthdraw
  public enum InvestmentState { Pending, Invested, Matured, Requested, Widthdrawn, Declined }
  public enum ProcessType { Deposit, Widthdraw }

}
