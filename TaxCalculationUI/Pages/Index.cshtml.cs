using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;
using TaxCalculationUI.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TaxCalculationUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public string postalCode { get; set; }
        [BindProperty]
        public string annualAmount { get;set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            using (HttpClient client = new HttpClient())
            {
                var baseUri= new Uri("https://localhost:7138");
                string url = $"{baseUri}api/PostalCode";
                var response = client.GetAsync(new Uri(url)).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = response.Content.ReadAsStringAsync().Result;
                    var postCode = JsonSerializer.Deserialize<List<PostalCodeResponse>>(responseBody);
                    ViewData["Codes"] = new SelectList(postCode, "code", "code");
                }
                else
                {

                    throw new Exception(response.IsSuccessStatusCode.ToString());

                }
            };

            //ViewData.codes = 
        }

        public void OnPost()
        {
            //calculate tax
            SaveTaxCalculation();

        }


        private decimal CalculateTax()
        {
            decimal tax = 0;
            using (HttpClient client = new HttpClient())
            {
                var baseUri = new Uri("https://localhost:7138");
                string url = $"{baseUri}api/CalculatedTax/CalculateTax?postalCode={postalCode}&annualIncome={annualAmount}";
                var response = client.PostAsync(new Uri(url),null).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = response.Content.ReadAsStringAsync().Result;
                     tax = JsonSerializer.Deserialize<decimal>(responseBody);
                }
                else
                {

                    throw new Exception(response.IsSuccessStatusCode.ToString());

                }
            };

            return tax;
        }

        private bool SaveTaxCalculation()
        {
            //calculate the tax
            var taxamount = CalculateTax();

            // save the tax calculation
            using (HttpClient client = new HttpClient())
            {
                var baseUri = new Uri("https://localhost:7138");
                string url = $"{baseUri}api/CalculatedTax";
                TaxcalculationRequest tax = new TaxcalculationRequest()
                {
                    postalCode = postalCode,
                    annualAmount = int.Parse(annualAmount),
                    isActive = true,
                    calculatedTaxAmount = taxamount

                };
                StringContent content = new StringContent(JsonSerializer.Serialize(tax), Encoding.UTF8, "application/json");
                var response = client.PostAsync(new Uri(url),content).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = response.Content.ReadAsStringAsync().Result;
                    var taxcalculationRequest = JsonSerializer.Deserialize<TaxcalculationRequest>(responseBody);

                    if (taxcalculationRequest != null)
                    {
                        ViewData["TaxAmount"] = taxamount;
                        return true;

                       
                    }
                 
                }
                else
                {

                    return false;

                }
                return false;
            };
        }
    }
}