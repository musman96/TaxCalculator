using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using TaxCalculationAPI.Data;
using TaxCalculationAPI.Repositories;

namespace TaxCalculationAPI.Services
{
    public class ProgressiveTaxCalculator : ITaxCalculator
    {
        public class PostalCodeResponse
        {
            public int id { get; set; }
            public string code { get; set; }
            public int taxCalculationTypeID { get; set; }
            public bool isActive { get; set; }
        }
        public List<PostalCodeResponse> GetPostalCode()
        {
            List<PostalCodeResponse> postCode = new List<PostalCodeResponse>();
            using (HttpClient client = new HttpClient())
            {
                var baseUri = new Uri("https://localhost:7138");
                string url = $"{baseUri}api/PostalCode";
                var response = client.GetAsync(new Uri(url)).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = response.Content.ReadAsStringAsync().Result;
                    postCode = JsonSerializer.Deserialize<List<PostalCodeResponse>>(responseBody);
                   // postCode = ode;
                }
                else
                {

                    throw new Exception(response.IsSuccessStatusCode.ToString());

                }
            };
            return postCode;
        }
        public decimal CalculateTax(string postalCode, decimal annualIncome,decimal rate)
        {
            decimal tax = 0;

            // Progressive tax calculation based on the provided table
            if (annualIncome <= 8350)
            {
                tax = annualIncome * rate;
            }
            else if (annualIncome <= 33950)
            {
                tax = 8350 * 0.10m + (annualIncome - 8350) * rate;
            }
            else if (annualIncome <= 82250)
            {
                tax = 8350 * 0.10m + (33950 - 8350) * 0.15m + (annualIncome - 33950) * rate;
            }
            else if (annualIncome <= 171550)
            {
                tax = 8350 * 0.10m + (33950 - 8350) * 0.15m + (82250 - 33950) * 0.25m + (annualIncome - 82250) * rate;
            }
            else if (annualIncome <= 372950)
            {
                tax = 8350 * 0.10m + (33950 - 8350) * 0.15m + (82250 - 33950) * 0.25m + (171550 - 82250) * 0.28m + (annualIncome - 171550) * rate;
            }
            else
            {
                tax = 8350 * 0.10m + (33950 - 8350) * 0.15m + (82250 - 33950) * 0.25m + (171550 - 82250) * 0.28m + (372950 - 171550) * 0.33m + (annualIncome - 372950) * rate;
            }

            return tax;
        }
    }
}
