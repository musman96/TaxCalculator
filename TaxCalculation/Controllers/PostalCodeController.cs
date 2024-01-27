using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaxCalculationAPI.Data;

namespace TaxCalculationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class PostalCodeController : ControllerBase
    {
        private readonly TaxCalculationDBContext _dbContext;
        public PostalCodeController(TaxCalculationDBContext DbContext)
        {
            _dbContext = DbContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostalCode>>> GetPostalCode()
        {
            if (_dbContext.PostalCode == null)
            {
                return NotFound();
            }
            return await _dbContext.PostalCode.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostalCode>> GetPostalCodeById(int id)
        {
            if (_dbContext.PostalCode == null)
            {
                return NotFound();
            }

            var postalCode = await _dbContext.PostalCode.FindAsync(id);

            if (postalCode == null)
            {
                return NotFound();
            }

            return postalCode;
        }

        [HttpGet("GetPostalCodeByCode")]
        public async Task<ActionResult<PostalCode>> GetPostalCodeByCode(string postalCode)
        {
            if (_dbContext.PostalCode == null)
            {
                return NotFound();
            }

            var code = await _dbContext.PostalCode.Where(x=>x.Code == postalCode).FirstOrDefaultAsync();

            if (code == null)
            {
                return NotFound();
            }

            return code;
        }

        [HttpPost]
        public async Task<ActionResult<PostalCode>> PostPostalCode(PostalCode postalCode)
        {
            _dbContext.PostalCode.Add(postalCode);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPostalCode), new { id = postalCode.ID }, postalCode);

        }


        [HttpPut]
        public async Task<IActionResult> PutPostalCode(int id, PostalCode postalCode)
        {

            if (id != postalCode.ID)
            {
                return BadRequest();
            }

            _dbContext.Entry(postalCode).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostalCodeAvailable(id))
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
        private bool PostalCodeAvailable(int id)
        {
            return (_dbContext.PostalCode?.Any(x => x.ID == id)).GetValueOrDefault();
        }


        /// <summary>
        /// Deletes a specific Postal Code.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostalCode(int id)
        {
            if (_dbContext.PostalCode == null)
            {
                return NotFound();
            }

            var postalCode = await _dbContext.PostalCode.FindAsync(id);
            if (postalCode == null)
            {
                return NotFound();
            }
            _dbContext.Remove(postalCode);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
