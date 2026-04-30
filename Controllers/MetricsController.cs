using PontuaFlow.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PontuaFlow.Controllers
{
    /// <summary>
    /// Fornece o resumo de Métricas e os Rankings de um Projeto.
    /// </summary>
    [ApiController]
    [Route("api/projects/{projectId}")]
    public class MetricsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MetricsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("metrics/devs")]
        public async Task<IActionResult> GetDevMetrics(int projectId)
        {
            var devs = await _context.Devs
                .Where(d => d.ProjectId == projectId && d.DeletedAt == null)
                .Select(d => new
                {
                    d.Id,
                    d.Nome,
                    d.Cargo,
                    PontosObtidos = d.Tasks.Where(t => t.DeletedAt == null).Sum(t => t.Pontuacao),
                    QtdTasks = d.Tasks.Where(t => t.DeletedAt == null).Count()
                })
                .ToListAsync();

            var metrics = devs.Select(d => new
            {
                DevId = d.Id,
                d.Nome,
                d.Cargo,
                TotalPontos = d.PontosObtidos,
                AproveitamentoPercent = d.QtdTasks == 0 ? 0 : (double)d.PontosObtidos / (d.QtdTasks * 5) * 100
            }).ToList();

            return Ok(metrics);
        }

        [HttpGet("ranking")]
        public async Task<IActionResult> GetRanking(int projectId)
        {
            var devs = await _context.Devs
                .Where(d => d.ProjectId == projectId && d.DeletedAt == null)
                .Select(d => new
                {
                    d.Id,
                    d.Nome,
                    d.Cargo,
                    PontosObtidos = d.Tasks.Where(t => t.DeletedAt == null).Sum(t => t.Pontuacao),
                    QtdTasks = d.Tasks.Where(t => t.DeletedAt == null).Count()
                })
                .ToListAsync();

            var ranking = devs.Select(d => new
            {
                DevId = d.Id,
                d.Nome,
                d.Cargo,
                TotalPontos = d.PontosObtidos,
                AproveitamentoPercent = d.QtdTasks == 0 ? 0 : (double)d.PontosObtidos / (d.QtdTasks * 5) * 100
            }).OrderByDescending(r => r.TotalPontos).ToList();

            return Ok(ranking);
        }
    }
}
