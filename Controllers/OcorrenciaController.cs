using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
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
private readonly IConfiguration _configuration;
    public OcorrenciaController(ApplicationDbContext context,  IConfiguration configuration)
    {
        _context = context;
    _configuration = configuration;
    }

    // POST: api/Ocorrencia/UploadImagemComGps
    [HttpPost("UploadImagemComGps")]
    public async Task<IActionResult> UploadImagemComGps([FromForm] UploadOcorrenciaDto request)
    {
        var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (usuarioId == null) return Unauthorized();

        if (request.Imagem == null || request.Imagem.Length == 0)
            return BadRequest("Imagem não enviada");

        // Pasta geral de uploads
        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        // Nome único do arquivo
        var uniqueFileName = $"{Guid.NewGuid()}_{request.Imagem.FileName}";
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        // Salvar arquivo
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await request.Imagem.CopyToAsync(stream);
        }

        // Criar ocorrência
        var ocorrencia = new Ocorrencia
        {
            DataHora = DateTime.UtcNow,
            UsuarioId = usuarioId,
            Descricao = request.Descricao,
            ImagemPath = uniqueFileName, // salvar apenas o nome do arquivo
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            RondaId = request.RondaId
        };

        _context.Ocorrencias.Add(ocorrencia);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            ocorrencia.Id,
            ocorrencia.Descricao,
            ImagemUrl = $"{Request.Scheme}://{Request.Host}/uploads/{ocorrencia.ImagemPath}",
            ocorrencia.Latitude,
            ocorrencia.Longitude,
            ocorrencia.DataHora
        });
    }

    // GET: api/Ocorrencia/MinhasOcorrencias
    [HttpGet("MinhasOcorrencias")]
    public async Task<IActionResult> MinhasOcorrencias()
    {
        var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (usuarioId == null) return Unauthorized();

        var ocorrencias = await _context.Ocorrencias
            .Where(o => o.UsuarioId == usuarioId)
            .OrderByDescending(o => o.DataHora)
            .ToListAsync();

        var resultados = ocorrencias.Select(o => new
        {
            o.Id,
            o.Descricao,
            ImagemUrl = $"{Request.Scheme}://{Request.Host}/uploads/{o.ImagemPath}",
            o.Latitude,
            o.Longitude,
            o.DataHora
        });

        return Ok(resultados);
    }

    // GET: api/Ocorrencia/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> DetalheOcorrencia(int id)
    {
        var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (usuarioId == null) return Unauthorized();

        var ocorrencia = await _context.Ocorrencias
            .FirstOrDefaultAsync(o => o.Id == id && o.UsuarioId == usuarioId);

        if (ocorrencia == null) return NotFound("Ocorrência não encontrada");

        return Ok(new
        {
            ocorrencia.Id,
            ocorrencia.Descricao,
            ImagemUrl = $"{Request.Scheme}://{Request.Host}/uploads/{ocorrencia.ImagemPath}",
            ocorrencia.Latitude,
            ocorrencia.Longitude,
            ocorrencia.DataHora
        });
    }

    // GET: api/Ocorrencia/ListaOcorrenciaPorRonda?idRonda=1
    [HttpGet("ListaOcorrenciaPorRonda")]
    public async Task<IActionResult> ListaOcorrenciaPorRonda(int idRonda)
    {
        var ocorrencias = await _context.Ocorrencias
            .Where(o => o.RondaId == idRonda)
            .OrderBy(o => o.DataHora)
            .ToListAsync();

        if (!ocorrencias.Any())
            return NotFound("Nenhuma ocorrência encontrada para esta ronda");

        var detalhes = ocorrencias.Select(o => new
        {
            o.Id,
            o.Descricao,
            ImagemUrl = $"{Request.Scheme}://{Request.Host}/uploads/{o.ImagemPath}",
            o.Latitude,
            o.Longitude,
            o.DataHora
        });

        return Ok(detalhes);
    }
[HttpGet("Publica/Lista")]
[AllowAnonymous] // permite acesso sem autenticação
public async Task<IActionResult> ListaOcorrenciasPublica()
{
    var ocorrencias = await _context.Ocorrencias
        .OrderByDescending(o => o.DataHora)
        .ToListAsync();

    if (!ocorrencias.Any())
        return NotFound("Nenhuma ocorrência encontrada");

    var baseUrl = _configuration["AppSettings:BaseUrl"];

    var resultados = ocorrencias.Select(o => new
    {
        o.Id,
        o.Descricao,
        ImagemUrl = $"{baseUrl}/uploads/{o.ImagemPath}",
        o.Latitude,
        o.Longitude,
        o.DataHora
    });

    return Ok(resultados);
}
    [HttpGet("ImagemCompleta/{id}")]
    [AllowAnonymous] 
public async Task<IActionResult> ObterImagemCompleta(int id)
{
    var ocorrencia = await _context.Ocorrencias.FirstOrDefaultAsync(o => o.Id == id);
    if (ocorrencia == null) return NotFound("Ocorrência não encontrada");

    var baseUrl = _configuration["AppSettings:BaseUrl"];
    var urlCompleta = $"{baseUrl}/uploads/{ocorrencia.ImagemPath}";

    return Ok(new
    {
        ocorrencia.Id,
        ocorrencia.Descricao,
        ImagemUrl = urlCompleta,
        ocorrencia.Latitude,
        ocorrencia.Longitude,
        ocorrencia.DataHora
    });
}
}
