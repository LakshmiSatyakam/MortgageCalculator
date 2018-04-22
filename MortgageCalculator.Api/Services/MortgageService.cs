using System;
using System.Collections.Generic;
using MortgageCalculator.Api.Repos;
using MortgageCalculator.Dto;

namespace MortgageCalculator.Api.Services
{
    public interface IMortgageService
    {
        List<Mortgage> GetAllMortgages();

        LoanDetails CalculateLoanDetails(double loanAmount, int duration, double interestRate);
    }

    public class MortgageService : IMortgageService
    {

        private readonly IMortgageRepo _mortgageRepo;
        public MortgageService() : this(new MortgageRepo())
        { }

        public MortgageService(IMortgageRepo mortgageRepo)
        {
            this._mortgageRepo = mortgageRepo;
        }

        public List<Mortgage> GetAllMortgages()
        {
            return _mortgageRepo.GetAllMortgages();
        }

        public LoanDetails CalculateLoanDetails(double loanAmount, int duration, double interestRate)
        {
            double monthlyInterestRate = interestRate / (12 * 100);

            double denominator = Math.Pow((1 + monthlyInterestRate), -duration * 12);

            double emi = (double)(monthlyInterestRate * loanAmount) / (1 - denominator);

            LoanDetails loanDetails = new LoanDetails();
            loanDetails.TotalLoan = Math.Round(emi * duration * 12, 2);
            loanDetails.TotalInterest = Math.Round(loanDetails.TotalLoan - loanAmount, 2);

            return loanDetails;
        }
    }
}