using System.ComponentModel.DataAnnotations;

namespace RondaSegurancaBack.Models
{
    public class Ronda
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public string Local { get; set; } = "";
        public DateTime DataHoraInicioPlanejada { get; set; } = DateTime.Now;
        public DateTime DataHoraFimPlanejada { get; set; } = DateTime.Now;
        public DateTime DataHoraInicioRealizada { get; set; } = DateTime.Now;
        public DateTime DataHoraFimRealizada { get; set; } = DateTime.Now;
        public string UsuarioResponsavelId { get; set; } = "";
        public DateTime DataHoraCriacao { get; set; } = DateTime.Now;
        public string UsuarioCriacaoId { get; set; } = "";
    }
}
