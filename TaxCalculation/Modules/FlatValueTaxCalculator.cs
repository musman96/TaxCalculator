using TaxCalculationAPI.Repositories;

namespace TaxCalculationAPI.Services
{
    public class FlatValueTaxCalculator : ITaxCalculator
    {
        public decimal CalculateTax(string postalCode, decimal annualIncome,decimal rate)
        {
            // Flat value tax calculation logic
            // If the annual income is less than 200000, return the flat value of 10000, else return 5% of the annual income
            // return annualIncome < 200000 ? 10000 : annualIncome * 0.05m;
            return annualIncome < 200000 ? 10000 : annualIncome * rate;
        }
    }
}
