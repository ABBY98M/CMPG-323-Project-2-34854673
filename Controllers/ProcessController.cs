using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Project_2_34854673.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace API_Project_2_34854673.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        private readonly NwutechTrendsContext _context;

        public ProcessController(NwutechTrendsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Process>>> GetProcesses()
        {
            return await _context.Processes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Process>> GetProcess(Guid id)
        {
            var process = await _context.Processes.FindAsync(id);

            if (process == null)
            {
                return NotFound();
            }

            return process;
        }

        [HttpPost]
        public async Task<ActionResult<Process>> PostProcess(Process process)
        {
            _context.Processes.Add(process);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProcess), new { id = process.ProcessId }, process);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchProcess(Guid id, Process process)
        {
            if (id != process.ProcessId)
            {
                return BadRequest();
            }

            _context.Entry(process).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProcessExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcess(Guid id)
        {
            var process = await _context.Processes.FindAsync(id);

            if (process == null)
            {
                return NotFound();
            }

            _context.Processes.Remove(process);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProcessExists(Guid id)
        {
            return _context.Processes.Any(e => e.ProcessId == id);
        }
    }
}
