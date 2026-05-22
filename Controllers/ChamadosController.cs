using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using W5iAtendimentoAPI.Data;
using W5iAtendimentoAPI.DTOs;
using W5iAtendimentoAPI.Models;

namespace W5iAtendimentoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChamadosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ChamadosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CriarChamado([FromBody] CriarChamadoDTO dto)
        {
            var setorExiste = await _context.Setores.AnyAsync(s => s.Id == dto.SetorId);
            var prioridadeExiste = await _context.Prioridades.AnyAsync(p => p.Id == dto.PrioridadeId);

            if (!setorExiste || !prioridadeExiste)
                return BadRequest("Setor ou Prioridade inválidos.");

            var chamado = new Chamado
            {
                SetorId = dto.SetorId,
                Descricao =  dto.Descricao,
                PrioridadeId = dto.PrioridadeId,
                Status = "Aberto",
                DataAbertura = DateTime.Now
            };

            _context.Chamados.Add(chamado);
            await _context.SaveChangesAsync();

            return Created($"/api/chamados/{chamado.Id}", chamado);
        }

        [HttpPut("{id}/checkin")]
        public async Task<IActionResult> Checkin(int id)
        {
            var chamado = await _context.Chamados.FindAsync(id);

            if (chamado == null) return NotFound("Chamado não encontrado.");
            
            if (chamado.Status != "Aberto")
                return BadRequest($"Não é possível iniciar atendimento. Status atual: {chamado.Status}");

            chamado.Status = "Em Atendimento";
            chamado.DataCheckin = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok(new { Mensagem = "Atendimento iniciado com sucesso.", Chamado = chamado });
        }

        [HttpPut("{id}/checkout")]
        public async Task<IActionResult> Checkout(int id, [FromBody] CheckoutChamadoDTO dto)
        {
            var chamado = await _context.Chamados.FindAsync(id);

            if (chamado == null) return NotFound("Chamado não encontrado.");

            if (chamado.Status != "Em Atendimento")
                return BadRequest($"Só é possível finalizar chamados 'Em Atendimento'. Status atual: {chamado.Status}");

            chamado.Status = "Finalizado";
            chamado.DataCheckout = DateTime.Now;
            chamado.Solucao = dto.Solucao;

            await _context.SaveChangesAsync();
            return Ok(new { Mensagem = "Chamado finalizado com sucesso.", Chamado = chamado });
        }
        
        [HttpGet("relatorio")]
        public async Task<IActionResult> ObterRelatorio()
        {
            var chamados = await _context.Chamados
                .Include(c => c.Setor)
                .Include(c => c.Prioridade)
                .Select(c => new
                {
                    c.Id,
                    c.Descricao,
                    c.Solucao,
                    Setor = c.Setor!.Nome,
                    Prioridade = c.Prioridade!.Descricao,
                    c.Status,
                    TempoEstimadoHoras = c.Prioridade.PrazoHoras,
                    TempoTotalAtendimentoHoras = (c.DataCheckin.HasValue && c.DataCheckout.HasValue) 
                        ? Math.Round((c.DataCheckout.Value - c.DataCheckin.Value).TotalHours, 2) 
                        : (double?)null,
                    EstaAtrasado = c.DataCheckin.HasValue &&
        ((c.DataCheckout ?? DateTime.Now) - c.DataCheckin.Value).TotalHours > c.Prioridade.PrazoHoras
                })
                .ToListAsync();

            return Ok(chamados);
        }

        [HttpPut("{id}/cancelar")]

        public async Task<IActionResult> Cancelar(int id)
        {
            var chamado = await _context.Chamados.FindAsync(id);
            if (chamado == null) return NotFound("Chamado não encontrado.");
            if (chamado.Status == "Finalizado")
                return BadRequest("Não é possível cancelar um chamado finalizado.");

            chamado.Status = "Cancelado";
            await _context.SaveChangesAsync();
            return Ok(new { Mensagem = "Chamado cancelado com sucesso.", Chamado = chamado });
        }
    }
}