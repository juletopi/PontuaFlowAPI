using System.Text.Json.Serialization;

namespace PontuaFlow.Models
{
    /// <summary>
    /// Uma Tarefa realizada por um Desenvolvedor em uma Semana de um Projeto.
    /// </summary>
    public class Task
    {
        public int Id { get; set; }

        /// <example>1</example>
        public int ProjectId { get; set; }

        /// <example>1</example>
        public int DevId { get; set; }

        /// <example>1</example>
        public int WeekId { get; set; }

        /// <example>Criar tela de login</example>
        public required string NomeTarefa { get; set; }

        /// <example>Implementar a interface de autenticação utilizando JWT</example>
        public string? Descricao { get; set; }

        /// <example>5</example>
        [System.ComponentModel.DataAnnotations.AllowedValues(0, 2, 3, 5, 8, ErrorMessage = "A pontuação deve ser 0, 2, 3, 5 ou 8.")]
        public int Pontuacao { get; set; }

        public DateOnly DataInicio { get; set; }
        public DateOnly? DataFim { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; }

        [JsonIgnore]
        public Project? Project { get; set; }
        [JsonIgnore]
        public Dev? Dev { get; set; }
        [JsonIgnore]
        public Week? Week { get; set; }

        [JsonIgnore]
        public ICollection<TaskTeamAssignment> TaskTeamAssignments { get; set; } = new List<TaskTeamAssignment>();
    }
}
