using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RondaSegurancaBack.Data;
using RondaSegurancaBack.DTO;
using RondaSegurancaBack.Models;
using System.Security.Claims;

namespace RondaSegurancaBack.Controllers
{

    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LocalizacaoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LocalizacaoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("localizacao")]
        public async Task<IActionResult> EnviarLocalizacao(
    [FromBody] LocalizacaoDto dto)
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (usuarioId == null)
                return Unauthorized();

            // Validações básicas
            if (dto.Latitude < -90 || dto.Latitude > 90)
                return BadRequest("Latitude inválida");

            if (dto.Longitude < -180 || dto.Longitude > 180)
                return BadRequest("Longitude inválida");

            if (dto.PrecisaoMetros.HasValue && dto.PrecisaoMetros > 200)
                return BadRequest("Precisão do GPS muito baixa");

            var localizacao = new AparelhoLocalizacao
            {
                UsuarioId = usuarioId,
                DeviceId = dto.DeviceId,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                PrecisaoMetros = dto.PrecisaoMetros,
                RondaId = dto.RondaId,
                DataHoraCapturaUtc = dto.DataHoraCapturaUtc,
                DataHoraRegistroUtc = DateTime.UtcNow
            };

            _context.AparelhoLocalizacoes.Add(localizacao);
            await _context.SaveChangesAsync();

            return Ok(new { status = "Localização registrada" });
        }

        [HttpGet("trajetoRonda")]
        public async Task<IActionResult> TrajetoRonda(int Id)
        {
            var rondas = await _context.AparelhoLocalizacoes
                .Where(r => r.RondaId == Id)
                .ToListAsync();
            return Ok(rondas);
        }
    }
}
