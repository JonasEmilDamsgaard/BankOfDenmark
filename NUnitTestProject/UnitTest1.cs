using System.Linq;
using BankApp.Services;
using Data.EF.DataAccess;
using NUnit.Framework;

namespace NUnitTestProject
{
  public class Tests
  {
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
      // Arrange
      var customerService = new CustomerService(new UnitOfWork(new CustomerContext()));
      int init = customerService.GetAllCustomers.Count();

      // Act
      var costumer = customerService.AddCustomer();

      // Assert
      Assert.AreEqual(init+1, customerService.GetAllCustomers.Count());
    }
  }
}