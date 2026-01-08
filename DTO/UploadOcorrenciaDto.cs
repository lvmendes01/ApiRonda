using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace RondaSegurancaBack.DTO
{
    public class UploadOcorrenciaDto
    {
        [Required]
        public IFormFile Imagem { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        public string Descricao { get; set; }

        public int? RondaId { get; set; } // opcional
    }
}
