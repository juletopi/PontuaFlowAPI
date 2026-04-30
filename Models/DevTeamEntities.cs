using System.Text.Json.Serialization;

namespace PontuaFlow.Models
{
    public class DevTeam
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public required string Nome { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; }

        [JsonIgnore]
        public Project? Project { get; set; }
        [JsonIgnore]
        public ICollection<DevTeamMember> DevTeamMembers { get; set; } = new List<DevTeamMember>();
        [JsonIgnore]
        public ICollection<TaskTeamAssignment> TaskTeamAssignments { get; set; } = new List<TaskTeamAssignment>();
    }

    public class DevTeamMember
    {
        public int DevId { get; set; }
        public int DevTeamId { get; set; }
        public DateTime? JoinedAt { get; set; }

        [JsonIgnore]
        public Dev? Dev { get; set; }
        [JsonIgnore]
        public DevTeam? DevTeam { get; set; }
    }

    public class TaskTeamAssignment
    {
        public int TaskId { get; set; }
        public int DevTeamId { get; set; }

        [JsonIgnore]
        public Task? Task { get; set; }
        [JsonIgnore]
        public DevTeam? DevTeam { get; set; }
    }
}
