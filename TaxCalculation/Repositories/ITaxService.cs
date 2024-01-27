namespace TaxCalculationAPI.Repositories
{
    public interface ITaxService
    {
        decimal CalculateTax(string postalCode, decimal annualIncome);
        void StoreTaxData(string postalCode, decimal annualIncome, decimal calculatedTax);
    }
}
