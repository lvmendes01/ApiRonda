using System.ComponentModel.DataAnnotations;

namespace RondaSegurancaBack.Models
{
    public class AparelhoLocalizacao
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public long RondaId { get; set; } 

        [Required]
        [MaxLength(100)]
        public string DeviceId { get; set; } = null!;

        [Required]
        public string UsuarioId { get; set; } = null!;

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        // Precisão do GPS em metros
        public double? PrecisaoMetros { get; set; }

        // Hora capturada no dispositivo (UTC)
        public DateTime DataHoraCapturaUtc { get; set; }

        // Hora que o backend gravou
        public DateTime DataHoraRegistroUtc { get; set; }
    }
}
