using PontuaFlow.Data;
using PontuaFlow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PontuaFlow.Controllers
{
    /// <summary>
    /// Gerencia as Semanas (Weeks) dentro do contexto de um Projeto.
    /// </summary>
    [ApiController]
    [Route("api/projects/{projectId}/[controller]")]
    public class WeeksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WeeksController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Week>>> GetWeeks(int projectId)
        {
            return await _context.Weeks.Where(w => w.ProjectId == projectId && w.DeletedAt == null).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Week>> PostWeek(int projectId, Week week)
        {
            if (projectId != week.ProjectId) return BadRequest("ProjectId mismatch");
            
            var existing = await _context.Weeks.FirstOrDefaultAsync(w => w.ProjectId == projectId && w.NumeroSemana == week.NumeroSemana);
            if (existing != null) return BadRequest("NumeroSemana already exists in this project.");
            
            _context.Weeks.Add(week);
            await _context.SaveChangesAsync();
            return Ok(week);
        }

        [HttpPut("{weekId}")]
        public async Task<IActionResult> PutWeek(int projectId, int weekId, Week week)
        {
            if (weekId != week.Id || projectId != week.ProjectId) return BadRequest();

            var existing = await _context.Weeks.FirstOrDefaultAsync(w => w.ProjectId == projectId && w.NumeroSemana == week.NumeroSemana && w.Id != weekId);
            if (existing != null) return BadRequest("NumeroSemana already exists in this project.");

            _context.Entry(week).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{weekId}")]
        public async Task<IActionResult> DeleteWeek(int projectId, int weekId)
        {
            var week = await _context.Weeks.FirstOrDefaultAsync(w => w.Id == weekId && w.ProjectId == projectId);
            if (week == null) return NotFound();
            week.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
