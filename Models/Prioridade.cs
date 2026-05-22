using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace W5iAtendimentoAPI.Models
{
    [Table("Prioridades")]
    public class Prioridade
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Descricao { get; set; } = string.Empty;

        [Required]
        public int PrazoHoras { get; set; }
    }
}