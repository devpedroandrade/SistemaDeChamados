using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace W5iAtendimentoAPI.Models
{
    [Table("Chamados")]
    public class Chamado
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
        public string Descricao { get; set; } = string.Empty;

        [Required]
        public int SetorId { get; set; }
        [ForeignKey("SetorId")]
        public Setor? Setor { get; set; }

        [Required]
        public int PrioridadeId { get; set; }
        [ForeignKey("PrioridadeId")]
        public Prioridade? Prioridade { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Aberto";

        public DateTime DataAbertura { get; set; } = DateTime.Now;
        public DateTime? DataCheckin { get; set; }
        public DateTime? DataCheckout { get; set; }
        
        [MaxLength(500)]
        public string? Solucao { get; set; }
    }
}