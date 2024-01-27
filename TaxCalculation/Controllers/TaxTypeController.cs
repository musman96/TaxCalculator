using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaxCalculationAPI.Data;

namespace TaxCalculationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class TaxTypeController : ControllerBase
    {
        private readonly TaxCalculationDBContext _dbContext;
        public TaxTypeController(TaxCalculationDBContext DbContext)
        {
            _dbContext = DbContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaxType>>> GetTaxCalculationType()
        {
            if (_dbContext.TaxType == null)
            {
                return NotFound();
            }
            return await _dbContext.TaxType.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaxType>> GetTaxType(int id)
        {
            if (_dbContext.TaxType == null)
            {
                return NotFound();
            }

            var taxType = await _dbContext.TaxType.FindAsync(id);

            if (taxType == null)
            {
                return NotFound();
            }

            return taxType;
        }

        [HttpPost]
        public async Task<ActionResult<TaxType>> PostTaxType(TaxType taxType)
        {
            _dbContext.TaxType.Add(taxType);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTaxType), new { id = taxType.ID }, taxType);

        }


        [HttpPut]
        public async Task<IActionResult> PutTaxType(int id, TaxType taxType)
        {

            if (id != taxType.ID)
            {
                return BadRequest();
            }

            _dbContext.Entry(taxType).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaxCalculationTypeAvailable(id))
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
        private bool TaxCalculationTypeAvailable(int id)
        {
            return (_dbContext.TaxType?.Any(x => x.ID == id)).GetValueOrDefault();
        }


        /// <summary>
        /// Deletes a specific Postal Code.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaxCalculationType(int id)
        {
            if (_dbContext.TaxType == null)
            {
                return NotFound();
            }

            var taxCalculationType = await _dbContext.TaxType.FindAsync(id);
            if (taxCalculationType == null)
            {
                return NotFound();
            }
            _dbContext.Remove(taxCalculationType);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
