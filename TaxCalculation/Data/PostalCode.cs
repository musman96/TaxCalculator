using System.ComponentModel.DataAnnotations;

namespace TaxCalculationAPI.Data
{
    public class PostalCode
    {
        [Key]
        public int ID { get; set; }
        public string? Code { get; set; }
        public int TaxCalculationTypeID { get; set; }
        public bool IsActive { get; set; }
    }
}
