namespace TaxCalculationAPI.Repositories
{
    public interface ITaxCalculatorFactory
    {
        ITaxCalculator CreateTaxCalculator(string postalCode);
    }
}
