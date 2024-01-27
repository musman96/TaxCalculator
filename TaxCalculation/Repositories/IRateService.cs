namespace TaxCalculationAPI.Repositories
{
    public interface IRateService
    {
        decimal getRatePercentage(string postalCode);
    }
}
