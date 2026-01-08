using System.ComponentModel.DataAnnotations;

namespace RondaSegurancaBack.Models
{
    public class Ocorrencia
    {
        [Key]
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string? ImagemPath { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime DataHora { get; set; }
        public string UsuarioId { get; set; } = null!;
    }
}
