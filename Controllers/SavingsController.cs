using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Project_2_34854673.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;

namespace API_Project_2_34854673.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SavingsController : ControllerBase
    {
        private readonly NwutechTrendsContext _context;

        public SavingsController(NwutechTrendsContext context)
        {
            _context = context;
        }

        [HttpGet("GetSavingsByProject")]
        public async Task<ActionResult> GetSavingsByProject(Guid projectId, DateTime startDate, DateTime endDate)
        {
            var telemetryData = await _context.JobTelemetries
                .Where(t => t.ProjectId == projectId && t.EntryDate >= startDate && t.EntryDate <= endDate)
                .ToListAsync();

            var cumulativeTimeSaved = telemetryData.Sum(t => t.HumanTime ?? 0);
            var cumulativeCostSaved = cumulativeTimeSaved * 0.1; 

            return Ok(new { CumulativeTimeSaved = cumulativeTimeSaved, CumulativeCostSaved = cumulativeCostSaved });
        }

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
    }
}
