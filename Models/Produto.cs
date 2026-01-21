using System.ComponentModel.DataAnnotations;

namespace RondaSegurancaBack.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; } = "";
    }
}
