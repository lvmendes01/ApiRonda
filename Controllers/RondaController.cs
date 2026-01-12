using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RondaSegurancaBack.Data;
using RondaSegurancaBack.Models;
using System.Security.Claims;

namespace MinhaApiRonda.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RondaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RondaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("criar")]
        public async Task<IActionResult> CriarRonda([FromBody] Ronda ronda)
        {
            ronda.UsuarioCriacaoId =  User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ronda.Nome = string.IsNullOrWhiteSpace(ronda.Nome)
            ? $"Ronda Avulsa Realizada por {User.Identity.Name}"
            : ronda.Nome;

            ronda.DataHoraInicioPlanejada = DateTime.MinValue;
            ronda.DataHoraFimPlanejada = DateTime.MinValue;
            ronda.DataHoraFimRealizada = DateTime.MinValue;
            _context.Rondas.Add(ronda);
            await _context.SaveChangesAsync();
            return Ok(ronda);
        }


        [HttpPost("fecharRonda")]
        public async Task<IActionResult> FecharRonda([FromBody] int rondaId)
        {
            var ronda = await _context.Rondas
                .SingleOrDefaultAsync(r => r.Id == rondaId);
            if (ronda == null) return NotFound();

            ronda.DataHoraFimRealizada = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return Ok(ronda);
        }

        [HttpGet("minhas-rondas")]
        public async Task<IActionResult> MinhasRondas()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var rondas = await _context.Rondas
                .Where(r => r.UsuarioCriacaoId == userId)
                .ToListAsync();
            return Ok(rondas);
        }
        [HttpPost("Cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] Ronda ronda)
        {
            ronda.UsuarioResponsavelId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _context.Rondas.Add(ronda);
            await _context.SaveChangesAsync();
            return Ok(ronda);
        }
        [HttpGet("rondasPlanejadaHoje")]
        public async Task<IActionResult> RondasPlanejadaHoje()
        {
            var rondas = await _context.Rondas
                .Where(r => r.DataHoraInicioPlanejada.Date == DateTime.Today.Date)
                .ToListAsync();
            return Ok(rondas);
        }

        [HttpGet("ronda/{id}")]
        public async Task<IActionResult> RondaPeloId(int id)
        {
            var ronda = await _context.Rondas.FirstOrDefaultAsync(r => r.Id == id);

            if (ronda == null)
                return NotFound(new { message = "Ronda não encontrada" });

            return Ok(ronda);
        }
    }
}
