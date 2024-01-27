using Microsoft.EntityFrameworkCore;
using TaxCalculationAPI.Data;
using TaxCalculationAPI.Repositories;

namespace TaxCalculationAPI.Services
{
    public class RateService: IRateService
    {
        private readonly TaxCalculationDBContext _dBContext;
        public RateService(TaxCalculationDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public decimal getRatePercentage(string postalCode)
        {
            var code = _dBContext.PostalCode.Where(x => x.Code == postalCode).FirstOrDefault();

            if (code != null)
            {
                var rate = _dBContext.CalculationType
                                      .Where(x => x.TaxCalculationTypeID == code.TaxCalculationTypeID)
                                      .FirstOrDefault();

                if (rate != null)
                {
                    return rate.RatePercentage;
                }
            }

            // Default value in case of null references
            return 0;

        }

    }
}
