using PontuaFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace PontuaFlow.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Week> Weeks { get; set; }
        public DbSet<Dev> Devs { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<DevTeam> DevTeams { get; set; }
        public DbSet<DevTeamMember> DevTeamMembers { get; set; }
        public DbSet<TaskTeamAssignment> TaskTeamAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Week>()
                .HasIndex(w => new { w.ProjectId, w.NumeroSemana })
                .IsUnique();

            modelBuilder.Entity<DevTeamMember>()
                .HasKey(dtm => new { dtm.DevId, dtm.DevTeamId });

            modelBuilder.Entity<TaskTeamAssignment>()
                .HasKey(tta => new { tta.TaskId, tta.DevTeamId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
