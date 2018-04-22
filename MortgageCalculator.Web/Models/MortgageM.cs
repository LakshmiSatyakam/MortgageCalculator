using MortgageCalculator.Dto;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace MortgageCalculator.Web.Models
{
    public class MortgageM
    {
        [RegularExpression(@"^\d+.\d{0,2}$")]
        [Range(1, double.MaxValue)]
        public double LoanAmount { get; set; }

        [RegularExpression(@"^\d+$")]
        [Range(1, int.MaxValue)]
        public int Duration { get; set; }

        public string InterestRate { get; set; }

        public IEnumerable<SelectListItem> MortgageTypes { get; private set; }

        public MortgageM()
        {
        }

        public MortgageM(IList<Mortgage> mortgages)
        {
            IEnumerable<SelectListItem> items = from value in mortgages
                                                select new SelectListItem
                                                {
                                                    Text = value.Name,
                                                    Value = value.InterestRate.ToString(),
                                                };
            items.ToList().First().Selected = true;

            MortgageTypes = items;
        }
    }
}