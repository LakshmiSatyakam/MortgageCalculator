using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using MortgageCalculator.Api.Services;
using MortgageCalculator.Dto;
using Newtonsoft;

namespace MortgageCalculator.Api.Controllers
{
    public class MortgageController : ApiController
    {
        // GET: api/Mortgage
        public IEnumerable<Dto.Mortgage> Get()
        {
            var mortgageService = new MortgageService();
            return mortgageService.GetAllMortgages().OrderBy(x => x.MortgageType).ThenBy(x => x.InterestRate);
        }

        // GET: api/Mortgage/5
        public Dto.Mortgage Get(int id)
        {
            var mortgageService = new MortgageService();
            return mortgageService.GetAllMortgages().FirstOrDefault(x => x.MortgageId == id);
        }

        // GET: api/mortgage?loanAmount=1000&duration=2&interestRate=5.24
        public LoanDetails Get(double loanAmount, int duration, double interestRate)
        {
            var mortgageService = new MortgageService();
            return mortgageService.CalculateLoanDetails(loanAmount, duration, interestRate);
        }
    }
}
