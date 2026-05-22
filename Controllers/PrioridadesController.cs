using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using W5iAtendimentoAPI.Data;
using W5iAtendimentoAPI.DTOs;
using W5iAtendimentoAPI.Models;

namespace W5iAtendimentoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrioridadesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PrioridadesController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prioridade>>> GetPrioridades()
        {
            return await _context.Prioridades.ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> CriarPrioridade([FromBody] CriarPrioridadeDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Descricao) || dto.PrazoHoras <= 0)
                return BadRequest("Descrição e prazo (maior que zero) são obrigatórios.");

            var prioridade = new Prioridade
            {
                Descricao = dto.Descricao,
                PrazoHoras = dto.PrazoHoras
            };
            _context.Prioridades.Add(prioridade);
            await _context.SaveChangesAsync();
            return Created($"/api/prioridades/{prioridade.Id}", prioridade);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrioridade(int id)
        {
            var prioridade = await _context.Prioridades.FindAsync(id);
            if (prioridade == null)
            {
                return NotFound("Prioridade não encontrada.");
            }

            var possuiChamados = await _context.Chamados.AnyAsync(c => c.PrioridadeId == id);
            if (possuiChamados)
            {
                return BadRequest("Não é possível deletar esta prioridade pois existem chamados vinculados a ela.");
            }

            _context.Prioridades.Remove(prioridade);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}