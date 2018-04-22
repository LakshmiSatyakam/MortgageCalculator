using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MortgageCalculator.Api.Services;

namespace MortgageCalculator.Api.UnitTests
{
    [TestClass]
    public class MortgageServiceUnitTest
    {
        private MortgageService _mortgageService;

        public MortgageServiceUnitTest()
        {
            _mortgageService = new MortgageService();
        }

        [TestMethod]
        public void MortgageTypes_Test()
        {
            var list = _mortgageService.GetAllMortgages();
            Assert.IsTrue(list.Count > 0);
        }

        [TestMethod]
        public void Mortgage_Calculate_Test1()
        {
            var loanDetails = _mortgageService.CalculateLoanDetails(1000, 10, 5.12);
            Assert.IsTrue(loanDetails.TotalInterest == 279.84);
            Assert.IsTrue(loanDetails.TotalLoan == 1279.84);
        }

        [TestMethod]
        public void Mortgage_Calculate_Test2()
        {
            var loanDetails = _mortgageService.CalculateLoanDetails(1000, 5, 5.39);
            Assert.IsTrue(loanDetails.TotalInterest == 143.03);
            Assert.IsTrue(loanDetails.TotalLoan == 1143.03);
        }

        [TestMethod]
        public void Mortgage_Calculate_Test3()
        {
            var loanDetails = _mortgageService.CalculateLoanDetails(500, 10, 4.99);
            Assert.IsTrue(loanDetails.TotalInterest == 136.10);
            Assert.IsTrue(loanDetails.TotalLoan == 636.10);
        }

        [TestMethod]
        public void Mortgage_Calculate_Test4()
        {
            var loanDetails = _mortgageService.CalculateLoanDetails(2000, 5, 5.19);
            Assert.IsTrue(loanDetails.TotalInterest == 275.01);
            Assert.IsTrue(loanDetails.TotalLoan == 2275.01);
        }
    }
}
