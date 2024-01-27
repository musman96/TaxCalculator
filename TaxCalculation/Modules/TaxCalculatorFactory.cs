using TaxCalculationAPI.Repositories;

namespace TaxCalculationAPI.Services
{
    public class TaxCalculatorFactory: ITaxCalculatorFactory
    {
        public ITaxCalculator CreateTaxCalculator(string postalCode)
        {
            switch (postalCode)
            {
                case "7441":
                    return new ProgressiveTaxCalculator();
                case "1000":
                    return new ProgressiveTaxCalculator();
                case "A100":
                    return new FlatValueTaxCalculator();
                case "7000":
                    return new FlatRateTaxCalculator();
                default:
                    throw new ArgumentException("Unsupported postal code");
            }
        }
    }
}
