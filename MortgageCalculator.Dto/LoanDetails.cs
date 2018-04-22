
namespace MortgageCalculator.Dto
{
    public class LoanDetails
    {
        public double TotalLoan { get; set; }

        public double TotalInterest { get; set; }

        public LoanDetails()
        {

        }
        public LoanDetails(double totalLoan, double totalInterest)
        {
            TotalLoan = totalLoan;
            TotalInterest = totalInterest;
        }
    }
}
