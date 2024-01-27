using System.ComponentModel.DataAnnotations;

namespace TaxCalculationAPI.Data
{
    public class TaxType
    {
        [Key]
        public int ID { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}
