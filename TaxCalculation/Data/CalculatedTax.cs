using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TaxCalculationAPI.Data
{
    public class CalculatedTax
    {
        [Key]
        public int ID { get; set; }
        public string? PostalCode { get; set; }
        public decimal AnnualAmount { get; set; }
        public decimal CalculatedTaxAmount { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }
    }
}
