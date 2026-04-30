using System.Text.Json.Serialization;

namespace PontuaFlow.Models
{
    /// <summary>
    /// Entidade que representa um Projeto.
    /// </summary>
    public class Project
    {
        public int Id { get; set; }

        /// <example>PontuaDevs</example>
        public required string Nome { get; set; }

        /// <example>Time altamente profissional de uma software house</example>
        public string? Descricao { get; set; }

        /// <example>caminho_da_foto/foto.jpg</example>
        public string? Foto { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; }

        [JsonIgnore]
        public ICollection<Week> Weeks { get; set; } = new List<Week>();
        [JsonIgnore]
        public ICollection<Dev> Devs { get; set; } = new List<Dev>();
        [JsonIgnore]
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
        [JsonIgnore]
        public ICollection<DevTeam> DevTeams { get; set; } = new List<DevTeam>();
    }
}
