using PontuaFlow.Data;
using PontuaFlow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PontuaFlow.Controllers
{
    /// <summary>
    /// Gerencia os Desenvolvedores (Devs) atuantes num Projeto específico.
    /// </summary>
    [ApiController]
    [Route("api/projects/{projectId}/[controller]")]
    public class DevsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DevsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dev>>> GetDevs(int projectId)
        {
            return await _context.Devs.Where(d => d.ProjectId == projectId && d.DeletedAt == null).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Dev>> PostDev(int projectId, Dev dev)
        {
            if (projectId != dev.ProjectId) return BadRequest("ProjectId mismatch");
            
            _context.Devs.Add(dev);
            await _context.SaveChangesAsync();
            return Ok(dev);
        }

        [HttpPut("{devId}")]
        public async Task<IActionResult> PutDev(int projectId, int devId, Dev dev)
        {
            if (devId != dev.Id || projectId != dev.ProjectId) return BadRequest();
            _context.Entry(dev).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{devId}")]
        public async Task<IActionResult> DeleteDev(int projectId, int devId)
        {
            var dev = await _context.Devs.FirstOrDefaultAsync(d => d.Id == devId && d.ProjectId == projectId);
            if (dev == null) return NotFound();
            dev.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
