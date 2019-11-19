using System.IO;
using Data.Models;

namespace BankApp
{
  public static class BankManagerFactory
  {
    public static BankManager Create()
    {
      if (File.Exists(@"C:/db.db"))
      {
        return null;
      }
      else
      {
        return new BankManager();
      }
    }
  }
}