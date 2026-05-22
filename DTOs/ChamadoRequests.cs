using System.ComponentModel.DataAnnotations;

namespace W5iAtendimentoAPI.DTOs
{
    public class CriarChamadoDTO
    {
        [Required] public int SetorId { get; set; }
        [Required] public int PrioridadeId { get; set; }

        [Required(ErrorMessage = "A descrição do chamado é obrigatória.")]
        [StringLength(500, ErrorMessage = "A descrição não pode passar de 500 caracteres.")]
        public string Descricao { get; set; } = string.Empty;
    }

    public class CheckoutChamadoDTO
    {
        [Required] 
        [MinLength(5, ErrorMessage = "A solução deve ter uma descrição mínima.")]
        public string Solucao { get; set; } = string.Empty;
    }

    public class CriarSetorDTO
    {
        [Required(ErrorMessage = "O nome do setor é obrigatório.")]
        public string Nome { get; set; } = string.Empty;
    }

    public class CriarPrioridadeDTO
    {
        [Required(ErrorMessage = "A descrição da prioridade é obrigatória.")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O prazo em horas é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O prazo deve ser maior que zero.")]
        public int PrazoHoras { get; set; }
    }
}