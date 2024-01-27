namespace TaxCalculationUI.Models
{
    public class TaxcalculationRequest
    {
        public int id { get; set; }
        public string postalCode { get; set; }
        public int annualAmount { get; set; }
        public decimal calculatedTaxAmount { get; set; }
        public DateTime createdDateTime { get; set; }
        public bool isActive { get; set; }
    }
}
