namespace koinfast.Interfaces
{
  public interface ITokenService
  {
    string CreateToken(string username, string rolename);
  }
}