using System.ComponentModel.DataAnnotations;

namespace TaxCalculationAPI.Data
{
    public class CalculationType
    {
        [Key]
        public int ID { get; set; }
        public int TaxCalculationTypeID { get; set; }
        public decimal RatePercentage { get; set; }
        public decimal RateFrom { get; set; }
        public decimal RateTo { get; set; }
        public bool IsActive { get; set; }
    }
}
