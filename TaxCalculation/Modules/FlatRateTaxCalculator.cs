using TaxCalculationAPI.Data;
using TaxCalculationAPI.Repositories;

namespace TaxCalculationAPI.Services
{
    public class FlatRateTaxCalculator : ITaxCalculator
    {
        public FlatRateTaxCalculator()
        {
        }
        public decimal CalculateTax(string postalCode, decimal annualIncome,decimal rate)
        {
            // Flat rate tax calculation logic
            // Return 17.5% of the annual income

            //return annualIncome * 0.175m;
            return annualIncome * rate;
        }
    }
}
