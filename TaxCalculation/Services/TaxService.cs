using TaxCalculationAPI.Data;
using TaxCalculationAPI.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaxCalculationAPI.Services
{
    public class TaxService: ITaxService
    {
        private readonly ITaxCalculatorFactory _taxCalculatorFactory;
        private readonly TaxCalculationDBContext _dBContext;

        public TaxService(ITaxCalculatorFactory taxCalculatorFactory, TaxCalculationDBContext context)
        {
            _taxCalculatorFactory = taxCalculatorFactory;
            _dBContext = context;
        }

        public decimal CalculateTax(string postalCode, decimal annualIncome)
        {
            var rate = getRatePercentage(postalCode,annualIncome);
            var taxCalculator = _taxCalculatorFactory.CreateTaxCalculator(postalCode);
            return taxCalculator.CalculateTax(postalCode, annualIncome,rate);
        }

        public void StoreTaxData(string postalCode, decimal annualIncome, decimal calculatedTax)
        {
            // Implement data storage logic (e.g., store in SQL Server with date/time)
            // ...
            // Placeholder logic for demonstration purposes
            Console.WriteLine($"Data stored: Postal Code: {postalCode}, Income: {annualIncome}, Tax: {calculatedTax}");
        }

        public decimal getRatePercentage(string postalCode, decimal income)
        {
            var code = _dBContext.PostalCode.Where(x => x.Code == postalCode).FirstOrDefault();

            if (code != null)
            {
                var range = GetIncomeRange(income);
                var rate = _dBContext.CalculationType
                                      .Where(x => x.TaxCalculationTypeID == code.TaxCalculationTypeID && x.RateTo == range)
                                      .FirstOrDefault();

                if (rate != null)
                {
                    return rate.RatePercentage;
                }
            }

            // Default value in case of null references
            return 0;

        }

        private int GetIncomeRange(decimal annualIncome)
        {
            if (annualIncome <= 8350)
            {
                return 8350;
            }
            else if (annualIncome <= 33950)
            {
                return 33950;
            }
            else if (annualIncome <= 82250)
            {
                return 82250;
            }
            else if (annualIncome <= 171550)
            {
                return 171550;
            }
            else if (annualIncome == 10000)
            {
                return 372950;
            }
            else if (annualIncome == 200000)
            {
                return 372950;
            }
            return 0;
        }
    }
}
