using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaxCalculationAPI.Data;
using TaxCalculationAPI.Repositories;

namespace TaxCalculationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private ITaxService taxService;
        public TestController(ITaxService service)
        {
            taxService = service;              
        }

        [HttpGet]
        public async Task<ActionResult<decimal>> CalculateTax(string postalCode, decimal annualIncome) 
        {
            var tax = await Task.Run(()=> taxService.CalculateTax(postalCode, annualIncome));

            return tax;
        }
    }
}
