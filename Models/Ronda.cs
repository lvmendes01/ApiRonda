using System.ComponentModel.DataAnnotations;

namespace RondaSegurancaBack.Models
{
    public class Ronda
    {
        [Key]
        public int Id { get; set; }
        public string Local { get; set; } = "";
        public DateTime DataHora { get; set; } = DateTime.Now;
        public string UsuarioId { get; set; } = "";
        public Usuario? Usuario { get; set; }
        public List<Ocorrencia>? Ocorrencias { get; set; }
    }
}
