using System.Text.Json.Serialization;

namespace PontuaFlow.Models
{
    /// <summary>
    /// Uma Semana em um Projeto.
    /// </summary>
    public class Week
    {
        public int Id { get; set; }

        /// <example>1</example>
        public int ProjectId { get; set; }

        /// <example>1</example>
        public int NumeroSemana { get; set; }

        public DateOnly? DataInicio { get; set; }
        public DateOnly? DataFim { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; }

        [JsonIgnore]
        public Project? Project { get; set; }
        [JsonIgnore]
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
