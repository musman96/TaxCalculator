namespace TaxCalculationAPI.Repositories
{
    public interface ITaxCalculator
    {
        decimal CalculateTax(string postalCode, decimal annualIncome, decimal rate);
    }
}
