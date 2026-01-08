using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RondaSegurancaBack.Data;
using RondaSegurancaBack.Models;

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
            ronda.UsuarioId = User.FindFirst("nameid")?.Value!;
            _context.Rondas.Add(ronda);
            await _context.SaveChangesAsync();
            return Ok(ronda);
        }

        [HttpGet("minhas-rondas")]
        public async Task<IActionResult> MinhasRondas()
        {
            var userId = User.FindFirst("nameid")?.Value!;
            var rondas = await _context.Rondas
                .Include(r => r.Ocorrencias)
                .Where(r => r.UsuarioId == userId)
                .ToListAsync();
            return Ok(rondas);
        }

      
    }
}
