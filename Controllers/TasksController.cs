using PontuaFlow.Data;
using PontuaFlow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task = PontuaFlow.Models.Task;

namespace PontuaFlow.Controllers
{
    /// <summary>
    /// Gerencia as Tasks vinculadas aos Projetos e seus respectivos Desenvolvedores e Semanas.
    /// </summary>
    [ApiController]
    [Route("api/projects/{projectId}/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Task>>> GetTasks(int projectId)
        {
            return await _context.Tasks.Where(t => t.ProjectId == projectId && t.DeletedAt == null).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Task>> PostTask(int projectId, Task task)
        {
            if (projectId != task.ProjectId) return BadRequest("ProjectId mismatch");

            int[] valoresPermitidos = { 0, 2, 3, 5, 8 };
            if (!valoresPermitidos.Contains(task.Pontuacao)) return BadRequest("Pontuacao must be 0, 2, 3, 5, or 8.");

            var dev = await _context.Devs.FindAsync(task.DevId);
            if (dev == null || dev.ProjectId != projectId) return BadRequest("Dev doesn't belong to this project.");

            var week = await _context.Weeks.FindAsync(task.WeekId);
            if (week == null || week.ProjectId != projectId) return BadRequest("Week doesn't belong to this project.");

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return Ok(task);
        }

        [HttpPut("{taskId}")]
        public async Task<IActionResult> PutTask(int projectId, int taskId, Task task)
        {
            if (taskId != task.Id || projectId != task.ProjectId) return BadRequest();

            int[] valoresPermitidos = { 0, 2, 3, 5, 8 };
            if (!valoresPermitidos.Contains(task.Pontuacao)) return BadRequest("Pontuacao must be 0, 2, 3, 5, or 8.");

            var dev = await _context.Devs.FindAsync(task.DevId);
            if (dev == null || dev.ProjectId != projectId) return BadRequest("Dev doesn't belong to this project.");

            var week = await _context.Weeks.FindAsync(task.WeekId);
            if (week == null || week.ProjectId != projectId) return BadRequest("Week doesn't belong to this project.");

            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTask(int projectId, int taskId)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == taskId && t.ProjectId == projectId);
            if (task == null) return NotFound();
            task.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
