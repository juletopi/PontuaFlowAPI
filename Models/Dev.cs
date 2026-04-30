using System.Text.Json.Serialization;

namespace PontuaFlow.Models
{
    /// <summary>
    /// Um Desenvolvedor em um Projeto.
    /// </summary>
    public class Dev
    {
        public int Id { get; set; }

        /// <example>1</example>
        public int ProjectId { get; set; }

        /// <example>Jorel Amando</example>
        public required string Nome { get; set; }

        /// <example>Desenvolvedor Backend</example>
        public required string Cargo { get; set; }

        /// <example>jor3llove.silva@exemplo.com</example>
        public string? Email { get; set; }

        public DateOnly? DataInicio { get; set; }

        /// <example>caminho_do_avatar/avatar-jorel.jpg</example>
        public string? Avatar { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; }

        [JsonIgnore]
        public Project? Project { get; set; }
        [JsonIgnore]
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
        [JsonIgnore]
        public ICollection<DevTeamMember> DevTeamMembers { get; set; } = new List<DevTeamMember>();
    }
}
