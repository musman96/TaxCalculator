using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaxCalculationAPI.Data;
using TaxCalculationAPI.Repositories;
using TaxCalculationAPI.Services;

namespace TaxCalculationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatedTaxController : ControllerBase
    {
        private readonly TaxCalculationDBContext _dbContext;
        private readonly ITaxService taxService;
        public CalculatedTaxController(TaxCalculationDBContext DbContext,TaxService service)
        {
            _dbContext = DbContext;
            taxService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CalculatedTax>>> GetCalculatedTax()
        {
            if (_dbContext.CalculatedTax == null)
            {
                return NotFound();
            }
            return await _dbContext.CalculatedTax.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CalculatedTax>> GetCalculatedTax(int id)
        {
            if (_dbContext.CalculatedTax == null)
            {
                return NotFound();
            }

            var calculatedTax = await _dbContext.CalculatedTax.FindAsync(id);

            if (calculatedTax == null)
            {
                return NotFound();
            }

            return calculatedTax;
        }

        [HttpPost]
        public async Task<ActionResult<CalculatedTax>> PostCalculatedTax(CalculatedTax calculatedTax)
        {
            _dbContext.CalculatedTax.Add(calculatedTax);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCalculatedTax), new { id = calculatedTax.ID }, calculatedTax);

        }

        [HttpPost("CalculateTax")]
        public async Task<decimal> CalculateTax(string postalCode, decimal annualIncome)
        {
            var tax_amount =await Task.Run(()=>taxService.CalculateTax(postalCode, annualIncome));

            return tax_amount;
        }


        [HttpPut]
        public async Task<IActionResult> PutCalculatedTax(int id, CalculatedTax calculatedTax)
        {

            if (id != calculatedTax.ID)
            {
                return BadRequest();
            }

            _dbContext.Entry(calculatedTax).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalculatedTaxAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


            return Ok();

        }
        private bool CalculatedTaxAvailable(int id)
        {
            return (_dbContext.CalculatedTax?.Any(x => x.ID == id)).GetValueOrDefault();
        }


        /// <summary>
        /// Deletes a specific Postal Code.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalculatedTax(int id)
        {
            if (_dbContext.CalculatedTax == null)
            {
                return NotFound();
            }

            var calculatedTax = await _dbContext.CalculatedTax.FindAsync(id);
            if (calculatedTax == null)
            {
                return NotFound();
            }
            _dbContext.Remove(calculatedTax);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
