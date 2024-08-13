using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Project_2_34854673.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;



namespace API_Project_2_34854673.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class JobTelemetryController : ControllerBase
    {
        private readonly NwutechTrendsContext _context;

        public JobTelemetryController(NwutechTrendsContext context)
        {
            _context = context;
        }

        // GET: JobTelemetry
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobTelemetry>>> GetJobTelemetries()
        {
            return await _context.JobTelemetries.ToListAsync();
        }

        // GET:JobTelemetry/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<JobTelemetry>> GetJobTelemetry(int id)
        {
            var jobTelemetry = await _context.JobTelemetries.FindAsync(id);

            if (jobTelemetry == null)
            {
                return NotFound();
            }

            return jobTelemetry;
        }

        // POST: api/JobTelemetry
        [HttpPost]
        public async Task<ActionResult<JobTelemetry>> PostJobTelemetry(JobTelemetry jobTelemetry)
        {
            _context.JobTelemetries.Add(jobTelemetry);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetJobTelemetry), new { id = jobTelemetry.Id }, jobTelemetry);
        }

        // PATCH: api/JobTelemetry/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchJobTelemetry(int id, JobTelemetry jobTelemetry)
        {
            if (id != jobTelemetry.Id)
            {
                return BadRequest();
            }

            if (!JobTelemetryExists(id))
            {
                return NotFound();
            }

            _context.Entry(jobTelemetry).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // DELETE: api/JobTelemetry/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobTelemetry(int id)
        {
            if (!JobTelemetryExists(id))
            {
                return NotFound();
            }

            var jobTelemetry = await _context.JobTelemetries.FindAsync(id);

            _context.JobTelemetries.Remove(jobTelemetry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/JobTelemetry/GetSavingsByProject
        [HttpGet("GetSavingsByProject")]
        public async Task<ActionResult> GetSavingsByProject(Guid projectId, DateTime startDate, DateTime endDate)
        {
            var telemetryData = await _context.JobTelemetries
                .Where(t => t.ProjectId == projectId && t.EntryDate >= startDate && t.EntryDate <= endDate)
                .ToListAsync();

            var cumulativeTimeSaved = telemetryData.Sum(t => t.HumanTime ?? 0);
            var cumulativeCostSaved = cumulativeTimeSaved * 0.1; // Example calculation for cost saved

            return Ok(new { CumulativeTimeSaved = cumulativeTimeSaved, CumulativeCostSaved = cumulativeCostSaved });
        }

        // GET: api/JobTelemetry/GetSavingsByClient
        [HttpGet("GetSavingsByClient")]
        public async Task<ActionResult> GetSavingsByClient(Guid clientId, DateTime startDate, DateTime endDate)
        {
            var telemetryData = await _context.JobTelemetries
                .Join(_context.Projects, t => t.ProjectId, p => p.ProjectId, (t, p) => new { t, p })
                .Where(tp => tp.p.ClientId == clientId && tp.t.EntryDate >= startDate && tp.t.EntryDate <= endDate)
                .Select(tp => tp.t)
                .ToListAsync();

            var cumulativeTimeSaved = telemetryData.Sum(t => t.HumanTime ?? 0);
            var cumulativeCostSaved = cumulativeTimeSaved * 0.1;

            return Ok(new { CumulativeTimeSaved = cumulativeTimeSaved, CumulativeCostSaved = cumulativeCostSaved });
        }

        private bool JobTelemetryExists(int id)
        {
            return _context.JobTelemetries.Any(e => e.Id == id);
        }
    }

}
