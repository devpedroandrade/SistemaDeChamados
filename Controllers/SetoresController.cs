using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using W5iAtendimentoAPI.Data;
using W5iAtendimentoAPI.DTOs;
using W5iAtendimentoAPI.Models;

namespace W5iAtendimentoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SetoresController : ControllerBase
    {
        private readonly AppDbContext _context;
        public SetoresController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Setor>>> GetSetores()
        {
            return await _context.Setores.ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> CriarSetor([FromBody] CriarSetorDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome))
                return BadRequest("Nome do setor é obrigatório.");

            var setor = new Setor { Nome = dto.Nome };
            _context.Setores.Add(setor);
            await _context.SaveChangesAsync();
            return Created($"/api/setores/{setor.Id}", setor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSetor(int id)
        {
            var setor = await _context.Setores.FindAsync(id);
            if (setor == null)
            {
                return NotFound("Setor não encontrado.");
            }

            var possuiChamados = await _context.Chamados.AnyAsync(c => c.SetorId == id);
            if (possuiChamados)
            {
                return BadRequest("Não é possível deletar este setor pois existem chamados vinculados a ele.");
            }

            _context.Setores.Remove(setor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}