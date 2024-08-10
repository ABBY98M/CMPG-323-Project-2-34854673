using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Project_2_34854673.Data;
using API_Project_2_34854673.Models;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace API_Project_2_34854673.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelemetryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TelemetryController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/telemetry
        [HttpGet("{id}")]
        public async Task<ActionResult<JobTelemetry>> GetTelemetryById(int id)
        {
            // Find the JobTelemetry entry by ID
            var telemetryEntry = await _context.JobTelemetries.FindAsync(id);

            // Check if the entry exists
            if (telemetryEntry == null)
            {
                return NotFound();
            }

            // Return the entry if found
            return Ok(telemetryEntry);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobTelemetryById(int id)
        {
            var jobTelemetry = await _context.JobTelemetries.FindAsync(id);

            if (jobTelemetry == null)
            {
                return NotFound();
            }

            return Ok(jobTelemetry);
        }


        /// POST: api/JobTelemetry
        [HttpPost]
        public async Task<IActionResult> CreateJobTelemetry([FromBody] JobTelemetry jobTelemetry)
        {
            if (jobTelemetry == null)
            {
                return BadRequest("JobTelemetry data is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.JobTelemetries.Add(jobTelemetry);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetJobTelemetryById), new { id = jobTelemetry.Id }, jobTelemetry);
        }

        private bool JobTelemetryExists(int id)
        {
            // Check if the JobTelemetry entry exists
            return _context.JobTelemetries.Any(e => e.Id == id);
        }

        // PATCH: api/JobTelemetry/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchJobTelemetry(int id, [FromBody] JobTelemetry updatedTelemetry)
        {
            // Check if the ID exists
            var existingTelemetry = await _context.JobTelemetry.FindAsync(id);
            if (existingTelemetry == null)
            {
                return NotFound();
            }

            // Update only the fields that were provided in the request
            if (updatedTelemetry.ProccesId != null)
            {
                existingTelemetry.ProccesID = updatedTelemetry.ProccesId;
            }
            if (updatedTelemetry.JobId != null)
            {
                existingTelemetry.JobID = updatedTelemetry.JobId;
            }
            // Add more fields to update as necessary
            // ...

            // Save changes to the database
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unable to save changes.");
            }

            return NoContent();
        }


        // PATCH: api/telemetry/{id}
        
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateJobTelemetry(int id, [FromBody] JsonPatchDocument<JobTelemetry> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("Patch document cannot be null.");
            }

            var jobTelemetry = await _context.JobTelemetries.FindAsync(id);

            if (jobTelemetry == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(jobTelemetry, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobTelemetryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        */

        // DELETE: api/telemetry/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobTelemetry(int id)
        {
            var jobTelemetry = await _context.JobTelemetries.FindAsync(id);

            if (jobTelemetry == null)
            {
                return NotFound();
            }

            _context.JobTelemetries.Remove(jobTelemetry);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobTelemetryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // GET: api/telemetry/savings
        [HttpGet("savings")]
        public async Task<IActionResult> GetSavings([FromQuery] Guid projectId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] Guid clientId)
        {
            // Query the database for telemetry data within the specified date range and project ID
            var telemetryData = await _context.JobTelemetries
                .Where(t => t.ProjectID == projectId &&
                            t.ClientID == clientId &&
                            t.EntryDate >= startDate &&
                            t.EntryDate <= endDate)
                .ToListAsync();

            // Calculating the cumulative time and cost saved
            var cumulativeTimeSaved = telemetryData.Sum(t => t.HumanTime ?? 0);
          
            var cumulativeCostSaved = 0; 
            // Creating an anonymous  to return the results
            var result = new
            {
                ProjectID = projectId,
                StartDate = startDate,
                EndDate = endDate,
                CumulativeTimeSaved = cumulativeTimeSaved,
                CumulativeCostSaved = cumulativeCostSaved
            };

            return Ok(result);
        }



    }

}
