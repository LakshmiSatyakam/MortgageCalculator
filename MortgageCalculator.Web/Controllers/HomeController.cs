using MortgageCalculator.Dto;
using MortgageCalculator.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MortgageCalculator.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var mortgageDetails = GetMortgageDetails().GetAwaiter().GetResult();
            TempData["MortgageDetails"] = mortgageDetails;
            TempData["LoanDetailsM"] = new LoanDetailsM();
            return View(new MortgageM(mortgageDetails));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public PartialViewResult CalculateMortgage(MortgageM mortgageM)
        {
            if (ModelState.IsValid)
            {
                var loanDetails = Calculate(mortgageM).GetAwaiter().GetResult();
                LoanDetailsM loanDetailsM = new LoanDetailsM()
                {
                    Duration = mortgageM.Duration,
                    InterestRate = mortgageM.InterestRate,
                    LoanAmount = mortgageM.LoanAmount,
                    TotalInterest = loanDetails.TotalInterest,
                    TotalLoan = loanDetails.TotalLoan
                };

                return PartialView("_PartialLoanDetails", loanDetailsM);
            }

            throw new HttpException("Invalid values supplied!");
        }

        private async Task<IList<Mortgage>> GetMortgageDetails()
        {
            string url = "http://localhost:49608/api/mortgage";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync(url).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            // Deserialize the response body.
            string data = await response.Content.ReadAsStringAsync();
            JavaScriptSerializer JSserializer = new JavaScriptSerializer();
            IList<Mortgage> mortgages = JSserializer.Deserialize<List<Mortgage>>(data);
            return mortgages;
        }

        private async Task<LoanDetails> Calculate(MortgageM mortgageM)
        {
            // mortgage?loanAmount=1000&duration=2&name=aaaaa
            string url = "http://localhost:49608/api/mortgage?";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            url = url + "&loanAmount=" + mortgageM.LoanAmount.ToString() + "&duration=" + mortgageM.Duration.ToString() + "&interestRate=" + mortgageM.InterestRate;
            var response = await client.GetAsync(url).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            // Deserialize the response body.
            string data = await response.Content.ReadAsStringAsync();
            JavaScriptSerializer JSserializer = new JavaScriptSerializer();
            return JSserializer.Deserialize<LoanDetails>(data);
        }
    }
}