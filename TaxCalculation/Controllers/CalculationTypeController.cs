using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaxCalculationAPI.Data;

namespace TaxCalculationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CalculationTypeController : ControllerBase
    {
        private readonly TaxCalculationDBContext _dbContext;
        public CalculationTypeController(TaxCalculationDBContext DbContext)
        {
            _dbContext = DbContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CalculationType>>> GetProgressiveTax()
        {
            if (_dbContext.CalculationType == null)
            {
                return NotFound();
            }
            return await _dbContext.CalculationType.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CalculationType>> GetProgressiveTax(int id)
        {
            if (_dbContext.CalculationType == null)
            {
                return NotFound();
            }

            var calculationType = await _dbContext.CalculationType.FindAsync(id);

            if (calculationType == null)
            {
                return NotFound();
            }

            return calculationType;
        }

        [HttpPost]
        public async Task<ActionResult<CalculationType>> PostProgressiveTax(CalculationType calculationType)
        {
            _dbContext.CalculationType.Add(calculationType);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProgressiveTax), new { id = calculationType.ID }, calculationType);

        }


        [HttpPut]
        public async Task<IActionResult> PutProgressiveTax(int id, CalculationType calculationType)
        {

            if (id != calculationType.ID)
            {
                return BadRequest();
            }

            _dbContext.Entry(calculationType).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalculationTypeAvailable(id))
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
        private bool CalculationTypeAvailable(int id)
        {
            return (_dbContext.CalculationType?.Any(x => x.ID == id)).GetValueOrDefault();
        }


        /// <summary>
        /// Deletes a specific calculation type.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalculationType(int id)
        {
            if (_dbContext.CalculationType == null)
            {
                return NotFound();
            }

            var calculationType = await _dbContext.CalculationType.FindAsync(id);
            if (calculationType == null)
            {
                return NotFound();
            }
            _dbContext.Remove(calculationType);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
