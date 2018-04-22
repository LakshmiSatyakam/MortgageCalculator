using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MortgageCalculator.Web.Models
{
    public class LoanDetailsM
    {
        public double LoanAmount { get; set; }

        public int Duration { get; set; }

        public string InterestRate { get; set; }

        public double TotalLoan { get; set; }

        public double TotalInterest { get; set; }
    }
}