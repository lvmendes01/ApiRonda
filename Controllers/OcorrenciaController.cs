using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RondaSegurancaBack.Data;
using RondaSegurancaBack.DTO;
using RondaSegurancaBack.Models;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OcorrenciaController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public OcorrenciaController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("UploadImagemComGps")]
    public async Task<IActionResult> UploadImagemComGps([FromForm] UploadOcorrenciaDto request)
    {
        var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (usuarioId == null) return Unauthorized();

        if (request.Imagem == null || request.Imagem.Length == 0)
            return BadRequest("Imagem não enviada");

        // Pasta do usuário
        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", usuarioId);
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var uniqueFileName = $"{Guid.NewGuid()}_{request.Imagem.FileName}";
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await request.Imagem.CopyToAsync(stream);
        }

        // Cria a ocorrência já com imagem e GPS
        var ocorrencia = new Ocorrencia
        {
            DataHora = DateTime.UtcNow,
            UsuarioId = usuarioId,
            Descricao = request.Descricao,
            ImagemPath = $"Uploads/{usuarioId}/{uniqueFileName}",
            Latitude = request.Latitude,
            Longitude = request.Longitude
        };

        _context.Ocorrencias.Add(ocorrencia);
        await _context.SaveChangesAsync();

        return Ok(ocorrencia);
    }


    [HttpGet("MinhasOcorrencias")]
    public async Task<IActionResult> MinhasOcorrencias()
    {
        var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (usuarioId == null) return Unauthorized();

        var ocorrencias = await _context.Ocorrencias
            .Where(o => o.UsuarioId == usuarioId)
            .OrderByDescending(o => o.DataHora)
            .ToListAsync();

        return Ok(ocorrencias);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> DetalheOcorrencia(int id)
    {
        var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (usuarioId == null) return Unauthorized();

        var ocorrencia = await _context.Ocorrencias
            .FirstOrDefaultAsync(o => o.Id == id && o.UsuarioId == usuarioId);

        if (ocorrencia == null) return NotFound("Ocorrência não encontrada");

        var detalhe = new
        {
            ocorrencia.Id,
            ocorrencia.Descricao,
            ImagemUrl = $"{Request.Scheme}://{Request.Host}/uploads/{usuarioId}/{Path.GetFileName(ocorrencia.ImagemPath)}",
            ocorrencia.Latitude,
            ocorrencia.Longitude,
            ocorrencia.DataHora
        };

        return Ok(detalhe);
    }
}
