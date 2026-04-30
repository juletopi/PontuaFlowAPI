using PontuaFlow.Data;
using PontuaFlow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PontuaFlow.Controllers
{
    /// <summary>
    /// Gerencia os Times (DevTeams) e seus Membros.
    /// </summary>
    [ApiController]
    [Route("api/projects/{projectId}/teams")]
    public class DevTeamsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DevTeamsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DevTeam>>> GetTeams(int projectId)
        {
            return await _context.DevTeams.Where(dt => dt.ProjectId == projectId && dt.DeletedAt == null).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<DevTeam>> PostTeam(int projectId, DevTeam team)
        {
            if (projectId != team.ProjectId) return BadRequest("ProjectId mismatch");
            
            _context.DevTeams.Add(team);
            await _context.SaveChangesAsync();
            return Ok(team);
        }

        [HttpPut("{teamId}")]
        public async Task<IActionResult> PutTeam(int projectId, int teamId, DevTeam team)
        {
            if (teamId != team.Id || projectId != team.ProjectId) return BadRequest();
            _context.Entry(team).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{teamId}")]
        public async Task<IActionResult> DeleteTeam(int projectId, int teamId)
        {
            var team = await _context.DevTeams.FirstOrDefaultAsync(t => t.Id == teamId && t.ProjectId == projectId);
            if (team == null) return NotFound();
            team.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("{teamId}/members/{devId}")]
        public async Task<IActionResult> AddMember(int projectId, int teamId, int devId)
        {
            var member = new DevTeamMember { DevTeamId = teamId, DevId = devId, JoinedAt = DateTime.UtcNow };
            _context.DevTeamMembers.Add(member);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{teamId}/members/{devId}")]
        public async Task<IActionResult> RemoveMember(int projectId, int teamId, int devId)
        {
            var member = await _context.DevTeamMembers.FirstOrDefaultAsync(m => m.DevTeamId == teamId && m.DevId == devId);
            if (member == null) return NotFound();
            _context.DevTeamMembers.Remove(member);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        
        [HttpPost("{teamId}/assignments/{taskId}")]
        public async Task<IActionResult> AddTaskAssignment(int projectId, int teamId, int taskId)
        {
            var assignment = new TaskTeamAssignment { DevTeamId = teamId, TaskId = taskId };
            _context.TaskTeamAssignments.Add(assignment);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{teamId}/assignments/{taskId}")]
        public async Task<IActionResult> RemoveTaskAssignment(int projectId, int teamId, int taskId)
        {
            var assignment = await _context.TaskTeamAssignments.FirstOrDefaultAsync(a => a.DevTeamId == teamId && a.TaskId == taskId);
            if (assignment == null) return NotFound();
            _context.TaskTeamAssignments.Remove(assignment);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
