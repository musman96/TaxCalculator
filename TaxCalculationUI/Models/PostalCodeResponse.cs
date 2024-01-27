namespace TaxCalculationUI.Models
{
    public class PostalCodeResponse
    {
        public int id { get; set; }
        public string code { get; set; }
        public int taxCalculationTypeID { get; set; }
        public bool isActive { get; set; }
    }
}

